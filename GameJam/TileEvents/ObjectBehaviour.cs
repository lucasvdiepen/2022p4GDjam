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

        public virtual void OnEnter(MoveEvent moveEvent)
        {
            
        }

        public virtual void Update(UpdateEvent updateEvent)
        {
            
        }
    }
}
