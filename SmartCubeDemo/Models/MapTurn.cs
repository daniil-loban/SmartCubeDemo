using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{

    class MapTurn : SmartElement
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
                    ret = MapEngine.TypeMap.TurnDownRight;
                    break;
                case 1:
                    ret = MapEngine.TypeMap.TurnDownLeft;
                    break;
                case 2:
                    ret = MapEngine.TypeMap.TurnUpLeft;
                    break;
                case 3:
                    ret = MapEngine.TypeMap.TurnUpRight;
                    break;
            }
            return ret;
        }
        

        public override Resource.Type GetType()
        {
            return Resource.Type.MapTurn;
        }

        public MapTurn(int index)
        {
            Init(Resource.Type.MapTurn, index);
        }
        public MapTurn()
        {
            Init(Resource.Type.MapTurn);
        }
        public override string ToString()
        {

            string sfx = "";
            switch (base.GetImageIndex())
            {
                case 0:
                    sfx = "DR";
                    break;
                case 1:
                    sfx = "DL";
                    break;
                case 2:
                    sfx = "UL";
                    break;
                case 3:
                    sfx = "UR";
                    break;
            }
            return "T" + sfx;

        }


    }
}
