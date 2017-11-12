using UnityEngine;
using Zenject;

namespace uchlab.ecs.zenject {
    public abstract class RegisterComponentBehaviour : MonoBehaviour {

        protected IEntityRepository repository;

        private bool AttachedToEntity {
            get {
                return repository.IsRegistered(gameObject);
            }
        }

        [Inject]
        public void Init(IEntityRepository repository) {
            this.repository = repository;

            if (enabled) {
                registerComponents();
            }
        }

        protected abstract void registerComponents();

        protected abstract void unregisterComponents();
        protected void AddComponent<T>(){
            repository.AddComponent<T>(gameObject,GetComponent<T>());
        }
        protected void AddComponent<T>(T component){
            repository.AddComponent<T>(gameObject,component);
        }
        protected void RemoveComponent<T>(){
            RemoveComponent(GetComponent<T>());
        }
       protected void RemoveComponent(object component){
            repository.RemoveComponent(gameObject,component);
        }

        void OnEnable() {
            if (repository != null && !AttachedToEntity)
            {
                registerComponents();
            }
        }

        void OnDisable() {
            if (repository != null && AttachedToEntity)
            {
                unregisterComponents();
            }
        }
    }

    public class RegisterComponentBehaviour<T> : RegisterComponentBehaviour{

        [LabelGeneric]
        public T component;

        protected override void registerComponents() {
            repository.AddComponent<T>(gameObject,(object)component);
        }

        protected override void unregisterComponents() {
            repository.RemoveComponent(gameObject,(object)component);
        }
    }

    public class RegisterComponentBehaviour<T1,T2> : RegisterComponentBehaviour<T1>{

        [LabelGeneric]
        public T2 component2;

        protected override void registerComponents() {
            base.registerComponents();
            repository.AddComponent<T2>(gameObject,(object)component2);
        }

        protected override void unregisterComponents() {
            base.registerComponents();
            repository.RemoveComponent(gameObject,(object)component2);
        }
    }

    public class RegisterComponentBehaviour<T1,T2,T3> : RegisterComponentBehaviour<T1,T2>{

        [LabelGeneric]
        public T3 component3;

        protected override void registerComponents() {
            base.registerComponents();
            repository.AddComponent<T3>(gameObject,(object)component3);
        }

        protected override void unregisterComponents() {
            base.registerComponents();
            repository.RemoveComponent(gameObject,(object)component3);
        }
    }

    public class RegisterComponentBehaviour<T1,T2,T3,T4> : RegisterComponentBehaviour<T1,T2,T3>{

        [LabelGeneric]
        public T4 component4;

        protected override void registerComponents() {
            base.registerComponents();
            repository.AddComponent<T4>(gameObject,(object)component4);
        }

        protected override void unregisterComponents() {
            base.registerComponents();
            repository.RemoveComponent(gameObject,(object)component4);
        }
    }

    public class RegisterComponentBehaviour<T1,T2,T3,T4,T5> : RegisterComponentBehaviour<T1,T2,T3,T4>{

        [LabelGeneric]
        public T5 component5;

        protected override void registerComponents() {
            base.registerComponents();
            repository.AddComponent<T5>(gameObject,(object)component5);
        }

        protected override void unregisterComponents() {
            base.registerComponents();
            repository.RemoveComponent(gameObject,(object)component5);
        }
    }
}