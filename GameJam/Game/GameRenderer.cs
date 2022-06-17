﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WindowsFormsApp1.Game;

namespace GameJam.Game
{
    public class GameRenderer : IDisposable
    {
        private readonly GameContext context;
        private float frametime;
        private readonly Image image;
        private readonly Font font;
       
        Health hp = new Health(3);

        public GameRenderer(GameContext context)
        {
            this.context = context;

            image = Bitmap.FromFile("sprites.png");

            font = new Font(FontFamily.GenericMonospace, 10);

        }
        private Graphics InitGraphics(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //make nice pixels
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;


            g.Transform = new Matrix();
            g.ScaleTransform(context.scaleunit, context.scaleunit);
            //there will be some tearing between tiles, a solution to that is to render to a bitmap then draw that bitmap, fun challenge?
            g.Clear(Color.Black);
            return g;
        }
        internal void Render(PaintEventArgs e, float frametime)
        {
            this.frametime = frametime;

            Graphics g = InitGraphics(e);
            RenderRoom(g);
            RenderObject(g, context.player);

            List<RenderObject> activeObjects = context.room.activeObjects;


            g.DrawString(hp.CurrentHp.ToString(), font, Brushes.Black, 1,1);

            int c = activeObjects.Count;
            for(int i = 0; i < c; i++)
            {
                RenderObject(g, activeObjects[i]);
            }

        }

        private void RenderRoom(Graphics g)
        {
            foreach (Tile[] row in context.room.tiles)
            {
                foreach (Tile t in row)
                {
                    g.DrawImage(image, t.rectangle, t.sprite, GraphicsUnit.Pixel);
                }
            }
        }

        private void RenderObject(Graphics g, RenderObject renderObject)
        {
            g.DrawImage(image, renderObject.rectangle, renderObject.frames[renderObject.frame], GraphicsUnit.Pixel);
            renderObject.MoveFrame(frametime);
        }

        public void Dispose()
        {
            image.Dispose();
        }
    }

}


