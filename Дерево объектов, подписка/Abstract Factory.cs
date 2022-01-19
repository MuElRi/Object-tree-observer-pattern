using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouping_and_saving
{
    abstract class Factory
    {
        public abstract Shape createShape(string name);
    }
    class ShapeFactory : Factory
    {
        public override Shape createShape(string name)
        {
            Shape shape;
            switch (name)
            {
                case "circle":
                    shape = new Circle();
                    break;
                case "rectangle":
                    shape = new Rectangle_();
                    break;
                case "group":
                    shape = new Group();
                    break;
                case "polygon":
                    shape = new Polygon();
                    break;
                case "section":
                    shape = new Section();
                    break;
                default:
                    shape = null;
                    break;
            }
            return shape;
        }
    }

}
