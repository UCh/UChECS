using System;
using System.Collections.Generic;
using uchlab.ecs;

namespace uchlab.ecs.commands
{
    public class BaseEntityCommand
    {
        public readonly string name;
        private readonly HashSet<Type> _requiredComponents;

        public BaseEntityCommand(string name = "")
        {
            _requiredComponents = new HashSet<Type>();
            this.name = name;
        }

        protected BaseEntityCommand RequiresComponent<T>(){
            _requiredComponents.Add(typeof(T));
            return this;
        }

        

        public bool CanExecuteOn(Entity entity)
        {
            return _requiredComponents.IsSubsetOf(entity.ComponentTypes);
        }
    }

    public abstract class EntityCommand : BaseEntityCommand
    {
        public abstract void Execute(Entity entity);
    }

    public abstract class EntityCommand<T> : BaseEntityCommand
    {
        public abstract void Execute(Entity entity, T param);
    }
    public abstract class EntityCommand<T1,T2> : BaseEntityCommand
    {
        public abstract void Execute(Entity entity, T1 param1, T2 param2);
    }
    public abstract class EntityCommand<T1,T2,T3> : BaseEntityCommand
    {
        public abstract void Execute(Entity entity, T1 param1, T2 param2, T3 param3);
    }
    public abstract class EntityCommand<T1,T2,T3,T4> : BaseEntityCommand
    {
        public abstract void Execute(Entity entity, T1 param1, T2 param2, T3 param3, T4 param4);
    }
    public abstract class EntityCommand<T1,T2,T3,T4,T5> : BaseEntityCommand
    {
        public abstract void Execute(Entity entity, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);
    }




}