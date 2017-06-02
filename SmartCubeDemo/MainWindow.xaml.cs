using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartCubeDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MapEngine mpe;
        CommandLine cmdLine;
        ListCubes lc;
        ListMap lm;

        public MainWindow()
        {
            InitializeComponent();

            cmdScroll.ScrollToBottom();

            mpe = new MapEngine(mapView, arrowChoiser); //new MapView(mapView);
            Init();


            try
            {
                var txtFiles = Directory.EnumerateFiles("./Projects", "*.smc", SearchOption.TopDirectoryOnly);
                foreach (string currentFile in txtFiles)
                {
                    MenuItem rsc = new MenuItem();
                    rsc.Header = new FileInfo(currentFile).Name;
                    rsc.Click += MenuItem_open_project;
                    Rescent.Items.Add(rsc);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            arrowLeft.MouseUp += (s, e) => mpe.LeftArrow_Click();
            arrowRight.MouseUp += (s, e) => mpe.RightArrow_Click();
            arrowUp.MouseUp += (s, e) => mpe.UpArrow_Click();
            arrowDown.MouseUp += (s, e) => mpe.DownArrow_Click();
            arrowFin.MouseUp += (s, e) => mpe.FinArrow_Click();
            mpe.AddCubeEventHandler += SmartCubeAddToCommands;

            cmdLine = new CommandLine(commandLine);
            lm = new ListMap();
        }



        public void Init()
        {

            lc = new ListCubes();
            lc.AddElement(Resource.Type.Cycle, 0);
            lc.AddElement(Resource.Type.CycleEnd, 0);
            lc.AddElement(Resource.Type.ForwardBack, 0);
            lc.AddElement(Resource.Type.ForwardBack, 0);
            lc.AddElement(Resource.Type.ForwardBack, 0);
            lc.AddElement(Resource.Type.LeftRight, 0);
            lc.AddElement(Resource.Type.LeftRight, 0);
            lc.AddElement(Resource.Type.LeftRight, 0);
            cubeElements.ItemsSource = lc.Elements;

            mpe.AddElement(Resource.Type.Car, 0);
            mpe.car.curr_img.Visibility = Visibility.Hidden;

            ((Car)(mpe.car)).AddCarEventHandler += CarSignals;


        }

        private void commandLine_MouseMove(object sender, MouseEventArgs e)
        {
            Point pointToWindow = Mouse.GetPosition((IInputElement)sender);
        }


        private void cubeElements_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListElement ce = (((ListBox)sender).SelectedItem) as ListElement;
            if (ce != null && e.LeftButton == MouseButtonState.Pressed)
            {

                cmdLine.AddElement(ce.Type, ce.Index);
                lc.RemoveElement(ce);
              

            }
        }

        private void SmartCubeAddToCommands(object sender, SmartCubesEventArgs e)
        {
            switch (e.cubeSet)
            {
                case MapEngine.SmartCubeSet.Forward:
                    cmdLine.AddElement(new ForwardBack(0));
                    break;
                case MapEngine.SmartCubeSet.Left:
                    cmdLine.AddElement(new LeftRight(0));
                    break;
                case MapEngine.SmartCubeSet.Right:
                    cmdLine.AddElement(new LeftRight(1));
                    break;
            }
        }


        private void mapElements_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListElement me = (((ListBox)sender).SelectedItem) as ListElement;
            if (me != null && e.LeftButton == MouseButtonState.Pressed)
            {
                mpe.AddElement(me.Type, me.Index, 0);
                if (me.Type == Resource.Type.MapStart || me.Type == Resource.Type.MapEnd)
                {
                    lm.RemoveElement(me);
                }
                switch (me.Type)
                {
                    case Resource.Type.MapTurn:
                        cmdLine.AddElement(new LeftRight(me.Index));
                        break;
                    case Resource.Type.MapCross:
                        cmdLine.AddElement(new ForwardBack(0));
                        break;
                    case Resource.Type.MapForward:
                        cmdLine.AddElement(new ForwardBack(0));
                        break;
                }

            }
        }

        private void mapElements_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {   

            ListElement me = (((ListBox)sender).SelectedItem) as ListElement;
            if (me != null)
                me.Next();
            

        }

        private void Reset()
        {


            cmdLine.Clear();
            mpe.Clear();


            lc.Clear();
            lm.Clear();
            Init();
            arrowChoiser.Visibility = Visibility.Visible;
            
        }

        private void Move_By_Cubes()
        {

            cmdLine.ReadCommands();
            List<string> cmd_orig;
            List<string> cmd;
            cmd_orig = cmdLine.GetCommands();

            Intepreter ipt = new Intepreter();
            cmd = ipt.Convert(cmd_orig);

            MapEngine.Direction dir = mpe.GetStartDirection();

            Point pos = mpe.GetStartPosition();
            ((Car)mpe.car).SetPos(pos.X, pos.Y);
            ((Car)mpe.car).PositionByDirection(mpe.GetStartDirection());

            

            int i = 0;
            foreach (string s in cmd)
            {
                switch (dir)
                {
                    case MapEngine.Direction.Down:
                        switch (s)
                        {
                            case "R":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initDownLeft(pos));
                                dir = MapEngine.Direction.Left;
                                pos.X -= 120;
                                break;
                            case "L":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initDownRight(pos));
                                dir = MapEngine.Direction.Right;
                                pos.X += 120;
                                break;
                            case "F":
                            //case "S":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initUpDown(pos));
                                dir = MapEngine.Direction.Down;
                                pos.Y += 120;
                                break;
                            case "E":

                                break;
                        }
                        break;
                    case MapEngine.Direction.Right:
                        switch (s)
                        {
                            case "R":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initRightDown(pos));
                                dir = MapEngine.Direction.Down;
                                pos.Y += 120;
                                break;
                            case "L":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initRightUP(pos));
                                dir = MapEngine.Direction.Up;
                                pos.Y -= 120;
                                break;
                            case "F":
                            //case "S":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initLeftRight(pos));
                                dir = MapEngine.Direction.Right;
                                pos.X += 120;
                                break;
                            case "E":
                                break;
                        }
                        break;
                    case MapEngine.Direction.Up:
                        switch (s)
                        {
                            case "R":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initUpRight(pos));
                                dir = MapEngine.Direction.Right;
                                pos.X += 120;
                                break;
                            case "L":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initUpLeft(pos));
                                dir = MapEngine.Direction.Left;
                                pos.X -= 120;
                                break;
                            case "F":
                            //case "S":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initDownUp(pos));
                                dir = MapEngine.Direction.Up;
                                pos.Y -= 120;
                                break;
                            case "E":

                                break;
                        }
                        break;
                    case MapEngine.Direction.Left:
                        switch (s)
                        {
                            case "R":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initLeftUp(pos));
                                dir = MapEngine.Direction.Up;
                                pos.Y -= 120;
                                break;
                            case "L":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initLeftDown(pos));
                                dir = MapEngine.Direction.Down;
                                pos.Y += 120;
                                break;
                            case "F":
                            //case "S":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initRightLeft(pos));
                                dir = MapEngine.Direction.Left;
                                pos.X -= 120;
                                break;
                            case "E":

                                break;
                        }
                        break;
                }
                ++i;
            }
            ((Car)mpe.car).Run();
        }

        private Point CopyPoint(Point pt)
        {
            return new Point() { X = pt.X, Y = pt.Y };
        }

        private void MenuItem_Start(object sender, RoutedEventArgs e)
        {
            cmdLine.ReadCommands();
            List<string> cmd;
            cmd = cmdLine.GetCommands();

            MapEngine.Direction dir = MapEngine.Direction.None;

            Point ptStart = mpe.GetStartPosition(); 
            ((Car)mpe.car).SetPos(ptStart.X, ptStart.Y);
            dir = mpe.GetStartDirection();
            ((Car)mpe.car).PositionByDirection(dir);

            Point pos = new Point() { X = ptStart.X, Y = ptStart.Y};

            int i = 0;
            foreach (string s in cmd)
            {
                switch (dir)
                {
                    case MapEngine.Direction.Down:
                        switch (s)
                        {
                            case "R":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initDownLeft(CopyPoint(pos)));
                                dir = MapEngine.Direction.Left;
                                pos.X -= 120;
                                break;
                            case "L":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initDownRight(CopyPoint(pos)));
                                dir = MapEngine.Direction.Right;
                                pos.X += 120;
                                break;
                            case "F":

                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initUpDown(CopyPoint(pos)));
                                dir = MapEngine.Direction.Down;
                                pos.Y += 120;
                                break;
                            case "E":

                                break;
                        }
                        break;
                    case MapEngine.Direction.Right:
                        switch (s)
                        {
                            case "R":
                                
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initRightDown(CopyPoint(pos)));
                                dir = MapEngine.Direction.Down;
                                pos.Y += 120;
                                break;
                            case "L":
                                
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initRightUP(CopyPoint(pos)));
                                dir = MapEngine.Direction.Up;
                                pos.Y -= 120;
                                break;
                            case "F":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initLeftRight(CopyPoint(pos)));
                                dir = MapEngine.Direction.Right;
                                pos.X += 120;
                                break;
                            case "E":
                                break;
                        }
                        break;
                    case MapEngine.Direction.Up:
                       switch (s)
                        {
                            case "R":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initUpRight(CopyPoint(pos)));
                                dir = MapEngine.Direction.Right;
                                pos.X += 120;
                                break;
                            case "L":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initUpLeft(CopyPoint(pos)));
                                dir = MapEngine.Direction.Left;
                                pos.X -= 120;
                                break;
                            case "F":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initDownUp(CopyPoint(pos)));
                                dir = MapEngine.Direction.Up;
                                pos.Y -= 120;
                                break;
                            case "E":

                                break;
                        }
                        break;
                    case MapEngine.Direction.Left:
                        switch (s)
                        {
                            case "R":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initLeftUp(CopyPoint(pos)));
                                dir = MapEngine.Direction.Up;
                                pos.Y -= 120;
                                break;
                            case "L":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initLeftDown(CopyPoint(pos)));
                                dir = MapEngine.Direction.Down;
                                pos.Y += 120;
                                break;
                            case "F":
                                ((Car)mpe.car).AddQueue(((Car)mpe.car).initRightLeft(CopyPoint(pos)));
                                dir = MapEngine.Direction.Left;
                                pos.X -= 120;
                                break;
                            case "E":

                                break;
                        }
                        break;
                }
                ++i;
            }
            ((Car)mpe.car).Run();

        }








        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void arrowTop_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void CarSignals(object sender, SmartCarSignalEventArgs e)
        {
            switch (e.CarSignal)
            {
                case Car.CarSignals.FINISH:
                    miRun.IsEnabled =  true;
                    Console.WriteLine("SIGNAL FINISH!");
                    break;

                case Car.CarSignals.OUT:

                    break;


            }

        }
        private void MenuItem_Start_BY_Cubes(object sender, RoutedEventArgs e)
        {
            miRun.IsEnabled = false;
            Move_By_Cubes();
             
        }

        private void MenuItem_ClearCommands(object sender, RoutedEventArgs e)
        {
            cmdLine.Clear();
            lc = new ListCubes();
            lc.AddElement(Resource.Type.Cycle, 0);
            lc.AddElement(Resource.Type.CycleEnd, 0);
            lc.AddElement(Resource.Type.ForwardBack, 0);
            lc.AddElement(Resource.Type.ForwardBack, 0);
            lc.AddElement(Resource.Type.ForwardBack, 0);
            lc.AddElement(Resource.Type.LeftRight, 0);
            lc.AddElement(Resource.Type.LeftRight, 0);
            lc.AddElement(Resource.Type.LeftRight, 0);
            cubeElements.ItemsSource = lc.Elements;
        }

        private void MenuItem_save_project(object sender, RoutedEventArgs e)
        {
            FileManager fm = new FileManager("./Projects", "", new List<IFileManager>() { mpe, cmdLine });
            fm.Save();

            MenuItem rsc = new MenuItem();
            rsc.Header = new FileInfo(fm.currentFile).Name;
            rsc.Click += MenuItem_open_project;
            Rescent.Items.Add(rsc);


        }

        private void MenuItem_open_project(object sender, RoutedEventArgs e)
        {
            FileManager fm = new FileManager("./Projects", ((MenuItem)sender).Header.ToString(), new List<IFileManager>() { mpe, cmdLine });
            Reset();
            fm.Load();
        }   
    }
}
