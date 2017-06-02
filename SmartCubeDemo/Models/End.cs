using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    public class End : SmartElement
    {
        public End()
        {
            Init(Resource.Type.End);
        }
        public override string ToString()
        {
            return "E";
        }

    }

}
