namespace uchlab.ecs.processor {
    public abstract class BaseEntitySetProcessor<CG>  where CG : struct, IComponentGroup {
        protected IEntityRepository repository;
        protected BaseEntitySet<CG> entities;

        public BaseEntitySetProcessor(IEntityRepository repository, bool autoInit) {
            this.repository = repository;
            if(autoInit){
                Init();
            }
        }

        protected virtual void Init(){
            entities = GetEntitySet();
            entities.NodeAdded += internalOnNodeAdded;
            entities.NodeRemoved += internalOnNodeRemoved;

            if (entities.Count > 0) {
                var count = entities.Count;
                for (var i = 0; i < count; i++) {
                    internalOnNodeAdded(entities[i]);
                }
            }
        }

        protected abstract BaseEntitySet<CG> GetEntitySet();

        protected abstract void internalOnNodeAdded(CG group);

        protected abstract void internalOnNodeRemoved(CG group);
    }

    public abstract class EntitySetProcessor<T> : BaseEntitySetProcessor<ComponentGroup<T>> {

        public EntitySetProcessor(uchlab.ecs.IEntityRepository repository, bool autoInit = true) : base(repository, autoInit) {
        }

        protected override BaseEntitySet<ComponentGroup<T>> GetEntitySet() {
            return repository.GetEntitySet<T>();
        }

        protected sealed override void internalOnNodeAdded(ComponentGroup<T> group) {
            OnNodeAdded(group.entity, group.component);
        }

        protected sealed override void internalOnNodeRemoved(ComponentGroup<T> group) {
            OnNodeRemoved(group.entity, group.component);
        }

        protected virtual void OnNodeAdded(Entity entity, T component) {
        }

        protected virtual void OnNodeRemoved(Entity entity, T component) {
        }
    }

    public abstract class EntitySetProcessor<T1,T2> : BaseEntitySetProcessor<ComponentGroup<T1, T2>> {
        public EntitySetProcessor(uchlab.ecs.IEntityRepository repository, bool autoInit = true) : base(repository, autoInit) {
        }

        protected override BaseEntitySet<ComponentGroup<T1, T2>> GetEntitySet() {
            return repository.GetEntitySet<T1, T2>();
        }

        protected sealed override void internalOnNodeAdded(ComponentGroup<T1, T2> group) {
            OnNodeAdded(group.entity, group.component1, group.component2);
        }

        protected sealed override void internalOnNodeRemoved(ComponentGroup<T1, T2> group) {
            OnNodeRemoved(group.entity, group.component1, group.component2);
        }

        protected virtual void OnNodeAdded(Entity entity, T1 component1, T2 component2) {
        }

        protected virtual void OnNodeRemoved(Entity entity, T1 component1, T2 component2) {
        }

    }

    public abstract class EntitySetProcessor<T1,T2,T3> : BaseEntitySetProcessor<ComponentGroup<T1, T2, T3>> {
        public EntitySetProcessor(uchlab.ecs.IEntityRepository repository, bool autoInit = true) : base(repository, autoInit) {
        }

        protected override BaseEntitySet<ComponentGroup<T1, T2, T3>> GetEntitySet() {
            return repository.GetEntitySet<T1, T2, T3>();
        }

        protected sealed override void internalOnNodeAdded(ComponentGroup<T1, T2, T3> group) {
            OnNodeAdded(group.entity, group.component1, group.component2, group.component3);
        }

        protected sealed override void internalOnNodeRemoved(ComponentGroup<T1, T2, T3> group) {
            OnNodeRemoved(group.entity, group.component1, group.component2, group.component3);
        }

        protected virtual void OnNodeAdded(Entity entity, T1 component1, T2 component2, T3 component3) {
        }

        protected virtual void OnNodeRemoved(Entity entity, T1 component1, T2 component2, T3 component3) {
        }
    }

    public abstract class EntitySetProcessor<T1,T2,T3,T4> : BaseEntitySetProcessor<ComponentGroup<T1, T2, T3, T4>> {
        public EntitySetProcessor(uchlab.ecs.IEntityRepository repository, bool autoInit = true) : base(repository, autoInit) {
        }

        protected override BaseEntitySet<ComponentGroup<T1, T2, T3, T4>> GetEntitySet() {
            return repository.GetEntitySet<T1, T2, T3, T4>();
        }

        protected sealed override void internalOnNodeAdded(ComponentGroup<T1, T2, T3,T4> group) {
            OnNodeAdded(group.entity, group.component1, group.component2, group.component3, group.component4);
        }

        protected sealed override void internalOnNodeRemoved(ComponentGroup<T1, T2, T3,T4> group) {
            OnNodeRemoved(group.entity, group.component1, group.component2, group.component3, group.component4);
        }

        protected virtual void OnNodeAdded(Entity entity, T1 component1, T2 component2, T3 component3, T4 component4) {
        }

        protected virtual void OnNodeRemoved(Entity entity, T1 component1, T2 component2, T3 component3, T4 component4) {
        }
    }

    public abstract class EntitySetProcessor<T1,T2,T3,T4,T5> : BaseEntitySetProcessor<ComponentGroup<T1, T2, T3, T4, T5>> {
        public EntitySetProcessor(uchlab.ecs.IEntityRepository repository, bool autoInit = true) : base(repository, autoInit) {
        }

        protected override BaseEntitySet<ComponentGroup<T1, T2, T3, T4, T5>> GetEntitySet() {
            return repository.GetEntitySet<T1, T2, T3, T4, T5>();
        }

        protected sealed override void internalOnNodeAdded(ComponentGroup<T1, T2, T3,T4,T5> group) {
            OnNodeAdded(group.entity, group.component1, group.component2, group.component3, group.component4, group.component5);
        }

        protected sealed override void internalOnNodeRemoved(ComponentGroup<T1, T2, T3,T4,T5> group) {
            OnNodeRemoved(group.entity, group.component1, group.component2, group.component3, group.component4, group.component5);
        }

        protected virtual void OnNodeAdded(Entity entity, T1 component1, T2 component2, T3 component3, T4 component4, T5 component5) {
        }

        protected virtual void OnNodeRemoved(Entity entity, T1 component1, T2 component2, T3 component3, T4 component4, T5 component5) {
        }
    }
}