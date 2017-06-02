using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    public class Start : SmartElement
    {
        public Start()
        {
            Init(Resource.Type.Start);
        }
        public override string ToString()
        {
            return "S";
        }
    }
}
