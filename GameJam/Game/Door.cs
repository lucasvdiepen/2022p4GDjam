using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Game
{
    public class Door : TileBehaviour
    {
        public Door(bool isMoveBlocked) : base(isMoveBlocked) { }

        public override void OnEnter(MoveEvent moveEvent)
        {
            moveEvent.GameContext.room = moveEvent.LevelLoader.GetRoom(moveEvent.GameContext.room.roomx + (int)moveEvent.Direction.x, moveEvent.GameContext.room.roomy + (int)moveEvent.Direction.y);

            if ((int)moveEvent.Direction.y != 0)
            {
                moveEvent.PlayerRenderer.rectangle.Y += -(int)moveEvent.Direction.y * ((moveEvent.GameContext.room.tiles.Length - 2) * moveEvent.GameContext.tileSize);
            }
            else
            {
                moveEvent.PlayerRenderer.rectangle.X += -(int)moveEvent.Direction.x * ((moveEvent.GameContext.room.tiles[0].Length - 2) * moveEvent.GameContext.tileSize);
            }
        }

        public override void OnExit()
        {
            throw new NotImplementedException();
        }
    }
}
