using GameJam.Events;

namespace GameJam.TileEvents
{
    public class ObjectBehaviour
    {
        private bool _isMoveBlocked;
        private bool _isBuidable;

        public ObjectBehaviour()
        {

        }

        public ObjectBehaviour(bool isMoveBlocked, bool isBuildable)
        {
            _isMoveBlocked = isMoveBlocked;
            _isBuidable = isBuildable;
        }

        public bool IsMoveBlocked => _isMoveBlocked;

        public bool IsBuildable => _isBuidable;

        public virtual void OnEnter(MoveEvent moveEvent)
        {
            
        }

        public virtual void Update(UpdateEvent updateEvent)
        {
            
        }
    }
}
