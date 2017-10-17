using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using uchlab.ecs.processor;
using uchlab.ecs.test;

namespace uchlab.ecs {
    [TestFixture]
    public class EntitySetProcessorTest {

        private EntitySet<string, int> entityList;
        private IEntityRepository repository;

        private EntityProcessorSpy processor;

        [SetUp]
        public void Setup() {
            entityList = new EntitySet<string, int>();

            repository = Substitute.For<IEntityRepository>();
            repository.GetEntitySet<string, int>().Returns(entityList);
        }

        [Test]
        public void OnNodeAdded_called_for_each_entity_on_list_on_instantiation() {

            AddEntitiesToListEntityList(10);
            processor = new EntityProcessorSpy(repository);

            Assert.That(processor.AddedNodes.Count, Is.EqualTo(10));
        }

        [Test]
        public void When_entity_added_to_list_OnNodeAdded_is_called() {
            processor = new EntityProcessorSpy(repository);
            AddEntitiesToListEntityList(5);

            Assert.That(processor.AddedNodes.Count, Is.EqualTo(5));
        }

        [Test]
        public void When_entity_remove_to_list_OnNodeRemoved_is_called() {

            processor = new EntityProcessorSpy(repository);
            AddEntitiesToListEntityList(5);
            while (entityList.Count > 0) {
                entityList.RemoveEntity(entityList[0].entity);
            }

            Assert.That(processor.RemovedNodes.Count, Is.EqualTo(5));
        }

        private void AddEntitiesToListEntityList(int entitiesCount) {
            for (var i = 0; i < entitiesCount; i++) {
                entityList.AddIfValid(Helper.CreateEntitySubstitute());
            }
        }

        class EntityProcessorSpy : EntitySetProcessor<string, int> {

            public readonly List<TestSet> AddedNodes = new List<TestSet>();
            public readonly List<TestSet> RemovedNodes = new List<TestSet>();

            public EntityProcessorSpy(IEntityRepository repository) : base(repository) {
            }

            protected override void OnNodeAdded(Entity entity, string myString, int myInt) {
                AddedNodes.Add(new TestSet(myString, myInt));
            }

            protected override void OnNodeRemoved(Entity entity, string myString, int myInt) {
                RemovedNodes.Add(new TestSet(myString, myInt));
            }
        }

    }
}