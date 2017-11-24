using System;
using System.Collections.Generic;
using uchlab.ecs;
using UniRx;

namespace uchlab.ecs.commands
{
    public interface IEntityCommand
    {
        IObservable<IEntityCommand> Executed { get; }
        string Name { get; }
        bool CanExecuteOn(Entity entity);

        void Execute(Entity entity);
    }

    public interface IAsyncEntityCommand : IEntityCommand
    {
        IObservable<IEntityCommand> Started { get; }
    }

    public interface ICancellableEntityCommand : IAsyncEntityCommand
    {
        IObservable<IEntityCommand> Cancelled { get; }
        void Cancel();
    }

}