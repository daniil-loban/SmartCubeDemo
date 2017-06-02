using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{

    class MapForward : SmartElement
    {

        protected override void MouseUpFunction()
        {

        }
        
        public override MapEngine.TypeMap GetMapType()
        {
            MapEngine.TypeMap ret = MapEngine.TypeMap.None;
            switch (base.GetImageIndex())
            {
                case 0:
                    ret = MapEngine.TypeMap.ForwardVertical;
                    break;
                case 1:
                    ret = MapEngine.TypeMap.ForwardHorizontal;
                    break;
            }
            return ret;
        }
        

        public override Resource.Type GetType()
        {
            return Resource.Type.MapForward;
        }


        public MapForward(int index)
        {
            Init(Resource.Type.MapForward, index);
        }

        public MapForward()
        {
            Init(Resource.Type.MapForward);
        }

        public override string ToString()
        {
            string sfx = "";
            switch (base.GetImageIndex())
            {
                case 0:
                    sfx = "V";
                    break;
                case 1:
                    sfx = "H";
                    break;
            }
            return "F" + sfx;
        }


    }
}
