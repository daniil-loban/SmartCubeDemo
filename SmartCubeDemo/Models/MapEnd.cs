using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    class MapEnd : SmartElement
    {
        protected override void MouseUpFunction()
        {

        }
        
        public override MapEngine.TypeMap GetMapType()
        {
            MapEngine.TypeMap ret=MapEngine.TypeMap.None;
            switch (base.GetImageIndex())
            {
                case 0:
                    ret = MapEngine.TypeMap.FinishToDown;
                    break;
                case 1:
                    ret = MapEngine.TypeMap.FinishToLeft;
                    break;
                case 2:
                    ret = MapEngine.TypeMap.FinishToRight;
                    break;
                case 3:
                    ret = MapEngine.TypeMap.FinishToUp;
                    break;
            }
            return ret;
        }
        
        public override Resource.Type GetType()
        {
            return Resource.Type.MapEnd;
        }





        public MapEnd(int index)
        {
            Init(Resource.Type.MapEnd, index);
        }
        public MapEnd()
        {
            Init(Resource.Type.MapEnd);
        }
        public override string ToString()
        {
            string sfx = "";
            switch (base.GetImageIndex())
            {
                case 0:
                    sfx = "D";
                    break;
                case 1:
                    sfx = "L";
                    break;
                case 2:
                    sfx = "R";
                    break;
                case 3:
                    sfx = "U";
                    break;
            }
            return "F" + sfx;
        }


    }
}
