using System.Collections.Generic;
using NUnit.Framework;
using uchlab.ecs.test;

namespace uchlab.ecs {
    [TestFixture]
    public class EntitySetTest {

        private EntitySet<string, int> entitySet;

        [SetUp]
        public void Setup() {
            entitySet = new EntitySet<string, int>();
        }

        [Test]
        public void AddIfValid_Adds_ComponentSetWithEntity() {
            var entity = Helper.CreateEntitySubstitute();
            var isAdded = entitySet.AddIfValid(entity);

            Assert.That(isAdded);
            Assert.That(entitySet.Count, Is.EqualTo(1));

            var group = entitySet[0];
            AssertEntityGroup(entity, group);
        }

        [Test]
        public void AddIfValid_return_false_if_entity_is_not_valid() {
            var entity = Helper.CreateEntitySubstitute(false);
            var isAdded = entitySet.AddIfValid(entity);

            Assert.That(isAdded, Is.False);
            Assert.That(entitySet.Count, Is.EqualTo(0));
        }

        [Test]
        public void Adding_multiple_valid_and_invalid_entities_only_adds_valid_ones_in_order() {
            bool odd = true;
            Entity entity;
            var validEntities = new List<Entity>();


            for (var i = 0; i < 20; i++) {
                entity = Helper.CreateEntitySubstitute(odd);
                if (odd) {
                    validEntities.Add(entity);
                }

                entitySet.AddIfValid(entity);
                odd = !odd;
            }

            Assert.That(entitySet.Count, Is.EqualTo(validEntities.Count));

            for (var i = 0; i < validEntities.Count; i++) {
                AssertEntityGroup(validEntities[i], entitySet[i]);
            }
        }

        [Test]
        public void ForEach_is_called_for_each_entity_in_order() {

            bool odd = true;
            Entity entity;
            var validEntities = new List<Entity>();

            for (var i = 0; i < 20; i++) {
                entity = Helper.CreateEntitySubstitute(odd);
                if (odd) {
                    validEntities.Add(entity);
                }

                entitySet.AddIfValid(entity);
                odd = !odd;
            }

            int index = 0;
            entitySet.ForEach((c) => {
                AssertEntityGroup(validEntities[index], c);
                index++;
            });
        }

        [Test]
        public void When_entity_added_NodeAdded_is_dispatched() {
            var receivedSet = new ComponentGroup<string, int>();
            var entity = Helper.CreateEntitySubstitute();

            entitySet.NodeAdded += (s) => receivedSet = s;
            entitySet.AddIfValid(entity);

            AssertEntityGroup(entity, receivedSet);
        }

        [Test]
        public void RemoveEntity_remove_it_from_list() {
            var entity = Helper.CreateEntitySubstitute();

            var isAdded = entitySet.AddIfValid(entity);
            Assert.That(isAdded);
            Assert.That(entitySet.Count, Is.EqualTo(1));

            entitySet.RemoveEntity(entity);

            Assert.That(entitySet.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveEntity_dispatch_NodeRemoved() {
            var receivedSet = new ComponentGroup<string, int>();
            var entity = Helper.CreateEntitySubstitute();

            entitySet.NodeRemoved += (s) => receivedSet = s;
            entitySet.AddIfValid(entity);

            entitySet.RemoveEntity(entity);

            AssertEntityGroup(entity, receivedSet);
        }

        [Test]
        public void ContainComponentOfType_return_true_for_valid_type() {
            Assert.That(entitySet.ContainComponentOfType(typeof(string)));
            Assert.That(entitySet.ContainComponentOfType(typeof(int)));
        }

        [Test]
        public void ContainComponentOfType_return_false_for_invalid_type() {
            Assert.That(entitySet.ContainComponentOfType(typeof(float)), Is.False);
            Assert.That(entitySet.ContainComponentOfType(typeof(object)), Is.False);
        }


        public void AssertEntityGroup(Entity entity, ComponentGroup<string, int> group) {
            Assert.That(group.Entity, Is.EqualTo(entity));
            Assert.That(group.component1, Is.EqualTo(Helper.STRING_COMPONENT));
            Assert.That(group.component2, Is.EqualTo(Helper.INT_COMPONENT));
        }

    }
}