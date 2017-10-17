using System;
using System.Collections.Generic;
using NUnit.Framework;
using uchlab.ecs.test;
using UnityEngine;

namespace uchlab.ecs {
    [TestFixture]
    public class EntityTest {

        private List<object> components = new List<object>() {1, 1f, "testString", new object(), Vector2.zero, Vector3.zero, new List<string>() };

        private GameObject gameObject;
        private Entity entity;

        [SetUp]
        public void Setup() {

            gameObject = new GameObject();
            entity = CreateEntity();
        }

        private Entity CreateEntity() {
            return new EntityDisposableInEditor(gameObject, EmptyDelegate, EmptyDelegate);
        }

        private Entity CreateEntity(EntityComponentChange componentAddedDelegate, EntityComponentChange componentRemovedDelegate) {
            return new EntityDisposableInEditor(gameObject, componentAddedDelegate, componentRemovedDelegate);
        }

        private void EmptyDelegate(Entity entity, object component, Type type) {
            return;
        }

        [Test]
        public void Entity_holds_reference_to_GameObject() {
            Assert.That(entity.gameObject, Is.EqualTo(gameObject));
        }

        [Test]
        public void Entity_is_empty_if_no_components_added() {
            Assert.That(entity.IsEmpty);
        }

        [Test]
        public void Entity_is_not_empty_when_component_added() {

            var component = new object();
            entity.AddComponent(component);

            Assert.That(entity.IsEmpty, Is.False);
        }

        [Test]
        public void If_all_components_removed_entity_is_empty() {

            components.ForEach((c) => {
                entity.AddComponent(c);
                Assert.That(entity.IsEmpty, Is.False);
            });

            components.ForEach((c) => entity.RemoveComponent(c));

            Assert.That(entity.IsEmpty);
        }

        [Test]
        public void If_Component_can_be_added_AddComponent_returns_true() {

            var component = new object();
            var returningValue = entity.AddComponent(component);

            Assert.That(returningValue, Is.True);
        }

        [Test]
        public void HasComponentOfType_returns_true_if_component_of_type_added() {

            var component = new object();
            entity.AddComponent(component);

            Assert.That(entity.HasComponentOfType(typeof(object)));
        }

        [Test]
        public void HasComponentOfType_returns_false_if_no_component_of_type_added() {

            Assert.That(entity.HasComponentOfType(typeof(object)), Is.False);
        }

        [Test]
        public void If_try_to_add_2_components_of_the_same_type_AddComponent_returns_false() {

            var component1 = new object();
            var component2 = new object();
            entity.AddComponent(component1);
            var returningValue = entity.AddComponent(component2);

            Assert.That(returningValue, Is.False);
        }

        [Test]
        public void When_component_added_ComponentAdded_event_is_dispatched() {

            bool eventCalled = false;
            entity = CreateEntity((e, c, t) => eventCalled = true,EmptyDelegate);
            entity.AddComponent(new object());

            Assert.That(eventCalled);
        }

        [Test]
        public void If_try_to_add_2_components_of_the_same_type_ComponentAdded_event_is_not_dispatched() {

            int eventCallsCounter = 0;
            entity = CreateEntity((e, c, t) => eventCallsCounter++,EmptyDelegate);

            var component1 = new object();
            var component2 = new object();

            entity.AddComponent(component1);
            entity.AddComponent(component2);

            Assert.That(eventCallsCounter, Is.EqualTo(1));
        }

        [Test]
        public void When_component_added_ComponentAdded_event_is_dispatched_with_expected_parameters() {

            Entity eventEntity = null;
            object eventComponent = null;
            Type eventType = null;

            entity = CreateEntity((e, c, t) => {
                eventEntity = e;
                eventComponent = c;
                eventType = t;
            }, EmptyDelegate);

            String expectedComponent = "TestComponent";
            entity.AddComponent(expectedComponent);

            Assert.That(entity, Is.EqualTo(eventEntity));
            Assert.That(expectedComponent, Is.EqualTo(eventComponent));
            Assert.That(expectedComponent.GetType(), Is.EqualTo(eventType));
        }

        [Test]
        public void If_Component_can_be_removed_RemoveComponent_returns_true() {

            var component = new object();
            entity.AddComponent(component);
            var returningValue = entity.RemoveComponent(component);

            Assert.That(returningValue, Is.True);
        }

        [Test]
        public void If_try_to_remove_component_that_was_not_added_RemoveComponent_returns_false() {

            var component = new object();
            var returningValue = entity.RemoveComponent(component);

            Assert.That(returningValue, Is.False);
        }

        [Test]
        public void If_try_to_remove_component_twice_RemoveComponent_returns_false() {

            var component = new object();
            entity.AddComponent(component);
            entity.RemoveComponent(component);
            var returningValue = entity.RemoveComponent(component);

            Assert.That(returningValue, Is.False);
        }

        [Test]
        public void When_component_removed_ComponentRemoved_event_is_dispatched() {

            var component = new object();
            bool eventCalled = false;

            entity = CreateEntity(EmptyDelegate,(e, c, t) => eventCalled = true);
            entity.AddComponent(component);
            entity.RemoveComponent(component);

            Assert.That(eventCalled);
        }

        [Test]
        public void If_try_to_remove_a_component_twice_ComponentRemoved_event_is_dispatched_once() {

            int eventCallsCounter = 0;
            entity = CreateEntity(EmptyDelegate,(e, c, t) => eventCallsCounter++);

            var component = new object();

            entity.AddComponent(component);
            entity.RemoveComponent(component);
            entity.RemoveComponent(component);

            Assert.That(eventCallsCounter, Is.EqualTo(1));
        }

        [Test]
        public void When_component_removed_ComponentRemoved_event_is_dispatched_with_expected_parameters() {

            Entity eventEntity = null;
            object eventComponent = null;
            Type eventType = null;

            entity = CreateEntity(EmptyDelegate, (e, c, t) => {
                eventEntity = e;
                eventComponent = c;
                eventType = t;
            });

            String expectedComponent = "TestComponent";
            entity.AddComponent(expectedComponent);
            entity.RemoveComponent(expectedComponent);

            Assert.That(entity, Is.EqualTo(eventEntity));
            Assert.That(expectedComponent, Is.EqualTo(eventComponent));
            Assert.That(expectedComponent.GetType(), Is.EqualTo(eventType));
        }

        [Test]
        public void GetComponent_return_instance_of_added_component() {
            var component = new object();

            entity.AddComponent(component);
            Assert.That(component, Is.EqualTo(entity.GetComponent(component.GetType())));
        }

        [Test]
        public void ComponentTypes_returns_Iterable_withCompoenet_Types() {
            var expectedComponentTypes = new HashSet<Type>();

            components.ForEach((c) => entity.AddComponent(c));
            components.ForEach((c) => expectedComponentTypes.Add(c.GetType()));

            Assert.That(expectedComponentTypes.SetEquals(entity.ComponentTypes));
        }

        [Test]
        public void Dispose_destroy_gameObject() {
            entity.Dispose();
            Assert.That(gameObject == null);
        }

        [Test]
        public void Dispose_remove_all_components() {

            entity.AddComponent(Helper.STRING_COMPONENT);
            entity.AddComponent(Helper.INT_COMPONENT);

            entity.Dispose();

            Assert.That(entity.HasComponentOfType(Helper.STRING_COMPONENT.GetType()), Is.False);
            Assert.That(entity.HasComponentOfType(Helper.INT_COMPONENT.GetType()), Is.False);
        }
    }
}