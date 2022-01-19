using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouping_and_saving
{
    class ObjectObserved
    {
        Vector<Shape> observers;
        public void Add(Vector<Shape> shapes)
        {
            if (observers == null)
                observers = shapes;
        }
        public void Notify(int current)
        {
            for (int i = 0; i < observers.Size(); i++)
                observers[i].Find(observers[current]);
        }

    }
}

    
