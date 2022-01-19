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
using System.Reflection;

namespace Grouping_and_saving
{
    public partial class Form1 : Form
    {
        private Vector<Shape> shape;
        private Tree tree;
        public Form1()
        {
            InitializeComponent();
            shape = new Vector<Shape>();
            tree = new Tree(shape, treeView1);
            shape.Add(tree);
            pictureBox6.MouseWheel += new MouseEventHandler(pictureBox6_MouseWheel);
        }
        private int current;
        private bool flag = false;
        private bool mouse_fl = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D0)
            {
                Shape _shape = new Circle();
                for (int i = 0; i < shape.Size(); i++)
                {
                    shape[i].Selected(0);
                }
                shape.push_back(_shape);
                current = shape.Size() - 1;
            }
            else if (e.KeyCode == Keys.D1)
            {
                Shape _shape = new Rectangle_();
                for (int i = 0; i < shape.Size(); i++)
                {
                    shape[i].Selected(0);
                }
                shape.push_back(_shape);
                current = shape.Size() - 1;
            }
            else if (e.KeyCode == Keys.D2)
            {
                Shape _shape = new Section();
                for (int i = 0; i < shape.Size(); i++)
                {
                    shape[i].Selected(0);
                }
                shape.push_back(_shape);
                current = shape.Size() - 1;
            }
            else if (e.KeyValue > 50 && e.KeyValue < 58)
            {
                int n = e.KeyValue - 48;
                Shape _shape = new Polygon(n);
                for (int i = 0; i < shape.Size(); i++)
                {
                    shape[i].Selected(0);
                }
                shape.push_back(_shape);
                current = shape.Size() - 1;
            }
            else
            {
                if (!shape.empty())
                {
                    if (e.Shift)
                    {
                        for (int i = 0; i < shape.Size(); i++)
                            if (shape[i].GetFlag() > 0)
                                if (!shape[i].Check_move(e, pictureBox6.Width, pictureBox6.Height))
                                    return;
                        for (int i = 0; i < shape.Size(); i++)
                            if (shape[i].GetFlag() > 0)
                                shape[i].Move(e, pictureBox6.Width, pictureBox6.Height);
                    }
                    else if (e.KeyCode == Keys.F)
                    {
                        flag = true;
                    }
                    else if (e.KeyCode == Keys.Right)
                    {
                        if (!flag)
                            for (int i = 0; i < shape.Size(); i++)
                                shape[i].Selected(0);
                        else
                        {
                            shape[current].Selected(1);
                        }
                        flag = false;
                        if (current == shape.Size() - 1)
                            current = 0;
                        else current++;
                        shape[current].Selected(2);
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        if (!flag)
                            for (int i = 0; i < shape.Size(); i++)
                                shape[current].Selected(0);
                        else
                        {
                            shape[current].Selected(1);
                        }
                        flag = false;
                        if (current == 0)
                            current = shape.Size() - 1;
                        else current--;
                        shape[current].Selected(2);
                    }
                    else if (e.KeyCode == Keys.Z)
                    {
                        for (int i = 0; i < shape.Size(); i++)
                            if (!shape[i].Check_increase(pictureBox6.Width, pictureBox6.Height))
                                return;
                        for (int i = 0; i < shape.Size(); i++)
                            if (shape[i].GetFlag() > 0)
                                shape[i].Increase(pictureBox6.Width, pictureBox6.Height);
                    }
                    else if (e.KeyCode == Keys.A)
                    {
                        for (int i = 0; i < shape.Size(); i++)
                            if (shape[i].GetFlag() > 0)
                                shape[i].Reduce();
                    }
                    else if (e.Control && e.KeyCode == Keys.Delete)
                    {
                        shape.clear();
                        current = 0;
                        shape.Notify();
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        for (int i = 0; i < shape.Size(); i++)
                            if (shape[i].GetFlag() > 0)
                                shape.pop(i);
                        if (!shape.empty())
                        {
                            current = 0;
                            shape[0].Selected(2);
                        }
                        shape.Notify();
                    }
                    else if (e.Control)
                    {
                        for (int i = 0; i < shape.Size(); i++)
                            if (shape[i].GetFlag() > 0)
                                shape[i].Choosed_color(e);
                    }
                    else if (e.KeyCode == Keys.S)
                    {
                        Group group = new Group();
                        for (int i = 0; i < shape.Size();)
                        {
                            if (shape[i].GetFlag() > 0)
                            {
                                group.AddShape(shape[i]);
                                shape.pop(i);
                            }
                            else
                                i++;
                        }
                        shape.push_back(group);
                        current = shape.Size() - 1;
                        flag = false;
                    }
                    else if (e.KeyCode == Keys.Q)
                    {
                        if (shape[current] is Group)
                        {
                            shape.push_back(((Group)shape[current]).Out());
                            shape.pop(current);
                            for (int i = 0; i < shape.Size(); i++)
                            {
                                if (shape[i].GetFlag() == 1)
                                {
                                    shape[i].Selected(2);
                                    current = i;
                                    break;
                                }
                            }
                        }
                        pictureBox6.Invalidate();
                    }
                }
            }
            pictureBox6.Invalidate();
        }
        private void pictureBox6_Paint(object sender, PaintEventArgs e)
        {
            if (!shape.empty())
            {
                for (int i = 0; i < shape.Size(); i++)
                {
                    if (i != current)
                        shape[i].Draw(e.Graphics);
                }
                shape[current].Draw(e.Graphics);
            }
        }
        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            if (!shape.empty())
            {
                int current_ = current;
                for (int i = 0; i < shape.Size(); i++)
                {
                    if (shape[i].Check(new Point(e.X, e.Y)))
                    {
                        current = i;
                        break;
                    }
                    if (i == shape.Size() - 1)
                    {
                        for (int j = 0; j < shape.Size(); j++)
                        {
                            if (shape[j].GetFlag() == 1)
                                shape[j].Selected(0);
                        }
                        pictureBox6.Invalidate();
                        return;
                    }
                }
                if (e.Button == MouseButtons.Left)
                {
                    shape[current_].Selected(0);
                    mouse_fl = true;
                }
                else
                {
                    shape[current_].Selected(1);
                }
                shape[current].Selected(2);
                pictureBox6.Invalidate();
                shape.Notify();
            }
        }
        private void pictureBox6_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_fl = false;
        }
        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_fl)
            {
                int last_elem = 0;
                for (int i = 0; i < shape.Size(); i++)
                    if (shape[i].GetFlag() > 0)
                    {
                        last_elem = i;
                        if (!shape[i].Check_mouse_move(new Point(e.X, e.Y), pictureBox6.Width, pictureBox6.Height))
                            return;
                    }
                for (int i = 0; i < shape.Size(); i++)
                    if (shape[i].GetFlag() > 0)
                    {
                        if (i == last_elem)
                        {
                            shape[last_elem].Mouse_move(new Point(e.X, e.Y), pictureBox6.Width, pictureBox6.Height, true);
                        }
                        else
                        {
                            shape[i].Mouse_move(new Point(e.X, e.Y), pictureBox6.Width, pictureBox6.Height, false);
                        }
                    }
                pictureBox6.Invalidate();
            }
        }
        private void pictureBox6_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                for (int i = 0; i < shape.Size(); i++)
                    if (!shape[i].Check_increase(pictureBox6.Width, pictureBox6.Height))
                        return;
                for (int i = 0; i < shape.Size(); i++)
                    if (shape[i].GetFlag() > 0)
                        shape[i].Increase(pictureBox6.Width, pictureBox6.Height);
            }
            else
            {
                for (int i = 0; i < shape.Size(); i++)
                    if (shape[i].GetFlag() > 0)
                        shape[i].Reduce();
            }
            pictureBox6.Invalidate();
        }
        private void label11_Click(object sender, EventArgs e)
        {
            Shape _shape = new Circle();
            for (int i = 0; i < shape.Size(); i++)
            {
                shape[i].Selected(0);
            }
            shape.push_back(_shape);
            current = shape.Size() - 1;
            pictureBox6.Invalidate();
            shape.Notify();
        }
        private void label12_Click(object sender, EventArgs e)
        {
            Shape _shape = new Rectangle_();
            for (int i = 0; i < shape.Size(); i++)
            {
                shape[i].Selected(0);
            }
            shape.push_back(_shape);
            current = shape.Size() - 1;
            pictureBox6.Invalidate();
            shape.Notify();
        }
        private void label14_Click(object sender, EventArgs e)
        {
            Shape _shape = new Section();
            for (int i = 0; i < shape.Size(); i++)
            {
                shape[i].Selected(0);
            }
            shape.push_back(_shape);
            current = shape.Size() - 1;
            pictureBox6.Invalidate();
            shape.Notify();
        }
        private void Polygon_Click(object sender, EventArgs e)
        {
            int lenght = (sender as Label).Text.Length;
            int n = (sender as Label).Text[lenght - 1] - 48;
            Shape _shape = new Polygon(n);
            for (int i = 0; i < shape.Size(); i++)
            {
                shape[i].Selected(0);
            }
            shape.push_back(_shape);
            current = shape.Size() - 1;
            pictureBox6.Invalidate();
            shape.Notify();
        }
        private void label22_Click(object sender, EventArgs e)
        {
            Group group = new Group();
            for (int i = 0; i < shape.Size();)
            {
                if (shape[i].GetFlag() > 0)
                {
                    group.AddShape(shape[i]);
                    shape.pop(i);
                }
                else
                    i++;
            }
            shape.push_back(group);
            current = shape.Size() - 1;
            flag = false;
            pictureBox6.Invalidate();
            shape.Notify();
        }
        private void label23_Click(object sender, EventArgs e)
        {
            if (shape[current] is Group)
            {
                int _size = shape.Size();
                shape.push_back(((Group)shape[current]).Out());
                shape.pop(current);
                for (int i = _size - 1; i < shape.Size(); i++)
                {
                    if (shape[i].GetFlag() == 1)
                    {
                        shape[i].Selected(2);
                        current = i;
                        break;
                    }
                }
            }
            pictureBox6.Invalidate();
            shape.Notify();
        }
        private void Color_Click(object sender, EventArgs e)
        {
            char str = (sender as Label).Text[0];
            for (int i = 0; i < shape.Size(); i++)
                if (shape[i].GetFlag() > 0)
                    shape[i].Color_click(str);
            pictureBox6.Invalidate();
        }
        private void label24_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < shape.Size();)
                if (shape[i].GetFlag() > 0)
                    shape.pop(i);
                else i++;
            if (!shape.empty())
            {
                current = 0;
                shape[0].Selected(2);
            }
            shape.Notify();
            pictureBox6.Invalidate();
        }
        private void label25_Click(object sender, EventArgs e)
        {
            shape.clear();
            current = 0;
            shape.Notify();
            pictureBox6.Invalidate();
        }
        private void label26_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream f = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                StreamWriter stream = new StreamWriter(f);
                stream.WriteLine(current);
                stream.WriteLine(flag);
                stream.WriteLine(mouse_fl);
                stream.WriteLine(shape.Size());
                if (shape.Size() != 0)
                {
                    for (int i = 0; i < shape.Size(); i++)
                    {
                        shape[i].Save(stream);
                    }
                    stream.Close();
                    f.Close();
                }
            }
        }

        Factory shape_factory = new ShapeFactory();
        private void label27_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                shape.clear();
                FileStream f = new FileStream(openFileDialog1.FileName, FileMode.Open);
                StreamReader stream = new StreamReader(f);
                current = Convert.ToInt32(stream.ReadLine());
                flag = bool.Parse(stream.ReadLine());
                mouse_fl = bool.Parse(stream.ReadLine());
                int size_ = Convert.ToInt32(stream.ReadLine());
                shape.reserve(size_);
                Shape shape_;
                for (int i = 0; i < size_; i++)
                {
                    string name = stream.ReadLine();
                    shape_ = shape_factory.createShape(name);
                    shape_.Load(stream);
                    if (shape_ is Group)
                    {
                        int size_group = Int32.Parse(stream.ReadLine());
                        Load_on_form(size_group, stream, (Group)shape_);
                    }
                    shape.push_back(shape_);
                }
                stream.Close();
                f.Close();
                shape.Notify();
            }
            pictureBox6.Invalidate();
        }
        private void Load_on_form(int size_group, StreamReader stream, Group group)
        {
            Shape shape_;
            for (int i = 0; i < size_group; i++)
            {
                string name = stream.ReadLine();
                shape_ = shape_factory.createShape(name);
                shape_.Load(stream);
                if (shape_ is Group)
                {
                    Group group_ = (Group)shape_;
                    size_group = Int32.Parse(stream.ReadLine());
                    Load_on_form(size_group, stream, group_);
                }
                group.AddShape(shape_);
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse && e.Node.Text != "Shape")
            {
                TreeNode node = e.Node;
                while (node.Parent.Text != "Shape")
                    node = node.Parent;
                treeView1.SelectedNode = node;
                shape[current].Selected(0);
                current = node.Index;
                shape[current].Selected(2);
                pictureBox6.Invalidate();
            }
        }
    }
}






















































/*
 * 
 * 
 * 
 * /* private void bt_Cicrle_Click(object sender, EventArgs e)
        {
            Shape _shape = new Circle();
            shape.push_back(ref _shape);
            Invalidate();
        }
* private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!shape.empty())
                shape[0].Draw(e.Graphics);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < shape.Size(); i++)
            {
                if (shape[i].Click(e.Location))
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                shape[0].Move(e.Location);
                pictureBox1.Invalidate();
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            flag = false;
        }

        private void bt_Cicrle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                Shape _shape = new Circle();
                shape.push_back(ref _shape);
                pictureBox1.Invalidate();
            }
        }*/
