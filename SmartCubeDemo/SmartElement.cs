using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SmartCubeDemo
{

    public abstract class SmartElement
    {
        public Image curr_img { get; private set; }
        public double width { get; private set; }
        public double height { get; private set; }
        Resource.Data data;

        public virtual new Resource.Type GetType()
        {
            return Resource.Type.None;
        }

        public virtual MapEngine.TypeMap GetMapType()
        {
            return MapEngine.TypeMap.None;
        }

        protected virtual void MouseUpFunction()
        {
            if (data.current_index <= data.images.Count() - 2)
            {
                ++data.current_index;
            }
            else
            {
                data.current_index = 0;
            }
            UpdateImage();
        }

        public void SetImageIndex(int index)
        {
            data.current_index = index;
            UpdateImage();
        }

        public int GetImageIndex()
        {
            return data.current_index;
        }

        public Resource.Type TypeID()
        {
            return data.type;
        }

        public int State()
        {
            return data.current_index;
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            MouseUpFunction();
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (data.is_switch == true) return;
            MouseUpFunction();
        }

        
        private void MouseLeave(object sender, MouseEventArgs e)
        {
            if (data.is_switch == true) return;
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {   //Cancel emulate
                    data.current_index-=2;
                    MouseUpFunction();
                } 
            }
        }
        private void UpdateImage()
        {
            BitmapImage bmi;
            if (data.images.Count - 1 >= data.current_index)
            {
                bmi = new BitmapImage(data.images[data.current_index].image);
            }
            else
            {
                bmi = new BitmapImage(data.images[0].image);
            }
            curr_img.Source = bmi;

        }

        public void Init(Resource.Type type, int index=0)
        {
            data = Resource.GetResource(type);
            if (index != 0) data.current_index = index;
            curr_img = new Image();
            curr_img.MouseUp += MouseUp;
            curr_img.MouseDown += MouseDown;
            curr_img.MouseLeave += MouseLeave;

            UpdateImage();
            curr_img.Stretch = System.Windows.Media.Stretch.None;

            width = data.width;
            height = data.height;
        }

        public void SetPos(double x, double y)
        {
            Canvas.SetLeft(curr_img, x);
            Canvas.SetTop(curr_img, y);
        }

        public void Move(double x, double y)
        {
            Canvas.SetLeft(curr_img, Canvas.GetLeft(curr_img) + x);
            Canvas.SetTop(curr_img, Canvas.GetTop(curr_img) + y);
        }

        public double GetRightPos()
        {
            return Canvas.GetLeft(curr_img) + width;
        }

        public Point GetPos()
        {
            Point pt = new Point();
            pt.X =  Canvas.GetLeft(curr_img);
            pt.Y = Canvas.GetTop(curr_img);
            return pt;
        }

        public double GetLeftPos()
        {
            return Canvas.GetLeft(curr_img);
        }

        public double GetTopPos()
        {
            return Canvas.GetTop(curr_img);
        }

        public double GetBottomPos()
        {
            return Canvas.GetBottom(curr_img) + height;
        }
    }

}
