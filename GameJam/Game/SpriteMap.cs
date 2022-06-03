using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameJam.Game
{
    internal class SpriteMap
    {
        private readonly Dictionary<char, Rectangle> tileMap = new Dictionary<char, Rectangle>();
        private readonly Rectangle[] playerAnimation;
        private readonly Rectangle[] sawAnimation;

        internal SpriteMap()
        {
            //21 Sprite different;

            //Staircase to next level EnterLevel (!) ExitLevel (?) 
            tileMap.Add('!', new Rectangle(44, 75, 16, 16));
            tileMap.Add('?', new Rectangle(44, 96, 16, 16));

            //Level Walls Level 1(1), Level 2(2), Level 3(3);
            tileMap.Add('1', new Rectangle(23, 75, 16, 16));
            tileMap.Add('2', new Rectangle(23, 96, 16, 16));
            tileMap.Add('3', new Rectangle(23, 117, 16, 16));

            //Level Floor Level 1(4), Level 2(5), Level 3(6)
            tileMap.Add('4', new Rectangle(86, 75, 16, 16));
            tileMap.Add('5', new Rectangle(86, 96, 16, 16));
            tileMap.Add('6', new Rectangle(86, 117, 16, 16));

            //Door Sprites North(N), South(S), West(W), East(E);
            tileMap.Add('N', new Rectangle(2, 75, 16, 16));
            tileMap.Add('S', new Rectangle(2, 96, 16, 16));
            tileMap.Add('W', new Rectangle(2, 117, 16, 16));
            tileMap.Add('E', new Rectangle(2, 138, 16, 16));

            playerAnimation = new Rectangle[]
            {
                    new Rectangle(43, 9, 16, 16),
                    new Rectangle(60, 9, 16, 16),
                    new Rectangle(77, 9, 16, 16)
            };

            sawAnimation = new Rectangle[]
            {
                    new Rectangle(149, 138, 16, 16),
                    new Rectangle(160, 138, 16, 16),
                    new Rectangle(181, 138, 16, 16),
                    new Rectangle(202, 138, 16, 16),
                    new Rectangle(149, 159, 16, 16),
                    new Rectangle(160, 159, 16, 16),
                    new Rectangle(181, 159, 16, 16),
                    new Rectangle(202, 159, 16, 16)
            };
        }

        internal Dictionary<char, Rectangle> GetMap()
        {
            return tileMap;
        }

        internal Rectangle[] GetPlayerFrames()
        {
            return playerAnimation;
        }
        internal Rectangle[] GetSawFrames()
        {
            return sawAnimation;
        }
    }

}


