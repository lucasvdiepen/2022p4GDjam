using GameJam.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Game
{
    public static class HealthRenderer
    {
        public static RenderObject[] RenderHearts(GameContext gameContext, int health, int maxHealth)
        {
            RenderObject[] hearts = new RenderObject[maxHealth];

            for(int i = 0; i < maxHealth; i++)
            {
                HeartState currentHeartState;
                if (i < health) currentHeartState = HeartState.Full;
                else currentHeartState = HeartState.Empty;

                var newHeart = new RenderObject()
                {
                    frames = gameContext.spriteMap.GetHeartFrames(currentHeartState),
                    rectangle = new Rectangle(i * gameContext.tileSize, 0, gameContext.tileSize, gameContext.tileSize),
                    animationTime = 0.1f
                };

                hearts[i] = newHeart;
            }

            return hearts;
        }
    }
}
