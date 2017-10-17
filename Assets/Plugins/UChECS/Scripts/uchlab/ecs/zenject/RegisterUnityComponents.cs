using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace uchlab.ecs.zenject {
    public class RegisterUnityComponents : RegisterComponentBehaviour {

        public List<Component> UnityComponents;

        override protected void registerComponents(){
            var count = UnityComponents.Count;
            for(var i=0;i<count;i++){
                repository.AddComponent(gameObject,UnityComponents[i]);
            }
        }

        override protected void unregisterComponents(){
            var count = UnityComponents.Count;
            for(var i=0;i<count;i++){
                repository.RemoveComponent(gameObject,UnityComponents[i]);
            }
        }
    }
}