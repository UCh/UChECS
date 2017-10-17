using System;

namespace uchlab.ecs {
    public abstract class ComponentSet {
        public readonly Entity entity;

        public ComponentSet() {
        }

        public ComponentSet(Entity entity) {
            this.entity = entity;
        }
    }

    public interface IComponentGroup {
        Entity Entity { get; }
    }

    public struct ComponentGroup<T> : IEquatable<ComponentGroup<T>>, IComponentGroup {
        public readonly Entity entity;

        public Entity Entity {
            get {
                return entity;
            }
        }

        public readonly T component;

        public ComponentGroup(Entity entity, T component) {
            this.entity = entity;
            this.component = component;
        }

        public bool Equals(ComponentGroup<T> other) {
            return other.entity == entity;
        }
    }

    public struct ComponentGroup<T1,T2> : IEquatable<ComponentGroup<T1, T2>>, IComponentGroup {
        public readonly Entity entity;

        public Entity Entity {
            get {
                return entity;
            }
        }

        public readonly T1 component1;
        public readonly T2 component2;

        public ComponentGroup(Entity entity,
                T1 component1,
                T2 component2) {
            this.entity = entity;
            this.component1 = component1;
            this.component2 = component2;
        }

        public bool Equals(ComponentGroup<T1, T2> other) {
            return other.entity == entity;
        }

    }

    public struct ComponentGroup<T1,T2,T3> : IEquatable<ComponentGroup<T1, T2, T3>>, IComponentGroup {
        public readonly Entity entity;

        public Entity Entity {
            get {
                return entity;
            }
        }

        public readonly T1 component1;
        public readonly T2 component2;
        public readonly T3 component3;

        public ComponentGroup(Entity entity,
                T1 component1,
                T2 component2,
                T3 component3) {
            this.entity = entity;
            this.component1 = component1;
            this.component2 = component2;
            this.component3 = component3;
        }

        public bool Equals(ComponentGroup<T1, T2, T3> other) {
            return other.entity == entity;
        }

    }

    public struct ComponentGroup<T1,T2,T3,T4> : IEquatable<ComponentGroup<T1, T2, T3, T4>>, IComponentGroup {
        public readonly Entity entity;

        public Entity Entity {
            get {
                return entity;
            }
        }

        public readonly T1 component1;
        public readonly T2 component2;
        public readonly T3 component3;
        public readonly T4 component4;

        public ComponentGroup(Entity entity,
                T1 component1,
                T2 component2,
                T3 component3,
                T4 component4) {
            this.entity = entity;
            this.component1 = component1;
            this.component2 = component2;
            this.component3 = component3;
            this.component4 = component4;
        }

        public bool Equals(ComponentGroup<T1, T2, T3, T4> other) {
            return other.entity == entity;
        }

    }

    public struct ComponentGroup<T1,T2,T3,T4,T5> : IEquatable<ComponentGroup<T1, T2, T3, T4, T5>>, IComponentGroup {
        public readonly Entity entity;

        public Entity Entity {
            get {
                return entity;
            }
        }

        public readonly T1 component1;
        public readonly T2 component2;
        public readonly T3 component3;
        public readonly T4 component4;
        public readonly T5 component5;

        public ComponentGroup(Entity entity,
                T1 component1,
                T2 component2,
                T3 component3,
                T4 component4,
                T5 component5) {
            this.entity = entity;
            this.component1 = component1;
            this.component2 = component2;
            this.component3 = component3;
            this.component4 = component4;
            this.component5 = component5;
        }

        public bool Equals(ComponentGroup<T1, T2, T3, T4, T5> other) {
            return other.entity == entity;
        }
    }
}