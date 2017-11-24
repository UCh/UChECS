namespace uchlab.ecs.commands
{
    public struct CommandEntityPair
    {
        public readonly Entity Entity;
        public readonly IEntityCommand Command;

        public CommandEntityPair(Entity entity, IEntityCommand command)
        {
            Entity = entity;
            Command = command;
        }
    }
    
}