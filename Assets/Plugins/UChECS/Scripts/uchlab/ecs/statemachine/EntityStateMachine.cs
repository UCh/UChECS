using System;
using System.Collections.Generic;
using uchlab.ecs;

namespace uchlab.ecs.statemachine
{
    public class EntityStateMachine<T> : BaseRegistrableComponent
    {
        public EntityStateMachine()
        {
            stateTypeMap = new Dictionary<T, EntityState<T>>();
        }
        public EntityState<T> State { get; private set; }
        public T StateKey { get{ return State.Key;} }
        private Dictionary<T, EntityState<T>> stateTypeMap;
        override public void AttachedTo(Entity entity)
        {
            base.AttachedTo(entity);
            State.Enter(entity);
        }

        override public void Detach()
        {
            State.Exit(Entity);
            base.Detach();
        }

        public EntityState<T> CreateState(T key)
        {
            if (stateTypeMap.ContainsKey(key))
            {
                throw new Exception("Can't create state. State already exist");
            }

            var newState = new EntityState<T>(key);
            stateTypeMap.Add(key, newState);

            if(State == null){
                State = newState;
            }

            return newState;
        }

        public bool SetState(T newStateKey)
        {
            if (newStateKey.Equals(State.Key))
            {
                return false;
            }

            EntityState<T> newState;

            if (stateTypeMap.TryGetValue(newStateKey, out newState))
            {
                if (IsAttachedToEntity)
                {
                    State.Exit(Entity);
                    newState.Enter(Entity);
                }

                State = newState;
                return true;
            }

            throw new Exception("Can't set a non-registered state");
        }
    }
}