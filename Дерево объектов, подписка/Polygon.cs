using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Grouping_and_saving
{
    class Polygon : Shape
    {
        public Polygon()
        {
            r = 0;
            arr_point = null;
            frame_point = null;
            flag = -1;
            centre.X = 0;
            centre.Y = 0;
            color = "";

        }
        private int r;
        protected Point[] arr_point;
        public Polygon(int _n)
        {
            arr_point = new Point[_n + 1];
            frame_point = new Point[5];
            centre.X = 75;
            centre.Y = 75;
            r = 60;
            color = "Red";
            arr_point = Build_polygon(arr_point.Length, r);
            Build_frame();
        }
        public void Build_frame()
        {
            int a = 0;
            for (int i = 0; i < arr_point.Length; i++)
            {
                if (a < arr_point[i].X)
                {
                    a = arr_point[i].X;
                }
            }
            frame_point[0].X = a + 1;
            frame_point[3].X = a + 1;
            frame_point[4].X = a + 1;
            for (int i = 0; i < arr_point.Length; i++)
            {
                if (a > arr_point[i].X)
                {
                    a = arr_point[i].X;
                }
            }
            frame_point[1].X = a - 1;
            frame_point[2].X = a - 1;
            a = 0;
            for (int i = 0; i < arr_point.Length; i++)
            {
                if (a < arr_point[i].Y)
                {
                    a = arr_point[i].Y;
                }
            }
            frame_point[2].Y = a + 1;
            frame_point[3].Y = a + 1;
            for (int i = 0; i < arr_point.Length; i++)
            {
                if (a > arr_point[i].Y)
                {
                    a = arr_point[i].Y;
                }
            }
            frame_point[0].Y = a - 1;
            frame_point[1].Y = a - 1;
            frame_point[4].Y = a - 1;
        }
        public override void Draw(Graphics g)
        {
            if (color == "Transparent")
            {
                Pen pen = new Pen(Color.Black, 3);
                g.DrawPolygon(pen, arr_point);
            }
            else
            {
                g.FillPolygon(Get_brush(), arr_point);
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
        public override void Move(KeyEventArgs e, int w, int h)
        {
            if (e.KeyCode == Keys.Left)
            {
                for (int i = 0; i < arr_point.Length; i++)
                {
                    arr_point[i].X -= 10;
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                for (int i = 0; i < arr_point.Length; i++)
                {
                    arr_point[i].X += 10;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                for (int i = 0; i < arr_point.Length; i++)
                {
                    arr_point[i].Y -= 10;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                for (int i = 0; i < arr_point.Length; i++)
                {
                    arr_point[i].Y += 10;
                }
            }
            base.Move(e, w, h);
        }
        public override void Increase(int w, int h)
        {
            r += 4;
            arr_point = Build_polygon(arr_point.Length, r);
            Build_frame();
        }
        public override void Reduce()
        {
            if (r > 30)
            {
                r -= 4;
                arr_point = Build_polygon(arr_point.Length, r);
                Build_frame();
            }
        }
        public override void Mouse_move(Point point, int w, int h, bool change)
        {
            base.Mouse_move(point, w, h, change);
            arr_point = Build_polygon(arr_point.Length, r);
        }
        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("polygon");
            stream.WriteLine(color + " " + centre + " " + " " + flag + " "+ r + " " + arr_point.Length);

        }

        public override void Load(StreamReader stream)
        {
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            color = data[0];
            string[] g = Regex.Replace(data[1], @"[\{\}a-zA-Z=]", "").Split(',');
            centre = new Point(int.Parse(g[0]), int.Parse(g[1]));
            flag = int.Parse(data[2]);
            r = int.Parse(data[3]);
            int i = int.Parse(data[4]);
            arr_point = new Point[i];
            arr_point = Build_polygon(i, r);
            frame_point = new Point[5];
            Build_frame();
        }
        public override string GetData()
        {
            return "Polygon: " + centre.X.ToString() + " " + centre.Y.ToString();
        }
    }
}