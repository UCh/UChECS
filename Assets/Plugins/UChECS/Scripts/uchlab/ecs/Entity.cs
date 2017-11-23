using System;
using System.Collections.Generic;
using UnityEngine;

namespace uchlab.ecs
{

    public delegate void EntityComponentChange(Entity entity, object component, Type type);

    public class Entity
    {

        public readonly GameObject gameObject;

        public event Action<Entity> OnDispose;

        private HashSet<Type> componentTypes = new HashSet<Type>();
        private Dictionary<Type, object> componentsByType = new Dictionary<Type, object>();
        private Dictionary<object, Type> typesByComponent = new Dictionary<object, Type>();

        public IEnumerable<Type> ComponentTypes
        {
            get
            {
                return componentTypes;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return componentTypes.Count == 0;
            }
        }

        private EntityComponentChange componentAddedDelegate;
        private EntityComponentChange componentRemovedDelegate;

        public Entity()
        {
        }

        public Entity(GameObject entity, EntityComponentChange componentAddedDelegate, EntityComponentChange componentRemovedDelegate)
        {
            this.gameObject = entity;
            this.componentAddedDelegate = componentAddedDelegate;
            this.componentRemovedDelegate = componentRemovedDelegate;
        }

        public bool AddComponent<T>(object component)
        {
            return AddComponent(component, typeof(T));
        }

        public bool AddComponent(object component)
        {
            return AddComponent(component, component.GetType());
        }

        public bool AddComponent(object component, Type componentType)
        {
            if (componentTypes.Contains(componentType))
            {
                return false;
            }

            typesByComponent.Add(component, componentType);
            componentTypes.Add(componentType);
            componentsByType.Add(componentType, component);

            if (component is IRegistrableComponent)
            {
                var registrable = (IRegistrableComponent)component;
                registrable.AttachedTo(this);
            }

            componentAddedDelegate(this, component, componentType);

            return true;
        }

        public bool RemoveComponent<T>()
        {
            return RemoveComponent(typeof(T));

        }
        public bool RemoveComponent(Type componentType)
        {
            object component;
            if (!componentsByType.TryGetValue(componentType, out component))
            {
                return false;
            }

            return RemoveComponent(component, componentType);
        }
        public bool RemoveComponent(object component)
        {
            Type componentType;

            if (!typesByComponent.TryGetValue(component, out componentType))
            {
                return false;
            }

            return RemoveComponent(component, componentType);
        }

        private bool RemoveComponent(object component, Type componentType)
        {
            if (componentTypes.Remove(componentType))
            {
                componentsByType.Remove(componentType);
                if (component is IRegistrableComponent)
                {
                    var registrable = (IRegistrableComponent)component;
                    registrable.Detach();
                }

                componentRemovedDelegate(this, component, componentType);
                return true;
            }
            return false;
        }

        public bool HasComponentOfType<T>()
        {
            return componentTypes.Contains(typeof(T));
        }

        public bool HasComponentOfType(Type type)
        {
            return componentTypes.Contains(type);
        }

        public object GetComponent(Type type)
        {
            return componentsByType[type];
        }

        public T GetComponent<T>()
        {
            return (T)componentsByType[typeof(T)];
        }

        public bool TryGetComponent<T>(out T component)
        {
            object internalComponent;
            if (componentsByType.TryGetValue(typeof(T), out internalComponent))
            {
                component = (T)internalComponent;
                return true;
            }

            component = default(T);
            return false;
        }

        public void Dispose()
        {
            if(OnDispose != null){
                OnDispose(this);
            }
            OnDispose = null;
            componentTypes.Clear();
            componentsByType.Clear();

            DestroyGameObject();
        }

        protected virtual void DestroyGameObject()
        {
            GameObject.Destroy(gameObject);
        }

    }
}