using System;
using System.Collections.Generic;
using System.Drawing;
using GameJam.Enums;
using GameJam.TileEvents;

namespace GameJam.Game
{
    public class SpriteMap
    {
        private readonly Dictionary<char, Rectangle> tileMap = new Dictionary<char, Rectangle>();
        private readonly Dictionary<char, Func<TileBehaviour>> tileObjects = new Dictionary<char, Func<TileBehaviour>>();

        private readonly Dictionary<HeartState, Rectangle> heart = new Dictionary<HeartState, Rectangle>();
        private readonly Dictionary<Vector2, Rectangle> wallSpikes = new Dictionary<Vector2, Rectangle>(new Vector2Comparer());
        private readonly Dictionary<Vector2, Rectangle> turret = new Dictionary<Vector2, Rectangle>(new Vector2Comparer());
        private readonly Dictionary<Vector2, Rectangle[]> bullet = new Dictionary<Vector2, Rectangle[]>(new Vector2Comparer());

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

            tileObjects.Add('N', () => { return new Door(); });
            tileObjects.Add('W', () => { return new Door(); });
            tileObjects.Add('E', () => { return new Door(); });
            tileObjects.Add('S', () => { return new Door(); });

            tileObjects.Add('1', () => { return new TileBehaviour(true, false); });
            tileObjects.Add('2', () => { return new TileBehaviour(true, false); });
            tileObjects.Add('3', () => { return new TileBehaviour(true, false); });

            tileObjects.Add('!', () => { return new Stair(-1); });
            tileObjects.Add('?', () => { return new Stair(1); });

            //Wall Spike sprites for GameObject;
            wallSpikes.Add(new Vector2(0, -1), new Rectangle(86, 138, 16, 16));
            wallSpikes.Add(new Vector2(0, 1), new Rectangle(86, 159, 16, 16));
            wallSpikes.Add(new Vector2(-1, 0), new Rectangle(107, 138, 16, 16));
            wallSpikes.Add(new Vector2(1, 0), new Rectangle(107, 159, 16, 16));

            //Turret sprite for Gameobject;
            turret.Add(new Vector2(0, -1), new Rectangle(44, 180, 16, 16));
            turret.Add(new Vector2(1, 0), new Rectangle(65, 180, 16, 16));
            turret.Add(new Vector2(0, 1), new Rectangle(86, 180, 16, 16));
            turret.Add(new Vector2(-1, 0), new Rectangle(107, 180, 16, 16));

            //Heart sprites
            heart.Add(HeartState.Full, new Rectangle(2, 180, 16, 16));
            heart.Add(HeartState.Empty, new Rectangle(23, 180, 16, 16));

            //Bullet Animation Dictinary;
            bullet.Add(new Vector2(0, -1), new Rectangle[] 
            {   
                new Rectangle(128, 180, 16, 16),
                new Rectangle(128, 201, 16, 16),
                new Rectangle(128, 222, 16, 16)
            });

            bullet.Add(new Vector2(1, 0), new Rectangle[] 
            {
                new Rectangle(149, 180, 16, 16),
                new Rectangle(149, 201, 16, 16),
                new Rectangle(149, 222, 16, 16)
            });

            bullet.Add(new Vector2(0, 1), new Rectangle[] 
            {
                new Rectangle(170, 180, 16, 16),
                new Rectangle(170, 201, 16, 16),
                new Rectangle(170, 222, 16, 16)
            });

            bullet.Add(new Vector2(-1, 0), new Rectangle[] 
            {
                new Rectangle(191, 180, 16, 16),
                new Rectangle(191, 201, 16, 16),
                new Rectangle(191, 222, 16, 16)
            });

            playerAnimation = new Rectangle[]
            {
                    new Rectangle(43, 9, 16, 16),
                    new Rectangle(60, 9, 16, 16),
                    new Rectangle(77, 9, 16, 16)
            };

            sawAnimation = new Rectangle[]
            {
                    new Rectangle(149, 138, 16, 16),
                    new Rectangle(170, 138, 16, 16),
                    new Rectangle(191, 138, 16, 16),
                    new Rectangle(212, 138, 16, 16),
                    new Rectangle(149, 159, 16, 16),
                    new Rectangle(170, 159, 16, 16),
                    new Rectangle(191, 159, 16, 16),
                    new Rectangle(212, 159, 16, 16)
            };
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
        internal Rectangle[] GetSawFrames()
        {
            return sawAnimation;
        }

        internal Rectangle[] GetBulletFrames(Vector2 direction)
        {
            return bullet[direction];
        }

        internal Rectangle[] GetTurretFrames(Vector2 direction)
        {
            return new Rectangle[] { turret[direction] };
        }

        internal Rectangle[] GetWallSpikeFrames(Vector2 direction)
        {
            return new Rectangle[] { wallSpikes[direction] };
        }

        internal Rectangle[] GetHeartFrames(HeartState heartState)
        {
            return new Rectangle[] { heart[heartState] };
        }
    }
}


