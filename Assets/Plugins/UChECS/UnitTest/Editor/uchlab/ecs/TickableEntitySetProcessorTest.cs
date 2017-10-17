using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using uchlab.ecs.processor;
using uchlab.ecs.test;

namespace uchlab.ecs {
    [TestFixture]
    public class TickableEntitySetProcessorTest {

        private EntitySet<String, int> entityList;
        private IEntityRepository repository;
        private TickableProcessorSpy processor;

        private float nextDeltaSeconds;
        private float deltaSecondsIncrement;

        [SetUp]
        public void Setup() {
            entityList = new EntitySet<string, int>();

            repository = Substitute.For<IEntityRepository>();
            repository.GetEntitySet<string, int>().Returns(entityList);

            nextDeltaSeconds = 0f;
            deltaSecondsIncrement = 0f;
        }

        private float deltaSecondsProvider() {
            return nextDeltaSeconds;
        }

        private float incrementalDeltaSecondsProvider() {
            var value = nextDeltaSeconds;
            nextDeltaSeconds += deltaSecondsIncrement;
            return value;
        }

        [Test]
        public void On_Tick_process_all_nodes() {

            AddEntitiesToListEntityList(10);
            processor = new TickableProcessorSpy(repository, deltaSecondsProvider);
            processor.Tick();

            var i = 0;
            entityList.ForEach((cs) => {
                Assert.That(cs.component1, Is.EqualTo(processor.ProcessedNodes[i].myString));
                Assert.That(cs.component2, Is.EqualTo(processor.ProcessedNodes[i].myInt));
                i++;
            });

            Assert.That(processor.ProcessedNodes.Count, Is.EqualTo(10));
        }

        [Test]
        public void When_node_added_is_processed_on_next_tick() {

            AddEntitiesToListEntityList(9);
            processor = new TickableProcessorSpy(repository, deltaSecondsProvider);
            processor.Tick();
            Assert.That(processor.ProcessedNodes.Count, Is.EqualTo(9));

            processor.ClearTest();
            AddEntitiesToListEntityList(2);
            processor.Tick();
            Assert.That(processor.ProcessedNodes.Count, Is.EqualTo(11));
        }

        [Test]
        public void When_node_removed_is_not_processed_on_next_tick() {

            AddEntitiesToListEntityList(9);
            processor = new TickableProcessorSpy(repository, deltaSecondsProvider);
            processor.Tick();
            Assert.That(processor.ProcessedNodes.Count, Is.EqualTo(9));

            processor.ClearTest();
            entityList.RemoveEntity(entityList[0].entity);
            entityList.RemoveEntity(entityList[0].entity);
            processor.Tick();

            Assert.That(processor.ProcessedNodes.Count, Is.EqualTo(7));
        }

        [Test]
        public void On_Tick_use_provided_delta_seconds() {

            AddEntitiesToListEntityList(23);
            nextDeltaSeconds = 5f;

            processor = new TickableProcessorSpy(repository, deltaSecondsProvider);
            processor.Tick();

            Assert.That(processor.AccumulatedDelta, Is.EqualTo(23 * nextDeltaSeconds));
        }

        [Test]
        public void On_Tick_calls_delta_seconds_provider_x_entity() {

            AddEntitiesToListEntityList(23);
            nextDeltaSeconds = 5f;
            deltaSecondsIncrement = 3f;

            float expectedAcummulatedSeconds = nextDeltaSeconds;
            for (var i = 1; i < 23; i++) {
                expectedAcummulatedSeconds += nextDeltaSeconds + deltaSecondsIncrement * i;
            }

            processor = new TickableProcessorSpy(repository, incrementalDeltaSecondsProvider);
            processor.Tick();

            Assert.That(processor.AccumulatedDelta, Is.EqualTo(expectedAcummulatedSeconds));

        }

        private void AddEntitiesToListEntityList(int entitiesCount) {
            for (var i = 0; i < entitiesCount; i++) {
                entityList.AddIfValid(Helper.CreateEntitySubstitute());
            }
        }

        class TickableProcessorSpy : TickableEntitySetProcessor<string, int> {

            public readonly List<TestSet> ProcessedNodes = new List<TestSet>();

            public float AccumulatedDelta { get; private set; }

            public float LastDelta { get; private set; }


            public TickableProcessorSpy(IEntityRepository repository, Func<float> DeltaSecondsProvider) : base(repository, DeltaSecondsProvider) {
            }


            protected override void ProcessEntity(Entity entity, string myString, int myInt) {
                ProcessedNodes.Add(new TestSet(myString, myInt));
                LastDelta = DeltaSecondsProvider();
                AccumulatedDelta += LastDelta;
            }

            public void ClearTest() {
                ProcessedNodes.Clear();
                AccumulatedDelta = 0f;
                LastDelta = 0f;
            }
        }
    }
}