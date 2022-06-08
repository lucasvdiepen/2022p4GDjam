using GameJam.Enums;
using GameJam.Events;
using GameJam.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Trap : TimerBehaviour
    {
        private Direction2D _direction2D;
        private int _direction;

        public Trap(float moveTime) : base(moveTime)
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

            Tile nextTile = gameContext.room.GetTile(newX, newY);

            if(nextTile != null)
            {
                if(nextTile.tileBehaviour != null && nextTile.tileBehaviour.IsMoveBlocked())
                {
                    InvertMove(gameContext, renderObject);
                    return;
                }

                renderObject.rectangle.X = newX;
                renderObject.rectangle.Y = newY;

                if((int)renderObject.rectangle.X == (int)gameContext.player.rectangle.X && (int)renderObject.rectangle.Y == (int)gameContext.player.rectangle.Y)
                {
                    gameContext.playerHealth.RemoveHealth(1);
                }
            }
            else
            {
                InvertMove(gameContext, renderObject);
                return;
            }
        }

        private void InvertMove(GameContext gameContext, RenderObject renderObject)
        {
            _direction *= -1;
            Move(gameContext, renderObject);
        }
    }
}
