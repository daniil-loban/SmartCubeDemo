using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCubeDemo
{
    public static class Resource
    {
        public struct ImageInfo{
            public Uri image;
            public string name;
        }

        public struct Data
        {
            public Type type;
            public List<ImageInfo> images;
            public double height;
            public double width;
            public bool is_switch;
            public int current_index;
        }

        static bool binit;
        public static List<ImageInfo> Car;
        public static List<ImageInfo> Start;
        public static List<ImageInfo> Cycle;
        public static List<ImageInfo> CycleEnd;
        public static List<ImageInfo> End;
        public static List<ImageInfo> ForwardBack;
        public static List<ImageInfo> LeftRight;
        public static List<ImageInfo> MapCross;
        public static List<ImageInfo> MapStart;
        public static List<ImageInfo> MapEnd;
        public static List<ImageInfo> MapTurn;
        public static List<ImageInfo> MapForward;

        public enum Type { None, Car, Start, Cycle, CycleEnd, End, ForwardBack, LeftRight , MapForward,MapCross, MapTurn, MapStart, MapEnd}
        public static string path = @"/SmartCubeDemo;component/Res/";

        
        public static ImageInfo NewImageInfo(string filename, string name)
        {
            return new ImageInfo() { image = new Uri(path + filename, UriKind.Relative), name = name };
        }


        

        private static void Init()
        {

            Car = new List<ImageInfo> { NewImageInfo("car_d.png","Car"),
                                        NewImageInfo("car_l.png","Car") ,
                                        NewImageInfo("car_r.png","Car"),
                                        NewImageInfo("car_u.png","Car")};

            Start = new List<ImageInfo> { NewImageInfo("start_default.png","Start"),
                                          NewImageInfo("start_push_button.png","Start push")};

            End = new List<ImageInfo> { NewImageInfo("end.png", "End") };

            CycleEnd = new List<ImageInfo> { NewImageInfo("cicle_end.png", "Cycle end")};
            Cycle = new List<ImageInfo>();
            MapTurn = new List<ImageInfo>();

            for (int i = 2; i < 10; i++)
            {
                Cycle.Add( NewImageInfo("cicle_" + i + "_default.png" , "Cycle (" + i  + ")" ));
                Cycle.Add(NewImageInfo("cicle_" + i + "_push.png", "Cycle (" + i + ") push")); 

            }

            ForwardBack = new List<ImageInfo> { NewImageInfo("+forward.png", "Forward"),
                                                NewImageInfo( "back.png", "Back")};

            LeftRight = new List<ImageInfo> { NewImageInfo("left.png", "Left"),
                                              NewImageInfo("right.png", "Right")}; 


            MapCross = new List<ImageInfo> { NewImageInfo("map_crossroads.png", "Crossroad") };

            MapStart = new List<ImageInfo> { NewImageInfo("map_start_d.png", "Start from down"),
                                             NewImageInfo("map_start_l.png", "Start from left"),
                                             NewImageInfo("map_start_r.png", "Start from right"),
                                             NewImageInfo("map_start_u.png", "Start from up")
            };

            MapEnd = new List<ImageInfo> { NewImageInfo("map_finish_d.png", "Finish on down"),
                                       NewImageInfo("map_finish_l.png", "Finish on left"),
                                       NewImageInfo("map_finish_r.png", "Finish on right"),
                                       NewImageInfo("map_finish_u.png", "Finish on up")

            };


            MapTurn = new List<ImageInfo> { NewImageInfo("map_turn_dr.png", "Turn right-down"),
                                       NewImageInfo("map_turn_dl.png", "Turn left-down"),
                                       NewImageInfo("map_turn_ul.png", "Turn left-up"),
                                       NewImageInfo("map_turn_ur.png", "Turn right-up")

            };
            MapForward = new List<ImageInfo> { NewImageInfo("map_forward_vert.png", "Forward vertical"),
                                            NewImageInfo("map_forward_hor.png", "Forward horizontal") };
                                       
                                       

            binit = true;
        }

        public static Data GetResource(Type type)
        {
            if (!binit)
            {
                Init();
            }

            Data ret = new Data();
            ret.type = type;

            switch (type)
            {
                case Type.MapStart:
                    ret.images = MapStart;
                    ret.width = 120;
                    ret.height = 120;
                    ret.is_switch = true;
                    break;
                case Type.MapForward:
                    ret.images = MapForward;
                    ret.width = 120;
                    ret.height = 120;
                    ret.is_switch = true;
                    break;
                case Type.MapEnd:
                    ret.images = MapEnd;
                    ret.width = 120;
                    ret.height = 120;
                    ret.is_switch = true;
                    break;
                case Type.MapCross:
                    ret.images = MapCross;
                    ret.width = 120;
                    ret.height = 120;
                    ret.is_switch = true;
                    break;
                case Type.MapTurn:
                    ret.images = MapTurn;
                    ret.width = 120;
                    ret.height = 120;
                    ret.is_switch = true;
                    break;
                case Type.Car:
                    ret.is_switch = true;
                    ret.images = Car;
                    ret.width = 54;
                    ret.height = 24;
                    break;
                case Type.Start:
                    ret.images = Start;
                    ret.width = 90;
                    ret.height = 60;
                    break;
                case Type.Cycle:
                    ret.images = Cycle;
                    ret.width = 55;
                    ret.height = 59;
                    break;
                case Type.CycleEnd:
                    ret.images = CycleEnd;
                    ret.width = 55;
                    ret.height = 36;
                    break;
                case Type.End:
                    ret.images = End;
                    ret.width = 55;
                    ret.height = 55;

                    break;
                case Type.ForwardBack:
                    ret.images = ForwardBack;
                    ret.width = 55;
                    ret.height = 59;
                    ret.is_switch = true;
                    break;
                case Type.LeftRight:
                    ret.images = LeftRight;
                    ret.width = 55;
                    ret.height = 59;
                    ret.is_switch = true;
                    break;
            };
            return ret;
        }
    }
}
