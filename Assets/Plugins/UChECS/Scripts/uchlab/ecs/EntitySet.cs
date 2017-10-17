namespace uchlab.ecs {
    public class EntitySet<T> : BaseEntitySet<ComponentGroup<T>> {
        protected override void PopulateValidComponentsSet() {
            validComponents.Add(typeof(T));
        }

        protected override ComponentGroup<T> CreateGroup(Entity entity) {
            return new ComponentGroup<T>(entity, entity.GetComponent<T>());
        }
    }

    public class EntitySet<T1,T2> : BaseEntitySet<ComponentGroup<T1,T2>> {
        protected override void PopulateValidComponentsSet() {
            validComponents.Add(typeof(T1));
            validComponents.Add(typeof(T2));
        }

        protected override ComponentGroup<T1,T2> CreateGroup(Entity entity) {
            return new ComponentGroup<T1,T2>(entity,
                    entity.GetComponent<T1>(),
                    entity.GetComponent<T2>()
            );
        }
    }

    public class EntitySet<T1,T2,T3> : BaseEntitySet<ComponentGroup<T1,T2,T3>> {
        protected override void PopulateValidComponentsSet() {
            validComponents.Add(typeof(T1));
            validComponents.Add(typeof(T2));
            validComponents.Add(typeof(T3));
        }

        protected override ComponentGroup<T1,T2,T3> CreateGroup(Entity entity) {
            return new ComponentGroup<T1,T2,T3>(entity,
                    entity.GetComponent<T1>(),
                    entity.GetComponent<T2>(),
                    entity.GetComponent<T3>()
            );
        }
    }

    public class EntitySet<T1,T2,T3,T4> : BaseEntitySet<ComponentGroup<T1,T2,T3,T4>> {
        protected override void PopulateValidComponentsSet() {
            validComponents.Add(typeof(T1));
            validComponents.Add(typeof(T2));
            validComponents.Add(typeof(T3));
            validComponents.Add(typeof(T4));
        }

        protected override ComponentGroup<T1,T2,T3,T4> CreateGroup(Entity entity) {
            return new ComponentGroup<T1,T2,T3,T4>(entity,
                    entity.GetComponent<T1>(),
                    entity.GetComponent<T2>(),
                    entity.GetComponent<T3>(),
                    entity.GetComponent<T4>()
            );
        }
    }

    public class EntitySet<T1,T2,T3,T4,T5> : BaseEntitySet<ComponentGroup<T1,T2,T3,T4,T5>> {
        protected override void PopulateValidComponentsSet() {
            validComponents.Add(typeof(T1));
            validComponents.Add(typeof(T2));
            validComponents.Add(typeof(T3));
            validComponents.Add(typeof(T4));
            validComponents.Add(typeof(T5));
        }

        protected override ComponentGroup<T1,T2,T3,T4,T5> CreateGroup(Entity entity) {
            return new ComponentGroup<T1,T2,T3,T4,T5>(entity,
                    entity.GetComponent<T1>(),
                    entity.GetComponent<T2>(),
                    entity.GetComponent<T3>(),
                    entity.GetComponent<T4>(),
                    entity.GetComponent<T5>()
            );
        }
    }
}