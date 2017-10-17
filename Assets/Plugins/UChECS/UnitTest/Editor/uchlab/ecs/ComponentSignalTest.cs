using NSubstitute;
using NUnit.Framework;
using uchlab.ecs.test;

namespace uchlab.ecs {
    [TestFixture]
    public class ComponentSignalTest {

        private IRegistrableComponent component;
        private ComponentSignal signal;

        [SetUp]
        public void SetUp() {
            component = Substitute.For<IRegistrableComponent>();
            signal = new ComponentSignal(component);
        }

        [Test]
        public void Trigger_dispatch_event_Triggered_if_component_attached_to_entity() {
            var triggered = false;
            component.IsAttachedToEntity.Returns(true);
            signal.Triggered += (e) => triggered = true;
            signal.Trigger();

            Assert.That(triggered);
        }

        [Test]
        public void Triggered_event_use_component_entity() {
            var expectedEntity = Helper.CreateEntitySubstitute();
            Entity entity = null;
            component.IsAttachedToEntity.Returns(true);
            component.Entity.Returns(expectedEntity);
            signal.Triggered += (e) => entity = e;
            signal.Trigger();

            Assert.That(entity, Is.EqualTo(expectedEntity));
        }

        [Test]
        public void Trigger_do_not_dispatch__if_component_not_attached_to_entity() {
            var triggered = false;
            component.IsAttachedToEntity.Returns(false);
            signal.Triggered += (e) => triggered = true;
            signal.Trigger();

            Assert.That(triggered, Is.False);
        }
    }
}