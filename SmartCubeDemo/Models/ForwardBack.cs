using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    public class ForwardBack : SmartElement
    {
        public ForwardBack(int index)
        {
            Init(Resource.Type.ForwardBack, index);
        }

        public ForwardBack()
        {
            Init(Resource.Type.ForwardBack);
        }

        public override string ToString()
        {
            return State() == 0 ? "F" : "B";
        }


    }
}
