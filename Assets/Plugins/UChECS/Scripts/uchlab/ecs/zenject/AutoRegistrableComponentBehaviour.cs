namespace uchlab.ecs.zenject {
    public class AutoRegistrableComponentBehaviour : RegisterComponentBehaviour {

        protected override void registerComponents() {
            repository.AddComponent(gameObject,this);
        }

        protected override void unregisterComponents() {
            repository.RemoveComponent(gameObject,this);
        }
    }
}