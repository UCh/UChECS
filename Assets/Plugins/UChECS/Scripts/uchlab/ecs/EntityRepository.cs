using System;
using System.Collections.Generic;
using UnityEngine;

namespace uchlab.ecs {
    public class EntityRepository : IEntityRepository {
        public static Entity DefaultEntityFactoryMethod(GameObject gameObject, EntityComponentChange componentAddedDelegate, EntityComponentChange componentRemovedDelegate) {
            return new Entity(gameObject, componentAddedDelegate, componentRemovedDelegate);
        }

        private Func<GameObject, EntityComponentChange, EntityComponentChange, Entity> CreateEntity = DefaultEntityFactoryMethod;

        public Func<GameObject, EntityComponentChange, EntityComponentChange, Entity> EntityFactoryMethod {
            get {
                return CreateEntity;
            }
            set {
                if (value == null) {
                    CreateEntity = DefaultEntityFactoryMethod;
                } else {
                    CreateEntity = value;
                }
            }
        }

        private Dictionary<GameObject, Entity> gameObjectEntity = new Dictionary<GameObject, Entity>();
        private List<Entity> entities = new List<Entity>();
        private List<IEntitySet> componentSetLists = new List<IEntitySet>();

        public int Count {
            get {
                return entities.Count;
            }
        }

        public bool IsRegistered(GameObject gameObject) {
            return gameObjectEntity.ContainsKey(gameObject);
        }

        public Entity GetEntityFor(GameObject gameObject) {
            return gameObjectEntity[gameObject];
        }

        public Entity AddComponent<T>(GameObject gameObject, object component) {
            Entity entity = GetOrCreateEntity(gameObject);
            entity.AddComponent<T>(component);
            return entity;
        }

        public Entity AddComponent(GameObject gameObject, object component) {
            Entity entity = GetOrCreateEntity(gameObject);
            entity.AddComponent(component);
            return entity;
        }

        private Entity GetOrCreateEntity(GameObject gameObject) {
            Entity entity;
            if (gameObjectEntity.TryGetValue(gameObject, out entity))
            {
                return entity;
            }

            entity = CreateEntity(gameObject, OnComponentAdded, OnComponentRemoved);
            gameObjectEntity[gameObject] = entity;
            entities.Add(entity);

            return entity;
        }

        private void OnComponentAdded(Entity entity, object component, Type type) {
            var count = componentSetLists.Count;
            for (var i = 0; i < count; i++) {
                componentSetLists[i].AddIfValid(entity);
            }
        }

        private void OnComponentRemoved(Entity entity, object component, Type type) {
            var count = componentSetLists.Count;
            for (var i = 0; i < count; i++) {
                componentSetLists[i].ComponentRemoved(entity, type);
            }
        }

        public void RemoveComponent(GameObject gameObject, object component) {
            Entity entity;
            if (!gameObjectEntity.TryGetValue(gameObject, out entity))
            {
                return;
            }

            entity.RemoveComponent(component);
        }

        public EntitySet<T> GetEntitySet<T>() {

            var entitySet = new EntitySet<T>();
            registerEntitySet(entitySet);

            return entitySet;
        }

        public EntitySet<T1, T2> GetEntitySet<T1,T2>() {

            var entitySet = new EntitySet<T1, T2>();
            registerEntitySet(entitySet);

            return entitySet;
        }

        public EntitySet<T1, T2, T3> GetEntitySet<T1,T2,T3>() {

            var entitySet = new EntitySet<T1, T2, T3>();
            registerEntitySet(entitySet);

            return entitySet;
        }

        public EntitySet<T1, T2, T3, T4> GetEntitySet<T1,T2,T3,T4>() {

            var entitySet = new EntitySet<T1, T2, T3, T4>();
            registerEntitySet(entitySet);

            return entitySet;
        }

        public EntitySet<T1, T2, T3, T4, T5> GetEntitySet<T1,T2,T3,T4,T5>() {

            var entitySet = new EntitySet<T1, T2, T3, T4, T5>();
            registerEntitySet(entitySet);

            return entitySet;
        }

        private void registerEntitySet(IEntitySet entitySet) {
            entities.ForEach((e) => entitySet.AddIfValid(e));
            componentSetLists.Add(entitySet);
        }

        public void DestroyEntity(Entity entity) {
            if (!gameObjectEntity.ContainsValue(entity))
            {
                return;
            }

            internalDestroyEntity(entity);
        }

        public void DestroyEntity(GameObject gameObject) {
            Entity entity;
            if (!gameObjectEntity.TryGetValue(gameObject, out entity))
            {
                return;
            }
            internalDestroyEntity(entity);
        }

        private void internalDestroyEntity(Entity entity) {
            componentSetLists.ForEach((cl) => cl.RemoveEntity(entity));
            gameObjectEntity.Remove(entity.gameObject);
            entities.Remove(entity);
            entity.Dispose();
        }
    }
}