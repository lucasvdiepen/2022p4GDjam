using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Door : TileBehaviour
    {
        public Door() : base(true, false, false)
        {

        }

        public override CanEnterEvent CanEnter(MoveEvent moveEvent)
        {
            moveEvent.GameContext.room = moveEvent.LevelLoader.GetRoom(moveEvent.GameContext.room.roomx + moveEvent.Direction.x, moveEvent.GameContext.room.roomy, moveEvent.GameContext.room.roomz + moveEvent.Direction.y);

            if (moveEvent.Direction.y != 0)
            {
                moveEvent.PlayerRenderer.rectangle.Y += -moveEvent.Direction.y * ((moveEvent.GameContext.room.tiles.Length - 2) * moveEvent.GameContext.tileSize);
            }
            else
            {
                moveEvent.PlayerRenderer.rectangle.X += -moveEvent.Direction.x * ((moveEvent.GameContext.room.tiles[0].Length - 2) * moveEvent.GameContext.tileSize);
            }

            return new CanEnterEvent() { BlockMovement = IsMoveBlocked, BlockEvents = false };
        }
    }
}
