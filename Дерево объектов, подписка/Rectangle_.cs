using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace Grouping_and_saving
{
    class Rectangle_ : Shape
    {
        private int w_r, h_r;
        public Rectangle_()
        {
            centre.X = 85;
            centre.Y = 65;
            w_r = 50;
            h_r = 30;
            color = "Red";
            frame_point = new Point[]
            {new Point(136, 34),
            new Point(34, 34),
            new Point(34, 96),
            new Point(136, 96),
            new Point(136, 34)};
        }
        public override void Draw(Graphics g)
        {
            if (color == "Transparent")
            {
                Pen pen = new Pen(Color.Black, 3);
                g.DrawRectangle(pen, centre.X - w_r, centre.Y - h_r, 2 * w_r, 2 * h_r);
            }
            else
            {
                g.FillRectangle(Get_brush(), centre.X - w_r, centre.Y - h_r, 2 * w_r, 2 * h_r);
            }
            if (flag == 2)
            {
                Pen pen = new Pen(Color.Red, 2);
                pen.DashPattern = new float[] { 1, 1 };
                g.DrawPolygon(pen, frame_point);
            }
            else if (flag == 1)
            {
                Pen pen = new Pen(Color.Gray, 2);
                pen.DashPattern = new float[] { 1, 1 };
                g.DrawPolygon(pen, frame_point);
            }
        }
        public override void Increase(int w, int h)
        {
            w_r += 5;
            h_r += 3;
            frame_point[0].Y -= 3;
            frame_point[1].Y -= 3;
            frame_point[2].Y += 3;
            frame_point[3].Y += 3;
            frame_point[4].Y -= 3;
            frame_point[0].X += 5;
            frame_point[3].X += 5;
            frame_point[1].X -= 5;
            frame_point[2].X -= 5;
            frame_point[4].X += 5;
        }
        public override void Reduce()
        {
            if (h_r > 10)
            {
                w_r -= 5;
                h_r -= 3;
                frame_point[0].Y += 3;
                frame_point[1].Y += 3;
                frame_point[2].Y -= 3;
                frame_point[3].Y -= 3;
                frame_point[4].Y += 3;
                frame_point[0].X -= 5;
                frame_point[3].X -= 5;
                frame_point[1].X += 5;
                frame_point[2].X += 5;
                frame_point[4].X -= 5;
            }
        }
        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("rectangle");
            stream.WriteLine(color + " " + centre + " " + " " + flag + " " + w_r +" " + h_r);
        }
        public override void Load(StreamReader stream)
        {
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            color = data[0];
            string[] g = Regex.Replace(data[1], @"[\{\}a-zA-Z=]", "").Split(',');
            centre = new Point(int.Parse(g[0]), int.Parse(g[1]));
            flag = int.Parse(data[2]);
            w_r = int.Parse(data[3]);
            h_r = int.Parse(data[4]);
            frame_point = new Point[]
            {new Point(centre.X + w_r + 1, centre.Y - h_r - 1),
            new Point(centre.X - w_r - 1, centre.Y - h_r - 1),
            new Point(centre.X - w_r - 1, centre.Y + h_r + 1),
            new Point(centre.X + w_r + 1, centre.Y + h_r + 1),
            new Point(centre.X + w_r + 1, centre.Y - h_r - 1)};
        }
        public override string GetData()
        {
            return "Rectangle: " + centre.X.ToString() + " " + centre.Y.ToString();
        }
    }

    class CopyOfRectangle_ : Shape
    {
        private int w_r, h_r;
        public CopyOfRectangle_()
        {
            centre.X = 85;
            centre.Y = 65;
            w_r = 50;
            h_r = 30;
            color = "Red";
            frame_point = new Point[]
            {new Point(136, 34),
            new Point(34, 34),
            new Point(34, 96),
            new Point(136, 96),
            new Point(136, 34)};
        }
        public override void Draw(Graphics g)
        {
            if (color == "Transparent")
            {
                Pen pen = new Pen(Color.Black, 3);
                g.DrawRectangle(pen, centre.X - w_r, centre.Y - h_r, 2 * w_r, 2 * h_r);
            }
            else
            {
                g.FillRectangle(Get_brush(), centre.X - w_r, centre.Y - h_r, 2 * w_r, 2 * h_r);
            }
            if (flag == 2)
            {
                Pen pen = new Pen(Color.Red, 2);
                pen.DashPattern = new float[] { 1, 1 };
                g.DrawPolygon(pen, frame_point);
            }
            else if (flag == 1)
            {
                Pen pen = new Pen(Color.Gray, 2);
                pen.DashPattern = new float[] { 1, 1 };
                g.DrawPolygon(pen, frame_point);
            }
        }
        public override void Increase(int w, int h)
        {
            w_r += 5;
            h_r += 3;
            frame_point[0].Y -= 3;
            frame_point[1].Y -= 3;
            frame_point[2].Y += 3;
            frame_point[3].Y += 3;
            frame_point[4].Y -= 3;
            frame_point[0].X += 5;
            frame_point[3].X += 5;
            frame_point[1].X -= 5;
            frame_point[2].X -= 5;
            frame_point[4].X += 5;
        }
        public override void Reduce()
        {
            if (h_r > 10)
            {
                w_r -= 5;
                h_r -= 3;
                frame_point[0].Y += 3;
                frame_point[1].Y += 3;
                frame_point[2].Y -= 3;
                frame_point[3].Y -= 3;
                frame_point[4].Y += 3;
                frame_point[0].X -= 5;
                frame_point[3].X -= 5;
                frame_point[1].X += 5;
                frame_point[2].X += 5;
                frame_point[4].X -= 5;
            }
        }
        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("rectangle");
            stream.WriteLine(color + " " + centre + " " + " " + flag + " " + w_r + " " + h_r);
        }
        public override void Load(StreamReader stream)
        {
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            color = data[0];
            string[] g = Regex.Replace(data[1], @"[\{\}a-zA-Z=]", "").Split(',');
            centre = new Point(int.Parse(g[0]), int.Parse(g[1]));
            flag = int.Parse(data[2]);
            w_r = int.Parse(data[3]);
            h_r = int.Parse(data[4]);
            frame_point = new Point[]
            {new Point(centre.X + w_r + 1, centre.Y - h_r - 1),
            new Point(centre.X - w_r - 1, centre.Y - h_r - 1),
            new Point(centre.X - w_r - 1, centre.Y + h_r + 1),
            new Point(centre.X + w_r + 1, centre.Y + h_r + 1),
            new Point(centre.X + w_r + 1, centre.Y - h_r - 1)};
        }
        public override string GetData()
        {
            return "Rectangle: " + centre.X.ToString() + " " + centre.Y.ToString();
        }
    }
}





