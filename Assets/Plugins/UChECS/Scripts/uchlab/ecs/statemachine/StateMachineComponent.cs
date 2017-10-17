using System;
using System.Collections.Generic;

namespace uchlab.ecs.statemachine {
    public class StateMachineComponent<T> : BaseRegistrableComponent where T : struct, IConvertible, IEquatable<T>{

        public T State { get; private set; }

        private Dictionary<T, List<object>> stateComponentMap;

        public StateMachineComponent() : base() {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
        }

        public bool ChangeState(T newState) {
            if (newState.Equals(State)) {
                return false;
            }

            removeComponentForState(State);
            addComponentsForState(newState);
            State = newState;
            return true;
        }

        public void AddComponentToState(T key, object component) {
            List<object> components;

            if (!stateComponentMap.TryGetValue(key, out components)) {
                components = new List<object>();
                stateComponentMap.Add(key, components);
            }

            components.Add(component);
        }

        private void removeComponentForState(T key) {
            List<object> components;

            if (stateComponentMap.TryGetValue(key, out components)) {
                for (int i = 0; i < components.Count; i++) {
                    this.Entity.RemoveComponent(components[i]);
                }
            }
        }

        private void addComponentsForState(T key) {
            List<object> components;

            if (stateComponentMap.TryGetValue(key, out components)) {
                for (int i = 0; i < components.Count; i++) {
                    this.Entity.AddComponent(components[i]);
                }
            }
        }
    }
}