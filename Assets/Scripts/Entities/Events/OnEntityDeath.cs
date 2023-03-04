using General;

namespace Entities.Events
{
    public struct OnEntityDeath : IEvent
    {
        public Entity Entity;
    }
}