using System;
using NUnit.Framework;
using UnityEngine;

namespace uchlab.ecs.test {
    public class Helper {
        public static string STRING_COMPONENT = "MyString";
        public static int INT_COMPONENT = 45;

        public static Entity CreateEntitySubstitute() {
            return CreateEntitySubstitute(true);
        }
        public static Entity CreateEntitySubstitute(bool isValid) {
            var entity = new Entity(null,dummyEntityComponentChange,dummyEntityComponentChange);

            entity.AddComponent(STRING_COMPONENT);

            if (isValid) {
                entity.AddComponent(INT_COMPONENT);
            }

            return entity;
        }

        public static Entity CreateTestEntity(params System.Object[] components) {
            var entity = new Entity(null,dummyEntityComponentChange,dummyEntityComponentChange);

            foreach (var component in components) {
                entity.AddComponent(component);

                if (component is IRegistrableComponent) {
                    var registrable = (IRegistrableComponent)component;
                    registrable.AttachedTo(entity);
                }
            }

            return entity;
        }

        private static void dummyEntityComponentChange(Entity entity, object component, Type type){
            return;
        }

        public static TestComponentSet CreateComponentSubstitute() {
            return new TestComponentSet(CreateEntitySubstitute(), STRING_COMPONENT, INT_COMPONENT);
        }


        public static void AssertThatComponentSetMatchEntity(Entity entity, TestComponentSet componentSet) {
            Assert.That(componentSet.entity, Is.EqualTo(entity));
            Assert.That(componentSet.StringComponent, Is.EqualTo(STRING_COMPONENT));
            Assert.That(componentSet.IntComponent, Is.EqualTo(INT_COMPONENT));
        }

        public static Entity EntityFactory(GameObject gameObject) {
            return new EntityDisposableInEditor(gameObject, EmptyDelegate, EmptyDelegate);
        }

        public static Entity EntityFactory(GameObject gameObject, EntityComponentChange componentAddedDelegate, EntityComponentChange componentRemovedDelegate) {
            return new EntityDisposableInEditor(gameObject, componentAddedDelegate, componentRemovedDelegate);
        }

        public static void EmptyDelegate(Entity entity, object component, Type type) {
            return;
        }

    }

    public class EntityDisposableInEditor : Entity {
        public EntityDisposableInEditor(UnityEngine.GameObject entity, uchlab.ecs.EntityComponentChange componentAddedDelegate, uchlab.ecs.EntityComponentChange componentRemovedDelegate) : base(entity, componentAddedDelegate, componentRemovedDelegate) {
        }

        //In edit mode is not allowed to use GameObject.Destroy
        override protected void DestroyGameObject() {
            GameObject.DestroyImmediate(gameObject);
        }
    }

    public class TestComponentSet : ComponentSet {


        public TestComponentSet() {
        }


        public TestComponentSet(Entity entity, string stringValue, int intValue) : base(entity) {
            StringComponent = stringValue;
            IntComponent = intValue;
        }

        public readonly string StringComponent;
        public readonly int IntComponent;
    }

    public class TestSet {
        public readonly string myString;
        public readonly int myInt;


        public TestSet(string myString, int myInt) {
            this.myString = myString;
            this.myInt = myInt;
        }
    }
}