/* public override void Mouse_move(Point point, int w, int h)
       {
           if ((frame_point[1].X >= 0 && frame_point[3].X <= w) && (frame_point[1].Y >= 0 && frame_point[3].Y <= h)) {
               base.Mouse_move(point, w, h);
               for (int i=0; i<5; i++)
               {
                   frame_point[i].X += point.X - last_point.X;
                   frame_point[i].Y += point.Y - last_point.Y;
               }
               last_point.X = point.X;
               last_point.Y = point.Y;
           }
           if (frame_point[1].X < 0)
           {
               int a = frame_point[1].X;
               centre.X += 0 - frame_point[1].X;
               for (int i = 0; i < 5; i++)
               {
                   frame_point[i].X += 0 - a;
               }
           }
           if (frame_point[1].Y < 0)
           {
               int a = frame_point[1].Y;
               centre.Y += 0 - a;
               for (int i = 0; i < 5; i++)
               {
                   frame_point[i].Y += 0 - a;
               }
           }
           if (frame_point[3].X > w)
           {
               int a = frame_point[3].X;
               centre.X -= a - w;
               for (int i = 0; i < 5; i++)
               {
                   frame_point[i].X -= a - w;
               }
           }
           if (frame_point[3].Y > h)
           {
               int a = frame_point[3].Y;
               centre.Y -= a - h;
               for (int i = 0; i < 5; i++)
               {
                   frame_point[i].Y -= a - h;
               }
           }
       }*/