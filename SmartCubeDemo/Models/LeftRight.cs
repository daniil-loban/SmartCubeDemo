using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    public class LeftRight : SmartElement
    {
        public LeftRight(int index)
        {
            Init(Resource.Type.LeftRight, index);
        }
        public LeftRight()
        {
            Init(Resource.Type.LeftRight);
        }
        public override string ToString()
        {
            return State() == 0 ? "L" : "R";
        }

    }
}
