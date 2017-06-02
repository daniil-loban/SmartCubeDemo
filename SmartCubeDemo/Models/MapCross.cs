using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    class MapCross : SmartElement
    {
        protected override void MouseUpFunction()
        {

        }

        public override Resource.Type GetType()
        {
            return Resource.Type.MapCross;
        }

        public override MapEngine.TypeMap GetMapType()
        {
            return MapEngine.TypeMap.Cross;
        }
        
        public MapCross(int index)
        {
            Init(Resource.Type.MapCross,index);
        }

        public MapCross()
        {
            Init(Resource.Type.MapCross);
        }
        public override string ToString()
        {
            return "X";
        }


    }
}
