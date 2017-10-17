namespace uchlab.ecs {
    public class BaseRegistrableComponent : IRegistrableComponent {

        private Entity entity;

        public Entity Entity {
            get {
                return entity;
            }
        }

        public void AttachedTo(Entity entity) {
            this.entity = entity;
        }

        public void Detach() {
            entity = null;
        }

        public bool IsAttachedToEntity {
            get {
                return entity != null;
            }
        }

    }
}