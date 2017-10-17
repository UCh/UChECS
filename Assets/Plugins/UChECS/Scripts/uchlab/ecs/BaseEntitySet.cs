using System;
using System.Collections.Generic;

namespace uchlab.ecs {
    public interface IEntitySet {
        bool AddIfValid(Entity entity);

        bool ContainComponentOfType(Type type);

        bool CointainEntity(Entity entity);

        void ComponentRemoved(Entity entity, Type type);

        void RemoveEntity(Entity entity);
    }

    public abstract class BaseEntitySet<CG> : IEntitySet where CG : struct, IComponentGroup  {
        public static int DEFAULT_CAPACITY = 100;

        public event Action<CG> NodeAdded = delegate(CG obj) {
        };

        public event Action<CG> NodeRemoved = delegate(CG obj) {
        };

        protected CG[] groups;

        protected HashSet<Type> validComponents;
        protected Dictionary<Entity, int> entityIndexes;

        public int Count {
            get {
                return count;
            }
        }

        public CG this[int index]{
            get {
                return groups[index];
            }
        }

        public CG this[Entity entity]{
            get {
                return groups[entityIndexes[entity]];
            }
        }

        private int count;
        private int capacity;
        private CG emptyElement = new CG();

        public BaseEntitySet() {
            validComponents = new HashSet<Type>();
            entityIndexes = new Dictionary<Entity, int>();

            PopulateValidComponentsSet();

            groups = new CG[DEFAULT_CAPACITY];
            capacity = DEFAULT_CAPACITY;
            count = 0;
        }

        protected abstract CG CreateGroup(Entity entity);

        protected abstract void PopulateValidComponentsSet();

        public bool AddIfValid(Entity entity) {
            if (!validComponents.IsSubsetOf(entity.ComponentTypes)) {
                return false;
            }

            if (entityIndexes.ContainsKey(entity)) {
                return false;
            }

            var group = CreateGroup(entity);

            entityIndexes[entity] = count;

            if (count == capacity) {
                capacity *= 2;
                Array.Resize<CG>(ref groups, capacity);
            }

            groups[count] = group;
            count++;

            NodeAdded(group);

            return true;
        }

        public bool ContainComponentOfType(Type type) {
            return validComponents.Contains(type);
        }

        public bool CointainEntity(Entity entity) {
            return entityIndexes.ContainsKey(entity);
        }

        public void ComponentRemoved(Entity entity, Type type) {
            if (validComponents.Contains(type) && entityIndexes.ContainsKey(entity)) {
                RemoveEntity(entity);
            }
        }

        public void RemoveEntity(Entity entity) {
            var index = entityIndexes[entity];
            count--;
            CG group = groups[index];
            groups[index] = groups[count];
            entityIndexes[groups[index].Entity] = index;
            entityIndexes.Remove(entity);
            groups[count] = emptyElement;

            NodeRemoved(group);
        }

        public void ForEach(Action<CG> callback) {
            for (var i = 0; i < count; i++) {
                callback(groups[i]);
            }
        }
    }
}