using UnityEngine;

namespace uchlab.ecs {
    public interface IEntityRepository {

        int Count { get; }

        bool IsRegistered(GameObject gameObject);

        Entity GetEntityFor(GameObject gameObject);

        Entity AddComponent(GameObject gameObject, object component);

        Entity AddComponent<T>(GameObject gameObject, object component);

        void RemoveComponent(GameObject gameObject, object component);

        EntitySet<T> GetEntitySet<T>();
        EntitySet<T1,T2> GetEntitySet<T1,T2>();
        EntitySet<T1,T2,T3> GetEntitySet<T1,T2,T3>();
        EntitySet<T1,T2,T3,T4> GetEntitySet<T1,T2,T3,T4>();
        EntitySet<T1,T2,T3,T4,T5> GetEntitySet<T1,T2,T3,T4,T5>();

        void DestroyEntity(Entity entity);

        void DestroyEntity(GameObject gameObject);
    }
}