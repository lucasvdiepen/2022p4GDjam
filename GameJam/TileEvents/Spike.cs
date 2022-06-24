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

        public Spike() : base(false, false, true)
        {

        }

        public override void OnEnter(MoveEvent moveEvent)
        {
            moveEvent.GameContext.playerHealth.RemoveHealth(1);
        }

        public Rectangle[] GetFrames(SpriteMap spriteMap) => spriteMap.GetWallSpikeFrames(_direction);

        public Vector2 GetSuitableLocation(Room room, int tileSize, Random rnd)
        {
            List<Tile> moveBlockingTiles = room.GetMoveBlockingTiles();
            for (int i = moveBlockingTiles.Count - 1; i >= 0; i--)
            {
                Tile currentTile = moveBlockingTiles[rnd.Next(0, moveBlockingTiles.Count)];

                Vector2 currentTilePosition = new Vector2(currentTile.rectangle.X, currentTile.rectangle.Y);

                List<Vector2> suitableDirection = new List<Vector2>();
                foreach(Vector2 direction in Vector2.AllDirections)
                {
                    Vector2 newPosition = currentTilePosition + direction * tileSize;

                    if (!room.IsMoveBlocked(newPosition) && room.IsBuildable(newPosition)) suitableDirection.Add(direction);
                }

                if (suitableDirection.Count == 0)
                {
                    moveBlockingTiles.Remove(currentTile);
                    continue;
                }

                _direction = suitableDirection[rnd.Next(0, suitableDirection.Count)];

                return currentTilePosition + _direction * tileSize;
            }

            return null;
        }
    }
}
