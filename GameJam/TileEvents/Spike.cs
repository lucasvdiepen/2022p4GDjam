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
                Tile randomBlockingTile = room.GetRandomMoveBlockingTile(rnd);
                Vector2 randomBlockingTilePosition = new Vector2(randomBlockingTile.rectangle.X, randomBlockingTile.rectangle.Y);

                List<Vector2> suitableDirection = new List<Vector2>();
                foreach(Vector2 direction in Vector2.AllDirections)
                {
                    Vector2 newPosition = randomBlockingTilePosition + direction * tileSize;

                    // todo: && check if buildable
                    if (!room.IsMoveBlocked(newPosition) && room.IsBuildable(newPosition)) suitableDirection.Add(direction);
                }

                if (suitableDirection.Count == 0)
                {
                    // todo: remove from blocking tiles list and try again
                    continue;
                }

                _direction = suitableDirection[rnd.Next(0, suitableDirection.Count)];

                return randomBlockingTilePosition + _direction * tileSize;
            }

            return null;
        }
    }
}
