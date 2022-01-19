using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace Grouping_and_saving
{
    class Circle : Shape
    {
        private int r, R;
        public Circle()
        {
            centre.X = 55;
            centre.Y = 55;
            r = 40;
            R = (int)(40 * Math.Sqrt(2));
            color = "Red";
            frame_point = new Point[5];
            frame_point = Build_polygon(5, R);
        }
        public override void Draw(Graphics g)
        {
            if (color == "Transparent")
            {
                Pen pen = new Pen(Color.Black, 2);
                g.DrawEllipse(pen, centre.X - r, centre.Y - r, 2 * r, 2 * r);
            }
            else
            {
                g.FillEllipse(Get_brush(), centre.X - r, centre.Y - r, 2 * r, 2 * r);
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
            r += 4;
            R = (int)(r * Math.Sqrt(2));
            frame_point = Build_polygon(frame_point.Length, R);
        }
        public override void Reduce()
        {
            if (r > 10)
            {
                r -= 4;
                R = (int)(r * Math.Sqrt(2));
                frame_point = Build_polygon(frame_point.Length, R);
            }
        }
        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("circle");
            stream.WriteLine(color + " " + centre + " " + " " + flag + " " + r + " " + R);

        }
        public override void Load(StreamReader stream)
        {
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            color = data[0];
            string[] g = Regex.Replace(data[1], @"[\{\}a-zA-Z=]", "").Split(',');
            centre = new Point(int.Parse(g[0]), int.Parse(g[1]));
            flag = int.Parse(data[2]);
            r = int.Parse(data[3]); 
            R = int.Parse(data[4]);
            frame_point = Build_polygon(5, R);
        }
        public override string GetData()
        {
            return "Circle: " + centre.X.ToString() + " " + centre.Y.ToString();
        }
    }

    class CopyOfCircle : Shape
    {
        private int r, R;
        public CopyOfCircle()
        {
            centre.X = 55;
            centre.Y = 55;
            r = 40;
            R = (int)(40 * Math.Sqrt(2));
            color = "Red";
            frame_point = new Point[5];
            frame_point = Build_polygon(5, R);
        }
        public override void Draw(Graphics g)
        {
            if (color == "Transparent")
            {
                Pen pen = new Pen(Color.Black, 2);
                g.DrawEllipse(pen, centre.X - r, centre.Y - r, 2 * r, 2 * r);
            }
            else
            {
                g.FillEllipse(Get_brush(), centre.X - r, centre.Y - r, 2 * r, 2 * r);
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
            r += 4;
            R = (int)(r * Math.Sqrt(2));
            frame_point = Build_polygon(frame_point.Length, R);
        }
        public override void Reduce()
        {
            if (r > 10)
            {
                r -= 4;
                R = (int)(r * Math.Sqrt(2));
                frame_point = Build_polygon(frame_point.Length, R);
            }
        }
        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("circle");
            stream.WriteLine(color + " " + centre + " " + " " + flag + " " + r + " " + R);

        }
        public override void Load(StreamReader stream)
        {
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            color = data[0];
            string[] g = Regex.Replace(data[1], @"[\{\}a-zA-Z=]", "").Split(',');
            centre = new Point(int.Parse(g[0]), int.Parse(g[1]));
            flag = int.Parse(data[2]);
            r = int.Parse(data[3]);
            R = int.Parse(data[4]);
            frame_point = Build_polygon(5, R);
        }
        public override string GetData()
        {
            return "CopyOfCircle: " + centre.X.ToString() + " " + centre.Y.ToString();
        }
    }
}





















/* public override void Mouse_move(Point point, int w, int h)
       {
           if ((frame_point[1].X >= 0 && frame_point[3].X <= w) && (frame_point[1].Y >= 0 && frame_point[3].Y <= h))
           {
               base.Mouse_move(point, w, h);
               frame_point = Build_polygon(5, R);
               last_point.X = point.X;
               last_point.Y = point.Y;
           }
           if (frame_point[1].X < 0)
           {
               centre.X += r - centre.X;
               frame_point = Build_polygon(5, R);
           }
           if (frame_point[1].Y < 0)
           {
               centre.Y += r - centre.Y;
               frame_point = Build_polygon(5, R);
           }
           if (frame_point[3].X > w)
           {
               centre.X -= r - w + centre.X;
               frame_point = Build_polygon(5, R);
           }
           if (frame_point[3].Y > h)
           {
               centre.Y -= r - h + centre.Y;
               frame_point = Build_polygon(5, R);
           }*/