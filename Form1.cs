using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpillBucketA3
{
    public partial class Form1 : Form
    {
        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 1);
        Pen Eraser = new Pen(Color.White, 10);
        int index;
        int x, y, sx, sy, cx, cy;
        public Form1()
        {
            InitializeComponent();
            bm = new Bitmap(Pic.Width, Pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            Pic.Image = bm;
        }

        private void Pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;

            cx = e.X;
            cy = e.Y;
        }

        private void Pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if(index == 1)
                {
                    px = e.Location;
                    g.DrawLine(p, px, py);
                    py = px;
                }
                if(index == 2)
                {
                    px = e.Location;
                    g.DrawLine(Eraser, px, py);
                    py = px;
                }
            }
            Pic.Refresh();

            x = e.X;
            y = e.Y;
            sx = e.X - cx;
            sy = e.Y - cy;
        }

        private void Pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;

            sx = x - cx;
            sy = y - cy;
            if(index == 3)
            {
                g.DrawEllipse(p, cx, cy, sx, sy);
            }
        }

        private void BtnEraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void BtnEllipse_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void BtnPencil_Click(object sender, EventArgs e)
        {
            index = 1;
        }
    }
}
