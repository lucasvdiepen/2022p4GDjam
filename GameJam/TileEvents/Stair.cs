using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Stair : TileBehaviour
    {
        private int _direction;

        public Stair(int direction)
        {
            _direction = direction;
        }

        public override void OnEnter(MoveEvent moveEvent)
        {
            moveEvent.GameContext.room = moveEvent.LevelLoader.GetRoom(moveEvent.GameContext.room.roomx, moveEvent.GameContext.room.roomy + _direction, moveEvent.GameContext.room.roomz);
        }
    }
}