using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Grouping_and_saving
{
    class Section : Shape
    {
        private Point centre_;
        public Section()
        {

            centre.X = 30;
            centre.Y = 30;
            centre_.X = 70;
            centre_.Y = 30;
            color = "Red";
            frame_point = new Point[]
            {new Point(80, 20),
            new Point(20, 20),
            new Point(20, 40),
            new Point(80, 40),
            new Point(80, 20)};
        }
        public override void Draw(Graphics g)
        {
            if (flag == 2)
            {
                Pen pen_ = new Pen(Color.Red, 2);
                pen_.DashPattern = new float[] { 1, 1 };
                g.DrawPolygon(pen_, frame_point);
            }
            else if (flag == 1)
            {
                Pen pen_ = new Pen(Color.Gray, 2);
                pen_.DashPattern = new float[] { 1, 1 };
                g.DrawPolygon(pen_, frame_point);
            }
            Pen pen = new Pen(Get_color(), 3);
            g.DrawLine(pen, centre, centre_);
        }
        public override void Increase(int w, int h)
        {
                centre.X -= 5;
                centre_.X += 5;
                frame_point[0].X += 5;
                frame_point[3].X += 5;
                frame_point[1].X -= 5;
                frame_point[2].X -= 5;
                frame_point[4].X += 5;
        }
        public override void Reduce()
        {
            if (centre_.X - centre.X > 20)
            {
                centre.X += 5;
                centre_.X -= 5;
                frame_point[0].X -= 5;
                frame_point[3].X -= 5;
                frame_point[1].X += 5;
                frame_point[2].X += 5;
                frame_point[4].X -= 5;
            }
        }
        public override void Move(KeyEventArgs e, int w, int h)
        {
            if (e.KeyCode == Keys.Left)
            {
                centre_.X -= 10;
            }
            else if (e.KeyCode == Keys.Right)
            { 
                centre_.X += 10;
            }
            else if (e.KeyCode == Keys.Up)
            {
                centre_.Y -= 10;
            }
            else if (e.KeyCode == Keys.Down)
            {
                centre_.Y += 10;
            }
            base.Move(e, w, h);
        }
        public override void Mouse_move(Point point, int w, int h, bool change)
        {
            centre.X += point.X - last_point.X;
            centre.Y += point.Y - last_point.Y;
            centre_.X += point.X - last_point.X;
            centre_.Y += point.Y - last_point.Y;
            for (int i = 0; i < 5; i++)
            {
                frame_point[i].X += point.X - last_point.X;
                frame_point[i].Y += point.Y - last_point.Y;
            }
            if (change)
            {
                last_point.X = point.X;
                last_point.Y = point.Y;
            }
            if (frame_point[1].X < 0)
            {
                int a = frame_point[1].X;
                centre.X += 0 - a;
                centre_.X += 0 - a;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].X += 0 - a;
                }
            }
            if (frame_point[1].Y < 0)
            {
                int a = frame_point[1].Y;
                centre.Y += 0 - a;
                centre_.Y += 0 - a;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].Y += 0 - a;
                }
            }
            if (frame_point[3].X > w)
            {
                int a = frame_point[3].X;
                centre.X -= a - w;
                centre_.X -= a - w;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].X -= a - w;
                }
            }
            if (frame_point[3].Y > h)
            {
                int a = frame_point[3].Y;
                centre.Y -= a - h;
                centre_.Y -= a - h;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].Y -= a - h;
                }
            }
        }
        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("section");
            stream.WriteLine(color + " " + centre  + " " + flag + " " + centre_);
        }
        public override void Load(StreamReader stream)
        {
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            color = data[0];
            string[] g = Regex.Replace(data[1], @"[\{\}a-zA-Z=]", "").Split(',');
            centre = new Point(int.Parse(g[0]), int.Parse(g[1]));
            flag = int.Parse(data[2]);
            g = Regex.Replace(data[3], @"[\{\}a-zA-Z=]", "").Split(',');
            centre_ = new Point(int.Parse(g[0]), int.Parse(g[1]));
            frame_point = new Point[]
            {new Point(centre_.X + 10, centre_.Y - 10),
            new Point(centre.X - 10, centre.Y - 10),
            new Point(centre.X - 10, centre.Y + 10),
            new Point(centre_.X + 10, centre_.Y + 10),
            new Point(centre_.X + 10, centre_.Y - 10)};
        }
        public override string GetData()
        {
            return "Section: " + ((centre.X + centre_.X)/2).ToString() + " " + centre.Y.ToString();
        }
    }
}
