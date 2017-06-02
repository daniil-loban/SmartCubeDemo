using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    class Intepreter
    {

        struct cycle
        {
            public int begin;
            public int count;
        };

        public List<string> Convert(List<string> prog)
        {
            List<string> result = new List<string>();
            cycle[] cycles = new cycle[10];
            int i;

            for (i = 0; i < 10; ++i)
            {
                cycles[i].begin = 0;
                cycles[i].count = 0;
            }

            int pos_f = -1;


            for (i = 0; i < prog.Count; ++i)
            {
                if (prog[i] == "CC")
                {
                    ++pos_f;
                    cycles[pos_f].begin = i;
                    ++i;
                    int cnt;
                    int.TryParse(prog[i], out cnt);
                    cycles[pos_f].count = cnt;

                }
                else if (prog[i] == "CE")
                {
                    if (cycles[pos_f].count > 1)
                    {
                        cycles[pos_f].count -= 1; //decrement cycle
                        i = cycles[pos_f].begin + 1;
                    }
                    else
                        --pos_f; //up cycle
                }
                else if (prog[i] == "E")
                {
                    result.Add(prog[i]); //just commands

                } else if ( prog[i] == "S" && prog.Count>2)
                {
                    result.Add("F"); //just commands
                }
                else
                {
                    result.Add(prog[i]); //just commands
                }


            }
            return result;
        }
    }

}