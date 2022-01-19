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
    class Tree : TreeObserver
    {
        private Vector<Shape> shapes;
        private TreeView tree;
        public Tree(Vector<Shape>  shapes_, TreeView tree_)
        {
            shapes = shapes_;
            tree = tree_;
        }

        public void Print()
        {
            tree.Nodes.Clear();
            if (!shapes.empty())
            {
                TreeNode start = new TreeNode("Shape");
                for (int i = 0; i < shapes.Size(); i++)
                {
                    PrintNode(start, shapes[i]);
                }
                tree.Nodes.Add(start);
                for (int i = 0; i < shapes.Size(); i++)
                {
                    if (shapes[i].GetFlag() == 2)
                        tree.SelectedNode = tree.Nodes[0].Nodes[i];
                }
                tree.ExpandAll();
            }
        }
        public void PrintNode(TreeNode node, Shape shape)
        {
            if (shape is Group)
            {
                TreeNode tn = new TreeNode(shape.GetData());
                for (int i = 0; i < ((Group)shape).GetSize(); i++)
                {
                    PrintNode(tn, ((Group)shape).GetShape(i));
                }
                node.Nodes.Add(tn);
            }
            else
            {
                node.Nodes.Add(shape.GetData());
            }
        }
        public override void SubjectChanged()
        {
            Print();
        }
    }
}



      
