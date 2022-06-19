using GameJam.Events;
using GameJam.Game;
using GameJam.TileEvents;
using GameJam.Tools;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GameJam
{
    public partial class RenderForm : Form
    {
        private LevelLoader levelLoader;
        private float frametime;
        private GameRenderer renderer;
        //private Audio audio;
        private readonly GameContext gc = new GameContext();
        private PlayerMovement playerMovement = new PlayerMovement();

        public RenderForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
            ResizeRedraw = true;

            //audio = new Audio();
            KeyDown += RenderForm_KeyDown;
            KeyUp += RenderForm_KeyUp;
            FormClosing += Form1_FormClosing;
            Load += RenderForm_Load;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            renderer.Dispose();
            //audio.Dispose();
        }
        private void RenderForm_Load(object sender, EventArgs e)
        {
            levelLoader = new LevelLoader(gc.tileSize, new FileLevelDataSource());
            levelLoader.LoadRooms(gc.spriteMap);

            renderer = new GameRenderer(gc);

            gc.room = levelLoader.GetRoom(0, 0, 0);

            gc.player = new RenderObject()
            {
                frames = gc.spriteMap.GetPlayerFrames(),
                rectangle = new Rectangle(2 * gc.tileSize, 2 * gc.tileSize, gc.tileSize, gc.tileSize),
                animationTime = 0.1f
            };

            ClientSize =
             new Size(

                (gc.tileSize * gc.room.tiles[0].Length) * gc.scaleunit,
                (gc.tileSize * gc.room.tiles.Length) * gc.scaleunit
                );
        }

        private void RenderForm_KeyDown(object sender, KeyEventArgs e)
        {
            playerMovement.OnKeyDown(e.KeyCode);
        }
        
        private void RenderForm_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);

            playerMovement.OnKeyUp(e.KeyCode);
        }

        public void Logic(float frametime)
        {
            this.frametime = frametime;

            var newUpdateEvent = new UpdateEvent()
            {
                FrameTime = frametime,
                GameContext = gc,
                LevelLoader = levelLoader,
                GameRenderer = renderer
            };

            playerMovement.Update(newUpdateEvent);

            //Update all tiles
            Tile[] allTiles = gc.room.GetAllTiles();

            int c = allTiles.Length;
            for (int i = 0; i < c; i++)
            {
                allTiles[i].tileBehaviour?.Update(newUpdateEvent);
            }

            //Update active objects
            List<RenderObject> activeObjects = gc.room.activeObjects;

            int l = activeObjects.Count;
            for(int i = l - 1; i >= 0; i--)
            {
                newUpdateEvent.RenderObject = activeObjects[i];
                activeObjects[i].objectBehaviour?.Update(newUpdateEvent);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            renderer.Render(e, frametime);
        }
    }

}


