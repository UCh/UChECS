using System;
using System.Collections.Generic;
using UnityEngine;

namespace uchlab.ecs {
    public interface IEntity {

        GameObject gameObject { get; }

        IEnumerable<Type> ComponentTypes { get; }

        bool IsEmpty { get; }

        bool AddComponent<T>(object component);

        bool AddComponent(object component);

        bool RemoveComponent(object component);

        bool HasComponentOfType(Type type);

        bool HasComponentOfType<T>();

        object GetComponent(Type type);

        T GetComponent<T>();

        bool TryGetComponent<T>(out T component);

        void Dispose();

    }
}