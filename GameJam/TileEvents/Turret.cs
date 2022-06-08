using GameJam.Events;
using GameJam.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Turret : TimerBehaviour
    {
        int xDirection = 0;
        int yDirection = 1;

        public Turret(float shootDelay) : base(true, shootDelay)
        {

        }

        public override void TimerTick(UpdateEvent updateEvent)
        {
            Shoot(updateEvent.GameContext, updateEvent.RenderObject);
        }

        private void Shoot(GameContext gameContext, RenderObject renderObject)
        {
            Vector2 spawnPosition = new Vector2(renderObject.rectangle.X + gameContext.tileSize * xDirection, renderObject.rectangle.Y + gameContext.tileSize * yDirection);
        }
    }
}
