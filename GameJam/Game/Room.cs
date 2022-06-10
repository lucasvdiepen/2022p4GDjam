using System.Collections.Generic;
using System.Linq;

namespace GameJam.Game
{
    public class Room
    {
        public Tile[][] tiles;
        public List<RenderObject> activeObjects = new List<RenderObject>();
        internal int roomx;
        internal int roomy;
        internal int roomz;

        public Tile GetTile(int x, int y)
        {
            return tiles.SelectMany(ty => ty.Where(tx => tx.rectangle.Contains(x, y))).FirstOrDefault();
        }

        public RenderObject[] GetActiveObjects(int x, int y)
        {
            List<RenderObject> list = new List<RenderObject>();

            foreach(RenderObject renderObject in activeObjects)
            {
                if ((int)renderObject.rectangle.X == x && (int)renderObject.rectangle.Y == y) list.Add(renderObject);
            }

            return list.ToArray();
        }

        public bool IsMoveBlocked(int x, int y)
        {
            Tile tile = GetTile(x, y);

            return IsMoveBlocked(tile);
        }

        public bool IsActiveRenderObjectBlocking(int x, int y)
        {
            RenderObject[] activeRenderObjects = GetActiveObjects(x, y);

            foreach (RenderObject activeObject in activeRenderObjects)
            {
                if (activeObject.objectBehaviour != null && activeObject.objectBehaviour.IsMoveBlocked) return true;
            }

            return false;
        }

        public bool IsMoveBlocked(Tile tile)
        {
            //Check if tile is blocking movement
            if (tile == null) return true;
            if (tile.tileBehaviour != null && tile.tileBehaviour.IsMoveBlocked) return true;

            //Check if active render objects are blocking movement
            return IsActiveRenderObjectBlocking(tile.rectangle.X, tile.rectangle.Y);
        }

        public Tile[] GetAllTiles()
        {
            List<Tile> allTiles = new List<Tile>();

            int yLength = tiles.Length;
            for(int y = 0; y < yLength; y++)
            {
                int xLength = tiles[y].Length;
                for(int x = 0; x < xLength; x++)
                {
                    allTiles.Add(tiles[y][x]);
                }
            }

            return allTiles.ToArray();
        }

        public Tile[] GetBuildableTiles()
        {

        }
    }
}