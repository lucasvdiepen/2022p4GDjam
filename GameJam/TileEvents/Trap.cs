using GameJam.Enums;
using GameJam.Events;
using GameJam.Game;
using GameJam.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Trap : TimerBehaviour, ITrap
    {
        // todo: moet bepaald worden door trap generator
        private Direction2D _direction2D;
        private int _direction;

        public Trap(float moveTime) : base(false, false, moveTime)
        {
            Random rnd = new Random();
            _direction = rnd.Next(0, 2) == 1 ? 1 : -1;
            _direction2D = (Direction2D)rnd.Next(0, Enum.GetValues(typeof(Direction2D)).Length);
        }

        public override void OnEnter(MoveEvent moveEvent)
        {
            //Deal damage to player
            moveEvent.GameContext.playerHealth.RemoveHealth(1);
        }

        public override void TimerTick(UpdateEvent updateEvent)
        {
            Move(updateEvent.GameContext, updateEvent.RenderObject);
        }

        private void Move(GameContext gameContext, RenderObject renderObject)
        {
            int newX = (int)renderObject.rectangle.X;
            int newY = (int)renderObject.rectangle.Y;

            switch(_direction2D)
            {
                case Direction2D.Horizontal:
                    newX += _direction * gameContext.tileSize;
                    break;
                case Direction2D.Vertical:
                    newY += _direction * gameContext.tileSize;
                    break;
            }

            if(gameContext.room.IsMoveBlocked(newX, newY))
            {
                InvertMove(gameContext, renderObject);
                return;
            }

            renderObject.rectangle.X = newX;
            renderObject.rectangle.Y = newY;

            //Deal damage to player
            if(CollisionUtility.HasCollision(renderObject.rectangle, gameContext.player.rectangle)) gameContext.playerHealth.RemoveHealth(1);
        }

        private void InvertMove(GameContext gameContext, RenderObject renderObject)
        {
            _direction *= -1;
            Move(gameContext, renderObject);
        }

        public Vector2 GetSuitableLocation(Room room, int tileSize)
        {
            Tile[] buildableTiles = room.GetBuildableTiles();

            if (buildableTiles.Length == 0) return null;

            Random rnd = new Random();
            var rect = buildableTiles[rnd.Next(0, buildableTiles.Length)].rectangle;

            return new Vector2(rect.X, rect.Y);
        }

        public Rectangle[] GetFrames(SpriteMap spriteMap) => spriteMap.GetPlayerFrames();
    }
}
