using Marten;
using Marten.Events;
using PostgresMarten.Events;

namespace PostgresMarten.DataAccess
{
    public class EventStorage : IEventStorage
    {
        private readonly IEventStore _eventStore;
        private Guid streamId = Guid.NewGuid();
        public EventStorage(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public void StartStream(UserAdded userAddedEvent)
        {
            _eventStore.StartStream(streamId, userAddedEvent);
        }

        public void AppendStream(UserAdded userAddedEvent)
        {
            _eventStore.Append(streamId, userAddedEvent);
        }
    }
}
