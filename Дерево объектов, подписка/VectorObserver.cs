using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouping_and_saving
{
    class VectorObserved
    {
        protected Vector<TreeObserver> observers;
        public void Add(TreeObserver o)
        {
            if (observers != null)
            {
                observers.push_back(ref o);
            }
            else
            {
                observers = new Vector<TreeObserver>();
                observers.push_back(ref o);
            }
        }
        public void Notify()
        {
            for (int i = 0; i < observers.Size(); i++)
                observers[i].SubjectChanged();
        }
    }
}
