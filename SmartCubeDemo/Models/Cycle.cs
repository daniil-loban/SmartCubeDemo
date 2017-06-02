using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    public class Cycle : SmartElement
    {
        public Cycle(int index)
        {
            Init(Resource.Type.Cycle, index);
        }

        public Cycle()
        {
            Init(Resource.Type.Cycle);
        }
        public override string ToString()
        {
            return "CC";
        }

        public string GetCounter()
        {
            return ""+ (char)(50 + State() / 2);
        }


    }
}
