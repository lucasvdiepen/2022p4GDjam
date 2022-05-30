using GameJam.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using GameJam.TileEvents;

namespace GameJam.Game
{
    public class LevelLoader
    {
        private readonly ILevelDataSource levelDataSource;
        private readonly int size;
        private readonly Dictionary<string, Room> rooms = new Dictionary<string, Room>();
        public LevelLoader(int size, ILevelDataSource levelDataSource)
        {
            this.levelDataSource = levelDataSource;
            this.size = size;
        }

        public void LoadRooms(Dictionary<char, Rectangle> tileMap, Dictionary<char, Func<TileBehaviour>> tileObject)
        {
            string dir = Path.Combine(PathHelper.ExeDir(), "leveldata");
            foreach (FileInfo file in new DirectoryInfo(dir).GetFiles())
            {
                string[] split= file.Name.Split('.');
                int x = int.Parse(split[1]);
                int y = int.Parse(split[2]);
                Room r = Load(x,y, tileMap, tileObject);
                rooms.Add($"{x}-{y}",r);
            }
        }

        public Room GetRoom(int roomX, int roomY)
        {
            return rooms[$"{roomX}-{roomY}"];
        }
        private Room Load(int roomX, int roomY, Dictionary<char, Rectangle> tileMap, Dictionary<char, Func<TileBehaviour>> roomObjects)
        {
            Room room = new Room()
            {
                roomx = roomX,
                roomy = roomY
            };
            string[] lines = levelDataSource.GetLines(roomX, roomY);

            room.tiles = new Tile[lines.Length][];
            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                room.tiles[y] = new Tile[line.Length];
                for (int x = 0; x < room.tiles[y].Length; x++)
                {
                    char tileChar = line[x];

                    room.tiles[y][x] = new Tile()
                    {
                        graphic = tileChar,
                        rectangle = new Rectangle(size * x, size * y, size, size),
                        sprite = tileMap[line[x]],
                        tileBehaviour = roomObjects.ContainsKey(tileChar) ? roomObjects[tileChar].Invoke() : null
                    };

                }
            }
            return room;
        }
    }
}



