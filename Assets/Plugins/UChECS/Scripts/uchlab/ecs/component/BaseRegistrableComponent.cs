namespace uchlab.ecs {
    public class BaseRegistrableComponent : IRegistrableComponent {
        public Entity Entity {
            get; private set;
        }

        public virtual void AttachedTo(Entity entity) {
            Entity = entity;
        }

        public virtual void Detach() {
            Entity = null;
        }

        public bool IsAttachedToEntity {
            get {
                return Entity != null;
            }
        }

    }
}