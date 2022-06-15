using GameJam.Events;
using GameJam.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Spike : ObjectBehaviour, ITrap
    {
        private Vector2 _direction;

        public override void OnEnter(MoveEvent moveEvent)
        {
            moveEvent.GameContext.playerHealth.RemoveHealth(1);
        }

        public Rectangle[] GetFrames(SpriteMap spriteMap) => spriteMap.GetWallSpikeFrames(_direction);

        public Vector2 GetSuitableLocation(Room room, int tileSize, Random rnd)
        {
            var tryTimes = 10;
            for (int i = 0; i < tryTimes; i++)
            {
                room.GetRandomMoveBlockingTile(rnd);
            }
        }
    }
}
