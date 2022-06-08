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

        public TileBehaviour(bool isMoveBlocked) : base(isMoveBlocked)
        {

        }

        public virtual void OnExit(MoveEvent moveEvent)
        {

        }
    }
}
