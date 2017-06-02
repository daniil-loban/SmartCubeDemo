using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    public class CycleEnd : SmartElement
    {
        public CycleEnd()
        {
            Init(Resource.Type.CycleEnd);
        }
        public override string ToString()
        {
            return "CE";
        }

    }

}
