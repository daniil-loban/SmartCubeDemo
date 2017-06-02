using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    class MapStart : SmartElement
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
                    ret = MapEngine.TypeMap.StartToDown;
                    break;
                case 1:
                    ret = MapEngine.TypeMap.StartToLeft;
                    break;
                case 2:
                    ret = MapEngine.TypeMap.StartToRight;
                    break;
                case 3:
                    ret = MapEngine.TypeMap.StartToUp;
                    break;
            }
            return ret;
        }
        

        public override Resource.Type GetType()
        {
            return Resource.Type.MapStart;
        }


        public MapStart(int index)
        {
            Init(Resource.Type.MapStart, index);
        }
        public MapStart()
        {
            Init(Resource.Type.MapStart);
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
            return "S" + sfx;

        }


    }

}
