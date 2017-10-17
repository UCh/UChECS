namespace uchlab.ecs.component {
    public abstract class BaseComponentGroupFactory<T> {
        public abstract T Create(Entity entity);
    }

    public class ComponentGroupFactory<T> : BaseComponentGroupFactory<ComponentGroup<T>>{

        public override ComponentGroup<T> Create(Entity entity) {
            return new ComponentGroup<T>(entity,entity.GetComponent<T>());
        }
    }
}