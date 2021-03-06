using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class TileBehaviour : ObjectBehaviour
    {
        public TileBehaviour()
        {

        }

        public TileBehaviour(bool isMoveBlocked, bool isBuildable, bool isConnectable) : base(isMoveBlocked, isBuildable, isConnectable)
        {

        }

        public virtual CanEnterEvent CanEnter(MoveEvent moveEvent)
        {
            return new CanEnterEvent() { BlockMovement = IsMoveBlocked, BlockEvents = IsMoveBlocked };
        }

        public virtual void OnExit(MoveEvent moveEvent)
        {

        }
    }
}
