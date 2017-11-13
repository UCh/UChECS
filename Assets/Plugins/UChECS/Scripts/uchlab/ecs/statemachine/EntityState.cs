using System;
using System.Collections.Generic;

namespace uchlab.ecs.statemachine
{
    public class EntityState<T>
    {
        public T Key { get; private set; }
        private List<object> components;
        private List<Func<object>> providers;
        private List<Type> componentsType;

        public EntityState(T key)
        {
            Key = key;
            components = new List<object>();
            componentsType = new List<Type>();
            providers = new List<Func<object>>();
        }
        public EntityState<T> withComponent<C>(object component)
        {
            return withComponent(component, typeof(C));
        }
        public EntityState<T> withComponent(object component)
        {
            return withComponent(component, component.GetType());
        }
        public EntityState<T> withComponent(object component, Type componentType)
        {
            components.Add(component);
            componentsType.Add(componentType);
            return this;
        }

        public EntityState<T> withProvider<C>(Func<object> provider)
        {
            return withProvider(provider, typeof(C));
        }
        public EntityState<T> withProvider(Func<object> provider, Type componentType)
        {
            providers.Add(provider);
            componentsType.Add(componentType);
            return this;
        }

        public void Enter(Entity entity)
        {
            for (int i = 0; i < components.Count; i++)
            {
                entity.AddComponent(components[i]);
            }

            for (int i = 0; i < providers.Count; i++)
            {
                entity.AddComponent(providers[i]());
            }
        }

        public void Exit(Entity entity)
        {
            for (int i = 0; i < componentsType.Count; i++)
            {
                entity.RemoveComponent(componentsType[i]);
            }
        }

    }
}