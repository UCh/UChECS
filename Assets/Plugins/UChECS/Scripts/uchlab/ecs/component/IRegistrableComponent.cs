namespace uchlab.ecs {

    public interface IRegistrableComponent {

        Entity Entity { get;}

        bool IsAttachedToEntity { get; }

        void AttachedTo(Entity entity);

        void Detach();
    }
}