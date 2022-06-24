using GameJam.Events;

namespace GameJam.TileEvents
{
    public class ObjectBehaviour
    {
        private bool _isMoveBlocked;
        private bool _isBuidable;
        private bool _isConnectable = true;

        public ObjectBehaviour()
        {

        }

        public ObjectBehaviour(bool isMoveBlocked, bool isBuildable, bool isConnectable)
        {
            _isMoveBlocked = isMoveBlocked;
            _isBuidable = isBuildable;
            _isConnectable = isConnectable;
        }

        public bool IsMoveBlocked => _isMoveBlocked;

        public bool IsBuildable => _isBuidable;

        public bool IsConnectable => _isConnectable;

        public virtual void OnEnter(MoveEvent moveEvent)
        {
            
        }

        public virtual void Update(UpdateEvent updateEvent)
        {
            
        }
    }
}
