using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    class ListMap
    {

        public ObservableCollection< ListElement> Elements { get; private set;}
        public ListMap()
        {
            Elements = new ObservableCollection<ListElement>();

        }

        
        public void  AddElement(Resource.Type list, int idx)
        {
            Elements.Add(new ListElement(list, idx));

        }


        public void RemoveElement(ListElement el)
        {
            Elements.Remove(el);

        }

        public void Clear()
        {
            Elements.Clear();
        }
    }
}
