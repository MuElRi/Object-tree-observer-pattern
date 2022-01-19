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
namespace Grouping_and_saving
{
    class Group : Shape
    {
        Vector<Shape> shapes;
        public Group()
        {
            shapes = new Vector<Shape>();
            frame_point = new Point[5];
        }
        public void AddShape(Shape shape)
        {
            shape.Selected(0);
            shapes.push_back(ref shape);
            Build_frame();
        }
        protected void Build_frame()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < shapes.Size(); i++)
            {
                if (shapes[i].get_frame_point(3).X > x)
                {
                    x = shapes[i].get_frame_point(3).X;
                }
                if (shapes[i].get_frame_point(3).Y > y)
                {
                    y = shapes[i].get_frame_point(3).Y;
                }
            }
            frame_point[0].X = x + 1;
            frame_point[3].X = x + 1;
            frame_point[4].X = x + 1;
            frame_point[2].Y = y + 1;
            frame_point[3].Y = y + 1;
            for (int i = 0; i < shapes.Size(); i++)
            {
                if (shapes[i].get_frame_point(1).X < x)
                {
                    x = shapes[i].get_frame_point(1).X;
                }
                if (shapes[i].get_frame_point(1).Y < y)
                {
                    y = shapes[i].get_frame_point(1).Y;
                }
            }
            frame_point[1].X = x - 1;
            frame_point[2].X = x - 1;
            frame_point[0].Y = y - 1;
            frame_point[1].Y = y - 1;
            frame_point[4].Y = y - 1;
        }
        public override void Draw(Graphics g)
        {

            for (int i = 0; i < shapes.Size(); i++)
                shapes[i].Draw(g);
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
            for (int i = 0; i < shapes.Size(); i++)
                if (!shapes[i].Check_increase(w, h))
                    return;
            for (int i = 0; i < shapes.Size(); i++)
                shapes[i].Increase(w, h);
            Build_frame();
        }
        public override void Reduce()
        {
            for (int i = 0; i < shapes.Size(); i++)
                shapes[i].Reduce();
            Build_frame();
        }
        public override void Choosed_color(KeyEventArgs e)
        {
            for (int i = 0; i < shapes.Size(); i++)
                shapes[i].Choosed_color(e);
        }
        public override void Color_click(char str)
        {
            for (int i = 0; i < shapes.Size(); i++)
                shapes[i].Color_click(str);
        }
        public override void Move(KeyEventArgs e, int w, int h)
        {
            if (e.KeyCode == Keys.Left)
            {
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].X -= 10;
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].X += 10;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].Y -= 10;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].Y += 10;
                }
            }
            for (int i = 0; i < shapes.Size(); i++)
                shapes[i].Move(e, w, h);
        }
        public Vector<Shape> Out()
        {
            for (int i = 0; i < shapes.Size(); i++)
                shapes[i].Selected(1);
            return shapes;
        }
        public int Size()
        {
            return shapes.Size();
        }
        public override void Mouse_move(Point point, int w, int h, bool change)
        {
            for (int i = 0; i < 5; i++)
            {
                frame_point[i].X += point.X - last_point.X;
                frame_point[i].Y += point.Y - last_point.Y;
            }
            if (frame_point[1].X < 0)
            {
                int a = frame_point[1].X;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].X += 0 - a;
                }
            }
            if (frame_point[1].Y < 0)
            {
                int a = frame_point[1].Y;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].Y += 0 - a;
                }
            }
            if (frame_point[3].X > w)
            {
                int a = frame_point[3].X;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].X -= a - w;
                }
            }
            if (frame_point[3].Y > h)
            {
                int a = frame_point[3].Y;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].Y -= a - h;
                }
            }
            for (int i = 0; i < shapes.Size() - 1; i++)
            {
                shapes[i].Mouse_move(point, w, h, false);
            }
            shapes.back().Mouse_move(point, w, h, change);
        }
        public override bool Check(Point point)
        {
            for (int i = 0; i < shapes.Size(); i++)
            {
                if (shapes[i].Check(point))
                    return true;
            }
            return false;
        }
        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("group");
            stream.WriteLine(flag);
            stream.WriteLine(shapes.Size());
            for (int i = 0; i < shapes.Size(); i++)
                shapes[i].Save(stream);
        }
        public override void Load(StreamReader stream)
        {
            flag = Int32.Parse(stream.ReadLine());
            shapes = new Vector<Shape>();
        }

        public Shape GetShape(int index)
        {
            if (index + 1 > shapes.Size())
                return null;
            else
                return shapes[index];
        }
        public override string GetData()
        {
            return "Group";
        }
        public int GetSize()
        {
            return shapes.Size();
        }
    }
}
