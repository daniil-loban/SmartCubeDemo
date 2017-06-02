using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    class ListCubes
    {

        public ObservableCollection<ListElement> Elements { get; private set; }
        public ListCubes()
        {
            Elements = new ObservableCollection<ListElement>();

        }

        
        public void Clear()
        {
            Elements.Clear();
        }

        public void AddElement(Resource.Type list, int idx)
        {
            Elements.Add(new ListElement(list, idx));

        }

        public void RemoveElement(ListElement el)
        {
            Elements.Remove(el);

        }

    }
}
