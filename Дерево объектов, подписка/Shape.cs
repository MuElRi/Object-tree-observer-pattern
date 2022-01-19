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
    abstract class Shape: ObjectObserved
    {
        protected bool sticky = false;
        protected Point centre;
        static protected Point last_point;
        protected Point[] frame_point;
        protected int flag = 2;
        protected string color;
        public virtual void Selected(int _flag)
        {
            flag = _flag;
        }
        public int GetFlag()
        {
            return flag;
        }
        public bool Check_move(KeyEventArgs e, int w, int h)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (frame_point[1].X <= 10)
                {
                    return false;
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (frame_point[3].X >= w - 10)
                {
                    return false;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (frame_point[1].Y <= 10)
                {
                    return false;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (frame_point[3].Y >= h - 10)
                {
                    return false;
                }
            }
            return true;
        }
        public bool Check_mouse_move(Point point, int w, int h)
        {
            if(frame_point[1].X == 0)
            {
                if(last_point.X - point.X > 0)
                {
                    return false;
                }
            }
            else if (frame_point[1].Y == 0)
            {
                if (last_point.Y - point.Y > 0)
                {
                    return false;
                }
            }
            else if (frame_point[3].X == w)
            {
                if (last_point.X - point.X < 0)
                {
                    return false;
                }
            }
            else if (frame_point[3].Y == h)
            {
                if (last_point.Y - point.Y < 0)
                {
                    return false;
                }
            }
            return true;
        }
        public virtual void Move(KeyEventArgs e, int w, int h)
        {
            if (e.KeyCode == Keys.Left)
            {
                centre.X -= 10;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].X -= 10;
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                centre.X += 10;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].X += 10;
                }

            }
            else if (e.KeyCode == Keys.Up)
            {
                centre.Y -= 10;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].Y -= 10;
                }

            }
            else if (e.KeyCode == Keys.Down)
            { 
                centre.Y += 10;
                for (int i = 0; i < 5; i++)
                {
                    frame_point[i].Y += 10;
                }
            }
        }
        public virtual void Choosed_color(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
                color = "Red";
            else if (e.KeyCode == Keys.O)
                color = "Orange";
            else if (e.KeyCode == Keys.N)
                color = "Navy";
            else if (e.KeyCode == Keys.C)
                color = "Cyan";
            else if (e.KeyCode == Keys.Y)
                color = "Yellow";
            else if (e.KeyCode == Keys.L)
                color = "Lime";
            else if (e.KeyCode == Keys.P)
                color = "Purple";
            else if (e.KeyCode == Keys.G)
                color = "Gray";
            else if (e.KeyCode == Keys.B)
                color = "Black";
            else if (e.KeyCode == Keys.T)
                color = "Transparent";
        }
        protected Brush Get_brush()
        {
            if (color == "Red")
                return Brushes.Red;
            else if (color == "Orange")
                return Brushes.Orange;
            else if (color == "Navy")
                return Brushes.Navy;
            else if (color == "Cyan")
                return Brushes.Cyan;
            else if (color == "Yellow")
                return Brushes.Yellow;
            else if (color == "Lime")
                return Brushes.Lime;
            else if (color == "Purple")
                return Brushes.Purple;
            else if (color == "Gray")
                return Brushes.Gray;
            else
                return Brushes.Black;
        }
        protected Color Get_color()
        {
            if (color == "Red")
                return Color.Red;
            else if (color == "Orange")
                return Color.Orange;
            else if (color == "Navy")
                return Color.Navy;
            else if (color == "Cyan")
                return Color.Cyan;
            else if (color == "Yellow")
                return Color.Yellow;
            else if (color == "Lime")
                return Color.Lime;
            else if (color == "Purple")
                return Color.Purple;
            else if (color == "Gray")
                return Color.Gray;
            else
                return Color.Black;
        }
        public virtual void Color_click(char str)
        {
            if (str == 'R')
                color = "Red";
            else if (str == 'O')
                color = "Orange";
            else if (str == 'N')
                color = "Navy";
            else if (str == 'C')
                color = "Cyan";
            else if (str == 'Y')
                color = "Yellow";
            else if (str == 'L')
                color = "Lime";
            else if (str == 'P')
                color = "Purple";
            else if (str == 'G')
                color = "Gray";
            else if (str == 'B')
                color = "Black";
            else if (str == 'T')
                color = "Transparent";
        }
        public abstract void Save(StreamWriter stream);
        public abstract void Load(StreamReader stream);
        public abstract void Draw(Graphics g);
        public bool Check_increase(int w, int h)
        {
            if (frame_point[3].X >= w - 5 || frame_point[3].Y >= h - 5)
                return false;
            else if (frame_point[1].X <= 5 || frame_point[1].Y <= 5)
                return false;
            return true;
        }
        public abstract void Increase(int w, int h);
        public abstract void Reduce();
        protected Point[] Build_polygon(int n, int _r)
        {
            double z = 90;
            if (n == 5 || n == 3)
                z = 45;
            double angle = 360 / (n - 1);
            int i = 0;
            Point[] t_point = new Point[n];
            while (i < n)
            {
                t_point[i].X = centre.X + (int)(Math.Round(Math.Cos(z / 180 * Math.PI) * _r));
                t_point[i].Y = centre.Y - (int)(Math.Round(Math.Sin(z / 180 * Math.PI) * _r));
                z = z + angle;
                i++;
            }
            return t_point;
        }
        public virtual bool Check(Point point)
        {
            if ((point.X > frame_point[1].X && point.X < frame_point[3].X) && (point.Y > frame_point[1].Y && point.Y < frame_point[3].Y))
            {
                last_point.X = point.X;
                last_point.Y = point.Y;
                return true;
            }
            else return false;
        }
        public virtual void Mouse_move(Point point, int w, int h, bool change)
        {
            centre.X += point.X - last_point.X;
            centre.Y += point.Y - last_point.Y;
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
        }
        public Point get_frame_point(int index)
        {
            return frame_point[index];
        }
        public abstract string GetData();
        public bool GetSticky()
        {
            return sticky;
        }
        public void SetSticky(bool f)
        {
            sticky = f;
        }
        public void Find(Shape shape)
        {
            for (int i = 0; i < 4; i++)
            {
                if (shape.frame_point[i].X > frame_point[1].X && shape.frame_point[i].X < frame_point[3].X)
                    if (shape.frame_point[i].Y > frame_point[1].Y && shape.frame_point[i].Y < frame_point[3].Y)
                    {
                        flag = 1;
                        return;
                    }
            }
            for (int i = 0; i < 4; i++)
            {
                if (shape.frame_point[1].X < frame_point[i].X && shape.frame_point[3].X > frame_point[i].X)
                    if (shape.frame_point[1].Y < frame_point[i].Y && shape.frame_point[3].Y > frame_point[i].Y)
                    {
                        flag = 1;
                        return;
                    }
            }
        }
    }
}