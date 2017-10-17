using System;
using UnityEngine;

namespace uchlab.ecs.processor {

    public abstract class BaseTickableEntitySetProcessor<CG> : BaseEntitySetProcessor<CG>, ITickableProcessor  where CG : struct, IComponentGroup {

        private struct RemovePair {
            public readonly GameObject gameObject;
            public readonly object component;

            public RemovePair(GameObject gameObject, object component) {
                this.gameObject = gameObject;
                this.component = component;
            }
        }

        private static RemovePair emptyRemoveItem = new RemovePair(null,null);

        public readonly Func<float> DeltaSecondsProvider;
        private RemovePair[] ToRemove;
        private int ToRemoveIndex = 0;

        public BaseTickableEntitySetProcessor(IEntityRepository repository, Func<float> DeltaSecondsProvider, bool autoInit) : base(repository, autoInit) {
            this.DeltaSecondsProvider = DeltaSecondsProvider;
            ToRemove = new RemovePair[10];
        }

        public abstract void Tick();

        protected void RemoveComponent(Entity entity, object component) {
            if (ToRemoveIndex == ToRemove.Length) {
                Array.Resize<RemovePair>(ref ToRemove, ToRemove.Length * 2);
            }
            ToRemove[ToRemoveIndex] = new RemovePair(entity.gameObject, component);
            ToRemoveIndex++;
        }

        protected void RemoveComponents() {
            for (var i = 0; i < ToRemoveIndex; i++) {
                repository.RemoveComponent(ToRemove[i].gameObject, ToRemove[i].component);
                ToRemove[i] = emptyRemoveItem;
            }

            ToRemoveIndex = 0;
        }
    }

    public abstract class TickableEntitySetProcessor<T> : BaseTickableEntitySetProcessor<ComponentGroup<T>> {

        public TickableEntitySetProcessor(uchlab.ecs.IEntityRepository repository, Func<float> DeltaSecondsProvider, bool autoInit = true) : base(repository, DeltaSecondsProvider, autoInit) {
        }

        protected override BaseEntitySet<ComponentGroup<T>> GetEntitySet() {
            return repository.GetEntitySet<T>();
        }

        public sealed override void Tick() {
            var count = entities.Count;
            for (var i = 0; i < count; i++) {
                var group = entities[i];
                ProcessEntity(group.entity, group.component);
            }

            RemoveComponents();
        }

        protected abstract void ProcessEntity(Entity entity, T component);

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

    public abstract class TickableEntitySetProcessor<T1,T2> : BaseTickableEntitySetProcessor<ComponentGroup<T1, T2>> {

        public TickableEntitySetProcessor(uchlab.ecs.IEntityRepository repository, Func<float> DeltaSecondsProvider, bool autoInit = true) : base(repository, DeltaSecondsProvider, autoInit) {
        }

        protected override BaseEntitySet<ComponentGroup<T1, T2>> GetEntitySet() {
            return repository.GetEntitySet<T1, T2>();
        }

        public sealed override void Tick() {
            var count = entities.Count;
            for (var i = 0; i < count; i++) {
                var group = entities[i];
                ProcessEntity(group.entity, group.component1, group.component2);
            }

            RemoveComponents();
        }

        protected abstract void ProcessEntity(Entity entity, T1 component1, T2 component2);

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

    public abstract class TickableEntitySetProcessor<T1,T2,T3> : BaseTickableEntitySetProcessor<ComponentGroup<T1, T2, T3>> {

        public TickableEntitySetProcessor(uchlab.ecs.IEntityRepository repository, Func<float> DeltaSecondsProvider, bool autoInit = true) : base(repository, DeltaSecondsProvider, autoInit) {
        }

        protected override BaseEntitySet<ComponentGroup<T1, T2, T3>> GetEntitySet() {
            return repository.GetEntitySet<T1, T2, T3>();
        }

        public sealed override void Tick() {
            var count = entities.Count;
            for (var i = 0; i < count; i++) {
                var group = entities[i];
                ProcessEntity(group.entity, group.component1, group.component2, group.component3);
            }

            RemoveComponents();
        }

        protected abstract void ProcessEntity(Entity entity, T1 component1, T2 component2, T3 component3);

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

    public abstract class TickableEntitySetProcessor<T1,T2,T3,T4> : BaseTickableEntitySetProcessor<ComponentGroup<T1, T2, T3, T4>> {

        public TickableEntitySetProcessor(uchlab.ecs.IEntityRepository repository, Func<float> DeltaSecondsProvider, bool autoInit = true) : base(repository, DeltaSecondsProvider, autoInit) {
        }

        protected override BaseEntitySet<ComponentGroup<T1, T2, T3, T4>> GetEntitySet() {
            return repository.GetEntitySet<T1, T2, T3, T4>();
        }

        public sealed override void Tick() {
            var count = entities.Count;
            for (var i = 0; i < count; i++) {
                var group = entities[i];
                ProcessEntity(group.entity, group.component1, group.component2, group.component3, group.component4);
            }

            RemoveComponents();
        }

        protected abstract void ProcessEntity(Entity entity, T1 component1, T2 component2, T3 component3, T4 component4);

        protected sealed override void internalOnNodeAdded(ComponentGroup<T1, T2, T3, T4> group) {
            OnNodeAdded(group.entity, group.component1, group.component2, group.component3, group.component4);
        }

        protected sealed override void internalOnNodeRemoved(ComponentGroup<T1, T2, T3, T4> group) {
            OnNodeRemoved(group.entity, group.component1, group.component2, group.component3, group.component4);
        }

        protected virtual void OnNodeAdded(Entity entity, T1 component1, T2 component2, T3 component3, T4 component4) {
        }

        protected virtual void OnNodeRemoved(Entity entity, T1 component1, T2 component2, T3 component3, T4 component4) {
        }
    }

    public abstract class TickableEntitySetProcessor<T1,T2,T3,T4,T5> : BaseTickableEntitySetProcessor<ComponentGroup<T1, T2, T3, T4, T5>> {

        public TickableEntitySetProcessor(uchlab.ecs.IEntityRepository repository, Func<float> DeltaSecondsProvider, bool autoInit = true) : base(repository, DeltaSecondsProvider, autoInit) {
        }

        protected override BaseEntitySet<ComponentGroup<T1, T2, T3, T4, T5>> GetEntitySet() {
            return repository.GetEntitySet<T1, T2, T3, T4, T5>();
        }

        public sealed override void Tick() {
            var count = entities.Count;
            for (var i = 0; i < count; i++) {
                var group = entities[i];
                ProcessEntity(group.entity, group.component1, group.component2, group.component3, group.component4, group.component5);
            }

            RemoveComponents();
        }

        protected virtual void ProcessEntity(Entity entity, T1 component1, T2 component2, T3 component3, T4 component4, T5 component5) {
        }

        protected sealed override void internalOnNodeAdded(ComponentGroup<T1, T2, T3, T4, T5> group) {
            OnNodeAdded(group.entity, group.component1, group.component2, group.component3, group.component4, group.component5);
        }

        protected sealed override void internalOnNodeRemoved(ComponentGroup<T1, T2, T3, T4, T5> group) {
            OnNodeRemoved(group.entity, group.component1, group.component2, group.component3, group.component4, group.component5);
        }

        protected virtual void OnNodeAdded(Entity entity, T1 component1, T2 component2, T3 component3, T4 component4, T5 component5) {
        }

        protected virtual void OnNodeRemoved(Entity entity, T1 component1, T2 component2, T3 component3, T4 component4, T5 component5) {
        }
    }
}