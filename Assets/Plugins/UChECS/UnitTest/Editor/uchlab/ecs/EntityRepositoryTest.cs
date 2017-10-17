using NUnit.Framework;
using uchlab.ecs.test;
using UnityEngine;

namespace uchlab.ecs {
    [TestFixture]
    public class EntityRepositoryTest {

        private EntityRepository repository;

        [SetUp]
        public void Setup() {
            repository = new EntityRepository();
        }

        [Test]
        public void IsRegistered_returns_false_for_gameObject_without_components() {
            var gameObject = new GameObject();
            Assert.That(repository.IsRegistered(gameObject),Is.False);
        }

        [Test]
        public void IsRegistered_returns_true_for_gameObject_with_components() {
            var gameObject = new GameObject();
            repository.AddComponent(gameObject, Helper.STRING_COMPONENT);
            Assert.That(repository.IsRegistered(gameObject));
        }

        [Test]
        public void When_component_added_return_matching_entity() {
            var gameObject = new GameObject();

            var entity = repository.AddComponent(gameObject, Helper.STRING_COMPONENT);

            Assert.That(entity.gameObject,Is.EqualTo(gameObject));
            Assert.That(entity.GetComponent(typeof(string)),Is.EqualTo(Helper.STRING_COMPONENT));
        }

        [Test]
        public void GetEntityFor_returns_correct_entity() {
            var gameObject = new GameObject();

            var expectedEntity = repository.AddComponent(gameObject, Helper.STRING_COMPONENT);

            Assert.That(repository.GetEntityFor(gameObject),Is.EqualTo(expectedEntity));
        }

        [Test]
        public void When_component_added_to_existing_entity_return_same_instance() {
            var gameObject = new GameObject();

            var entity1 = repository.AddComponent(gameObject, Helper.STRING_COMPONENT);
            var entity2 = repository.AddComponent(gameObject, Helper.INT_COMPONENT);

            Assert.That(entity1,Is.EqualTo(entity2));
            Assert.That(entity1.GetComponent(typeof(int)),Is.EqualTo(Helper.INT_COMPONENT));
            Assert.That(entity2.GetComponent(typeof(string)),Is.EqualTo(Helper.STRING_COMPONENT));
        }


        [Test]
        public void CreateGroupList_return_EntityList_with_same_type() {
            Assert.That(repository.GetEntitySet<int>() is EntitySet<int>);
            Assert.That(repository.GetEntitySet<int,float>() is EntitySet<int,float>);
            Assert.That(repository.GetEntitySet<int,float,string>() is EntitySet<int,float,string>);
            Assert.That(repository.GetEntitySet<int,float,string,object>() is EntitySet<int,float,string,object>);
            Assert.That(repository.GetEntitySet<int,float,string,object,Entity>() is EntitySet<int,float,string,object,Entity>);

        }

        [Test]
        public void When_matching_components_added_to_GameObject_CreateGroupList_return_entity() {
            var gameObject = new GameObject();

            repository.AddComponent(gameObject, "string component");
            repository.AddComponent(gameObject, 1);

            var entityList = repository.GetEntitySet<string,int>();
            Assert.That(entityList.Count,Is.EqualTo(1));
            Assert.That(entityList[0].Entity.gameObject,Is.EqualTo(gameObject));
        }

        [Test]
        public void When_matching_components_added_to_GameObject_entity_is_added_to_list() {

            var entityList = repository.GetEntitySet<string,int>();
            var gameObject = new GameObject();

            repository.AddComponent(gameObject, Helper.STRING_COMPONENT);
            Assert.That(entityList.Count,Is.EqualTo(0));

            repository.AddComponent(gameObject, Helper.INT_COMPONENT);
            Assert.That(entityList.Count,Is.EqualTo(1));
        }

        [Test]
        public void When_matching_components_removed_from_GameObject_entity_is_removed_from_list() {

            var entityList = repository.GetEntitySet<string,int>();
            var gameObject = new GameObject();

            repository.AddComponent(gameObject, Helper.STRING_COMPONENT);
            repository.AddComponent(gameObject, Helper.INT_COMPONENT);
            Assert.That(entityList.Count,Is.EqualTo(1));

            repository.RemoveComponent(gameObject, Helper.STRING_COMPONENT);
            Assert.That(entityList.Count,Is.EqualTo(0));
            Assert.That(repository.Count,Is.EqualTo(1));
        }

        [Test]
        public void DestroyEntity_destroys_entity() {

            var entityList = repository.GetEntitySet<string,int>();
            var gameObject = new GameObject();

            repository.EntityFactoryMethod = Helper.EntityFactory;

            repository.AddComponent(gameObject, Helper.STRING_COMPONENT);
            repository.AddComponent(gameObject, Helper.INT_COMPONENT);
            Assert.That(entityList.Count,Is.EqualTo(1));

            repository.DestroyEntity(entityList[0].Entity);

            Assert.That(entityList.Count,Is.EqualTo(0));
            Assert.That(repository.Count,Is.EqualTo(0));
            Assert.That(gameObject == null);
        }

        [Test]
        public void DestroyEntity_destroys_entity_associated_to_gameObject() {

            var entityList = repository.GetEntitySet<string,int>();
            var gameObject = new GameObject();

            repository.EntityFactoryMethod = Helper.EntityFactory;

            repository.AddComponent(gameObject, Helper.STRING_COMPONENT);
            repository.AddComponent(gameObject, Helper.INT_COMPONENT);
            Assert.That(entityList.Count,Is.EqualTo(1));

            repository.DestroyEntity(gameObject);

            Assert.That(entityList.Count,Is.EqualTo(0));
            Assert.That(repository.Count,Is.EqualTo(0));
            Assert.That(gameObject == null);
        }
    }
}