using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameJam.Game
{
    internal class SpriteMap
    {
        private readonly Dictionary<char, Rectangle> tileMap = new Dictionary<char, Rectangle>();
        private readonly Dictionary<char, Func<TileBehaviour>> tileObjects = new Dictionary<char, Func<TileBehaviour>>();
        private readonly Rectangle[] playerAnimation;

        internal SpriteMap()
        {
            tileMap.Add('#', new Rectangle(45, 75, 16, 16));
            tileMap.Add('.', new Rectangle(23, 75, 16, 16));
            tileMap.Add('D', new Rectangle(2, 75, 16, 16));
            tileMap.Add('!', new Rectangle(66, 75, 16, 16));

            tileObjects.Add('D', GetNewDoorInstance);
            tileObjects.Add('#', GetNewWallInstance);

            playerAnimation = new Rectangle[]
                {
                    new Rectangle(43, 9, 16, 16),
                    new Rectangle(60, 9, 16, 16),
                    new Rectangle(77, 9, 16, 16)
                };
        }

        private TileBehaviour GetNewWallInstance()
        {
            return new TileBehaviour(true);
        }

        private Door GetNewDoorInstance()
        {
            return new Door();
        }

        internal Dictionary<char, Rectangle> GetMap()
        {
            return tileMap;
        }

        internal Dictionary<char, Func<TileBehaviour>> GetTileObjects()
        {
            return tileObjects;
        }

        internal Rectangle[] GetPlayerFrames()
        {
            return playerAnimation;
        }
    }

}


