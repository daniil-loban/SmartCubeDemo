using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.XPath;

namespace SmartCubeDemo
{
    public class CommandLine: IFileManager
    {
        List<SmartElement> elements;
        Canvas parent;

        public CommandLine(Canvas parent)
        {
            this.parent = parent;
            elements = new List<SmartElement>();
            AddElement(new Start(), 0);
            AddElement(new End(), 1);
            ArrangeFrom(0);
        }
        public void Clear()
        {
            elements = new List<SmartElement>();
            parent.Children.Clear();
            AddElement(new Start(), 0);
            AddElement(new End(), 1);
            ArrangeFrom(0);
        }

        public void AddElement(Resource.Type type, int image_idx, int position = -1)
        {
            switch (type)
            {
                case Resource.Type.Start:
                    AddElement(new Start(), elements.Count - 1);
                    break;
                case Resource.Type.End:
                    AddElement(new End(), elements.Count - 1);
                    break;
                case Resource.Type.Cycle:
                    AddElement(new Cycle(image_idx), elements.Count - 1);
                    break;
                case Resource.Type.CycleEnd:
                    AddElement(new CycleEnd(), elements.Count - 1);
                    break;
                case Resource.Type.ForwardBack:
                    AddElement(new ForwardBack(image_idx), elements.Count - 1);
                    break;
                case Resource.Type.LeftRight:
                    AddElement(new LeftRight(image_idx), elements.Count - 1);
                    break;

            }
            ArrangeFrom(0);

        }

        public void AddElement(SmartElement sme,int position = -1)
        {
            elements.Insert(position == -1 ? elements.Count - 1 : position, sme);
            parent.Children.Add(sme.curr_img);
            ArrangeFrom(0);
        }

        private void ArrangeFrom(int position)
        {
            double top = parent.Height;
            for (int i = position; i < elements.Count; ++i)
            {
                
                elements[i].SetPos((140 - elements[i].width) / 2, (top +  4) - elements[i].height); //SetPos(left - 4, (120 - elements[i].height) / 2);
                top = elements[i].GetTopPos();

            }
        }

        public void ReadCommands()
        {
            int i = 0;
            string res = "";
            foreach (SmartElement el in elements)
            {
                res += el.ToString() + ";";
                if (el.ToString() == "CC")
                {
                    res += ((Cycle)el).GetCounter() + ";";
                }
                i++;
            }
            Console.WriteLine(res);
        }

        public List<string> GetCommands()
        {
            List<string> ret = new List<string>();
            foreach (SmartElement el in elements)
            {
                ret.Add(el.ToString());
                if (el.ToString() == "CC")
                {
                    ret.Add(((Cycle)el).GetCounter());
                }
            }
            return ret;            
        }

        public void Save(StreamWriter sw = null)
        {
            string res = "";
            sw.WriteLine("\t<Commands>");
            foreach (SmartElement el in elements)
            {
                res += el.ToString() + ";";
                if (el.ToString() == "CC")
                {
                    res += ((Cycle)el).GetCounter() + ";";
                }
            }
            sw.WriteLine("\t\t<List Data=\"" + res + "\" />");
            sw.WriteLine("\t</Commands>");
        }

        public void Load(XPathDocument doc = null, XPathNavigator nav = null)
        {

            XPathExpression expr;
            expr = nav.Compile("Settings/Commands/List[@Data]");
            XPathNodeIterator iterator = nav.Select(expr);

            try
            {
                while (iterator.MoveNext())
                {
                    XPathNavigator nav2 = iterator.Current.Clone();
                    string[] commands = nav2.GetAttribute("Data", "").Split(';');
                    for( int i=0; i<commands.Count();++i)
                    {
                        switch (commands[i])
                        {
                            case "S":
                               
                                break;
                            case "E":
                                break;
                            case "R":
                                AddElement(Resource.Type.LeftRight, 1);
                                break;
                            case "L":
                                AddElement(Resource.Type.LeftRight, 0);
                                break;
                            case "F":
                                AddElement(Resource.Type.ForwardBack, 0);
                                break;
                            case "B":
                                AddElement(Resource.Type.ForwardBack, 1);
                                break;
                            case "CC":
                                int img;
                                int.TryParse(commands[++i], out img);
                                AddElement(Resource.Type.Cycle, ((img-2)*2));
                                break;
                            case "CE":
                                AddElement(Resource.Type.CycleEnd, 1);
                                break;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
