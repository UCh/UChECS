using System;
using System.Collections.Generic;
using System.Linq;

namespace uchlab.ecs.commands
{
    public class EntityCommandProvider
    {
        private Dictionary<Type,IEntityCommand> commandsMap;
        public EntityCommandProvider(List<IEntityCommand> availableCommands)
        {
            commandsMap = availableCommands.ToDictionary(c => c.GetType(),c => c);
        }

        public IEntityCommand GetCommand<T>(){
            return commandsMap[typeof(T)];
        }
    }
}