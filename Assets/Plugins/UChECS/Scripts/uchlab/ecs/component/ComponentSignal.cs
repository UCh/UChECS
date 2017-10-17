using System;

namespace uchlab.ecs {
    public class BaseComponentSignal {

        protected IRegistrableComponent component;

        public BaseComponentSignal(IRegistrableComponent component) {
            this.component = component;
        }
    }

    public class ComponentSignal : BaseComponentSignal {

        public event Action<Entity> Triggered;


        public ComponentSignal(IRegistrableComponent component) : base(component) {
        }

        public void Trigger() {
            if (Triggered != null && component.IsAttachedToEntity) {
                Triggered(component.Entity);
            }
        }
    }

    public class ComponentSignal<T> : BaseComponentSignal {

        public event Action<Entity, T> Triggered;


        public ComponentSignal(IRegistrableComponent component) : base(component) {
        }

        public void Trigger(T param) {
            if (Triggered != null && component.IsAttachedToEntity) {
                Triggered(component.Entity, param);
            }
        }
    }

    public class ComponentSignal<T1,T2> : BaseComponentSignal {

        public event Action<Entity, T1, T2> Triggered;

        public ComponentSignal(IRegistrableComponent component) : base(component) {
        }

        public void Trigger(T1 param1, T2 param2) {
            if (Triggered != null && component.IsAttachedToEntity) {
                Triggered(component.Entity, param1, param2);
            }
        }
    }

    public class ComponentSignal<T1,T2, T3> : BaseComponentSignal {

        public event Action<Entity, T1, T2, T3> Triggered;

        public ComponentSignal(IRegistrableComponent component) : base(component) {
        }

        public void Trigger(T1 param1, T2 param2, T3 param3) {
            if (Triggered != null && component.IsAttachedToEntity) {
                Triggered(component.Entity, param1, param2, param3);
            }
        }
    }
}