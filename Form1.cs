using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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

        Color New_Color;
        ColorDialog cd = new ColorDialog();
        public Form1()
        {
            InitializeComponent();
            bm = new Bitmap(Pic.Width, Pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            Pic.Image = bm;
        }

        private void BtnRectangle_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void BtnLine_Click(object sender, EventArgs e)
        {
            index = 5;
        }

        private void Pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (paint)
            {
                if (index == 3)
                {
                    g.DrawEllipse(p, cx, cy, sx, sy);
                }
                if (index == 4)
                {
                    g.DrawRectangle(p, cx, cy, sx, sy);
                }
                if (index == 5)
                {
                    g.DrawLine(p, cx, cy, x, y);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            Pic.Image = bm;
            index = 0;
        }

        private void BtnPaint_Click(object sender, EventArgs e)
        {
            index = 6;
        }

        static Point set_Point(PictureBox pb, Point pt)
        {
            float px = 1f * pb.Width / pb.Width;
            float py = 1f * pb.Height / pb.Height;
            return new Point((int)(pt.X * px), (int)(pt.Y * py));
        }
        private void Validate(Bitmap bm, Stack<Point> sp, int x, int y, Color Old_Color, Color New_Color)
        {
            Color cx = bm.GetPixel(x, y);
            if(cx == Old_Color)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, New_Color);
            }
        }


        // Spill Bucket using Stack (Depth First Search - DFS)
        public void Fill(Bitmap bm, int x, int y, Color New_Clr)
        {
            // Get the color of the clicked pixel
            Color Old_Color = bm.GetPixel(x, y);

            // Stack to store pixels to be filled
            Stack<Point> pixel = new Stack<Point>();

            // Push starting point
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, New_Clr);
            if (Old_Color == New_Clr) return;

            // Continue until stack is empty
            while (pixel.Count > 0)
            {
                Point pt = (Point)pixel.Pop();
                if(pt.X > 0 && pt.Y > 0 && pt.X < bm.Width - 1 && pt.Y < bm.Height - 1)
                {
                    // Check all four neighbors
                    Validate(bm, pixel, pt.X - 1, pt.Y, Old_Color, New_Clr);
                    Validate(bm, pixel, pt.X + 1, pt.Y, Old_Color, New_Clr);
                    Validate(bm, pixel, pt.X, pt.Y - 1, Old_Color, New_Clr);
                    Validate(bm, pixel, pt.X, pt.Y + 1, Old_Color, New_Clr);
                }
            }
        }

        // Spill Bucket using Queue (Breadth First Search - BFS)
        public void FillQueue(Bitmap bm, int x, int y, Color New_Clr)
        {
            Color Old_Color = bm.GetPixel(x, y);
            if (Old_Color == New_Clr) return;

            Queue<Point> q = new Queue<Point>();
            // Add starting point
            q.Enqueue(new Point(x, y));
            bm.SetPixel(x, y, New_Clr);

            // Continue until queue is empty
            while (q.Count > 0)
            {
                Point pt = q.Dequeue();

                if (pt.X > 0 && pt.Y > 0 && pt.X < bm.Width - 1 && pt.Y < bm.Height - 1)
                {
                    // Check neighboring pixels
                    if (bm.GetPixel(pt.X - 1, pt.Y) == Old_Color)
                    {
                        bm.SetPixel(pt.X - 1, pt.Y, New_Clr);
                        q.Enqueue(new Point(pt.X - 1, pt.Y));
                    }
                    if (bm.GetPixel(pt.X + 1, pt.Y) == Old_Color)
                    {
                        bm.SetPixel(pt.X + 1, pt.Y, New_Clr);
                        q.Enqueue(new Point(pt.X + 1, pt.Y));
                    }
                    if (bm.GetPixel(pt.X, pt.Y - 1) == Old_Color)
                    {
                        bm.SetPixel(pt.X, pt.Y - 1, New_Clr);
                        q.Enqueue(new Point(pt.X, pt.Y - 1));
                    }
                    if (bm.GetPixel(pt.X, pt.Y + 1) == Old_Color)
                    {
                        bm.SetPixel(pt.X, pt.Y + 1, New_Clr);
                        q.Enqueue(new Point(pt.X, pt.Y + 1));
                    }
                }
            }
        }


        private void Pic_MouseClick(object sender, MouseEventArgs e)
        {
            if (index == 6)
            {
                Point pt = set_Point(Pic, e.Location);

                if (rbStack.Checked)
                {
                    Fill(bm, pt.X, pt.Y, New_Color);      // Stack (DFS)
                }
                else if (rbQueue.Checked)
                {
                    FillQueue(bm, pt.X, pt.Y, New_Color); // Queue (BFS)
                }

                Pic.Refresh();
            }
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            New_Color = cd.Color;
            Pic.BackColor = New_Color;
            p.Color = cd.Color;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*)|*.*";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, Pic.Width, Pic.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
            }
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
            if(index == 4)
            {
                g.DrawRectangle(p, cx, cy, sx, sy);
            }
            if(index == 5)
            {
                g.DrawLine(p, cx, cy, x, y);
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
