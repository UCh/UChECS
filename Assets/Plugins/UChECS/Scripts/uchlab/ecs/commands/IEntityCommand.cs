using System;
using System.Collections.Generic;
using uchlab.ecs;

namespace uchlab.ecs.commands
{
    public interface IEntityCommand
    {
        string Name { get; }
        bool CanExecuteOn(Entity entity);
        bool IsInmediate {get;}

        void Start(Entity entity);
        void Cancel();
        void Execute(Entity entity);
    }
}