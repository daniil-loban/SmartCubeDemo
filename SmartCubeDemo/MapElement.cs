using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SmartCubeDemo
{
    class ListElement: INotifyPropertyChanged
    {
        
        private string _name;
        private Uri _image;

        public string Name {
            get
            {
            return this._name;
            }

            private  set
            {
                if (value != this._name)
                {
                    this._name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Uri Image {
            get {
                return this._image;
            }

            private set {
                if (value != this._image)
                {
                    this._image = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Resource.Type Type { get; private set; }
        public int Index { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Next()
        {
            Resource.Data rs = Resource. GetResource(Type);
            if (rs.images.Count-1 == Index) 
                Index = -1;
            ++Index;
            Image = rs.images[Index].image;
            Name = rs.images[Index].name;

        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ListElement(Resource.Type type, int idx)
        {
            Resource.Data lu= Resource.GetResource(type);
            Index = idx;
            Image = lu.images[idx].image;
            Name = lu.images[idx].name;
            Type = type;
        }





    }
}
