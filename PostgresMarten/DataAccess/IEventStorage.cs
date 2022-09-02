using PostgresMarten.Events;

namespace PostgresMarten.DataAccess
{
    public interface IEventStorage
    {
        void StartStream(UserAdded userAddedEvent);
        void AppendStream(UserAdded userAddedEvent);
    }
}
