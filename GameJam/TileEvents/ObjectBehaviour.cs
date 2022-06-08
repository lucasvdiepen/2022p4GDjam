using GameJam.Events;

namespace GameJam.TileEvents
{
    public class ObjectBehaviour
    {
        private bool _isMoveBlocked;

        public ObjectBehaviour()
        {

        }

        public ObjectBehaviour(bool isMoveBlocked)
        {
            _isMoveBlocked = isMoveBlocked;
        }

        public bool IsMoveBlocked
        {
            get => _isMoveBlocked;
        }

        public virtual CanEnterEvent CanEnter(MoveEvent moveEvent)
        {
            return new CanEnterEvent() { BlockMovement = _isMoveBlocked, BlockEvents = _isMoveBlocked };
        }

        public virtual void OnEnter(MoveEvent moveEvent)
        {
            
        }

        public virtual void Update(UpdateEvent updateEvent)
        {
            
        }
    }
}
