using System;
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

        public bool IsMoveBlocked(Vector2 position)
        {
            return IsMoveBlocked(position.x, position.y);
        }

        public bool IsBuildable(int x, int y)
        {
            Tile tile = GetTile(x, y);

            return IsBuildable(tile);
        }

        public bool IsBuildable(Vector2 position)
        {
            return IsBuildable(position.x, position.y);
        }

        public bool IsBuildable(Tile tile)
        {
            //Check if tile is buildable
            if (tile == null) return false;
            if (tile.tileBehaviour != null && !tile.tileBehaviour.IsBuildable) return false;

            return IsActiveRenderObjectBuildable(tile.rectangle.X, tile.rectangle.Y);
        }

        public bool IsConnectable(int x, int y)
        {
            Tile tile = GetTile(x, y);

            return IsConnectable(tile);
        }

        public bool IsConnectable(Vector2 position)
        {
            return IsConnectable(position.x, position.y);
        }

        public bool IsConnectable(Tile tile)
        {
            if (tile == null) return false;
            if (tile.tileBehaviour != null && !tile.tileBehaviour.IsConnectable) return false;

            return IsActiveRenderObjectConnectable(tile.rectangle.X, tile.rectangle.Y);
        }

        public bool IsActiveRenderObjectBuildable(int x, int y)
        {
            RenderObject[] activeRenderObjects = GetActiveObjects(x, y);

            foreach (RenderObject activeObject in activeRenderObjects)
            {
                if (activeObject.objectBehaviour != null && !activeObject.objectBehaviour.IsBuildable) return false;
            }

            return true;
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

        public bool IsActiveRenderObjectConnectable(int x, int y)
        {
            RenderObject[] activeRenderObjects = GetActiveObjects(x, y);

            foreach(RenderObject activeObject in activeRenderObjects)
            {
                if (activeObject.objectBehaviour != null && !activeObject.objectBehaviour.IsConnectable) return false;
            }

            return true;
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

        public List<Tile> GetBuildableTiles()
        {
            Dictionary<Vector2, Tile> buildableTiles = new Dictionary<Vector2, Tile>(new Vector2Comparer());

            Tile[] allTiles = GetAllTiles();

            foreach(Tile tile in allTiles)
            {
                if (tile.tileBehaviour == null || tile.tileBehaviour.IsBuildable) buildableTiles.Add(new Vector2(tile.rectangle.X, tile.rectangle.Y), tile);
            }

            foreach(RenderObject renderObject in activeObjects)
            {
                if (renderObject.objectBehaviour == null || renderObject.objectBehaviour.IsBuildable) continue;

                buildableTiles.Remove(new Vector2(renderObject.rectangle.X, renderObject.rectangle.Y));
            }

            return buildableTiles.Values.ToList();
        }

        public Vector2 GetRandomBuildableTile(Random rnd)
        {
            List<Tile> buildableTiles = GetBuildableTiles();
            if (buildableTiles.Count == 0) return null;

            var rect = buildableTiles[rnd.Next(0, buildableTiles.Count)].rectangle;
            return new Vector2(rect.X, rect.Y);
        }

        public List<Tile> GetMoveBlockingTiles()
        {
            List<Tile> blockingTiles = new List<Tile>();

            Tile[] allTiles = GetAllTiles();

            foreach(Tile tile in allTiles)
            {
                if (tile.tileBehaviour != null && tile.tileBehaviour.IsMoveBlocked) blockingTiles.Add(tile);
            }

            return blockingTiles;
        }

        public Tile GetRandomMoveBlockingTile(Random rnd)
        {
            List<Tile> moveBlockingTiles = GetMoveBlockingTiles();
            if (moveBlockingTiles.Count == 0) return null;

            return moveBlockingTiles[rnd.Next(0, moveBlockingTiles.Count)];
        }
    }
}