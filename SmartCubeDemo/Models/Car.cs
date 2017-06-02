using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace SmartCubeDemo
{


    public class SmartCarSignalEventArgs : EventArgs
    {
        public Car.CarSignals CarSignal { get; set; }
    }

    public class Car : SmartElement
    {
        public Queue<MoveInfo> queue_car;
        public enum CarSignals { FINISH, OUT }


        public event EventHandler<SmartCarSignalEventArgs> AddCarEventHandler;

        public void AddCarSignal(CarSignals carSignal)
        {
            EventHandler<SmartCarSignalEventArgs> handler = AddCarEventHandler;
            if (handler != null)
            {
                SmartCarSignalEventArgs e = new SmartCarSignalEventArgs() { CarSignal = carSignal };
                handler(this, e);
            }
        }


        public Car(int index)
        {
            Init(Resource.Type.Car, index);

            Canvas.SetZIndex(curr_img, 99);
            queue_car = new Queue<MoveInfo>();
        }

        public Car()
        {
            Init(Resource.Type.Car);
            Canvas.SetZIndex(curr_img, 99);
            queue_car = new Queue<MoveInfo>();
        }

        public void PositionByDirection(MapEngine.Direction d )
        {
            switch (d)
            {
                case MapEngine.Direction.Down:
                    SetImageIndex(0);
                    break;
                case MapEngine.Direction.Left:
                    SetImageIndex(1);

                    break;
                case MapEngine.Direction.Right:
                    SetImageIndex(2);
                    break;
                case MapEngine.Direction.Up:
                    SetImageIndex(3);
                    break;


            }
            RotateTransform rotateTransform = new RotateTransform(0);
            curr_img.RenderTransform = rotateTransform;
        }

        public enum typeMove {
            LeftRight, RightLeft, UpDown, DownUp,
            LeftUp, RightUp, UpLeft, UpRight,
            LeftDown, RightDown, DownLeft, DownRight,
        };

        public void AddQueue(MoveInfo mi)
        {
            queue_car.Enqueue(mi);
        }

        public struct MoveInfo {


            public typeMove type;
            public Point pt;
            public int start;
            public int end;
            public int sign;

        }


        public MoveInfo initRightUP(Point ptm)
        {

            MoveInfo moveInfo= new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.RightUp;
            moveInfo.start = 0;
            moveInfo.end = 90;
            moveInfo.sign = 1;
            return moveInfo;

        }

        public MoveInfo initDownLeft(Point ptm)
        {
            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.DownLeft;
            moveInfo.start = 0;
            moveInfo.end = 90;
            moveInfo.sign = 1;
            return moveInfo;
        }

        public MoveInfo initDownRight(Point ptm)
        {
            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.DownRight;
            moveInfo.start = 0;
            moveInfo.end = 90;
            moveInfo.sign = 1;
            return moveInfo;
        }
        public MoveInfo initLeftUp(Point ptm)
        {
            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.LeftUp;
            moveInfo.start = 0;
            moveInfo.end = 90;
            moveInfo.sign = 1;
            return moveInfo;
        }

        public MoveInfo initUpRight(Point ptm)
        {
            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.UpRight;
            moveInfo.start = 0;
            moveInfo.end = 90;
            moveInfo.sign = 1;
            return moveInfo;
        }

        public MoveInfo initLeftDown(Point ptm)
        {
            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.LeftDown;
            moveInfo.start = 0;
            moveInfo.end = 90;
            moveInfo.sign = 1;
            return moveInfo;
        }


        public MoveInfo initUpLeft(Point ptm)
        {
            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.UpLeft;
            moveInfo.start = 0;
            moveInfo.end = 90;
            moveInfo.sign = 1;
            return moveInfo;
        }

        public MoveInfo initRightDown(Point ptm)
        {
            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.RightDown;
            moveInfo.start = 0;
            moveInfo.end = 90;
            moveInfo.sign = 1;
            return moveInfo;
        }

        public MoveInfo initLeftRight(Point ptm) {

            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.LeftRight;
            moveInfo.start = 0;
            moveInfo.end = 120;
            moveInfo.sign =1;
            return moveInfo;

        }

        public MoveInfo initRightLeft(Point ptm)
        {
            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.RightLeft;
            moveInfo.start = 120;
            moveInfo.end = 0;
            moveInfo.sign = -1;
            return moveInfo;

        }

        public MoveInfo initUpDown(Point ptm)
        {

            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X;
            moveInfo.pt.Y = ptm.Y;
            moveInfo.type = Car.typeMove.UpDown;
            moveInfo.start = 0;
            moveInfo.end = 120;
            moveInfo.sign = 1;
            return moveInfo;

        }


        public MoveInfo initDownUp(Point ptm)
        {

            MoveInfo moveInfo = new MoveInfo();
            moveInfo.pt.X = ptm.X ;
            moveInfo.pt.Y = ptm.Y ;
            moveInfo.type = Car.typeMove.DownUp;
            moveInfo.start = 120;
            moveInfo.end = 0;
            moveInfo.sign = -1;
            return moveInfo;

        }


        public void Run()
        {
            Thread tr = new Thread(new ParameterizedThreadStart(ThreadProc));
            tr.Start(queue_car);
        }

        delegate Point MoveDelegate(int n, ref Point pt);

        private void ThreadProc(Object q)
        {
            Queue<MoveInfo> queue = (Queue<MoveInfo>)q;

            MoveInfo moveInfo = new MoveInfo();
            Point pt = new Point() { X=0.0,Y=0.0 }; //mi.pt;

            do
            {
                if (queue.Count() != 0)
                    moveInfo = queue.Dequeue();

                MoveDelegate dMove = null;

                pt.X = moveInfo.pt.X;
                pt.Y = moveInfo.pt.Y;

                switch (moveInfo.type)
                {
                    case typeMove.DownUp:
                        dMove = delegate (int n, ref Point pti) { pti.Y--; this.SetPos(pt.X, pt.Y); return pti; };
                        break;
                    case typeMove.LeftRight:
                        dMove = delegate (int n, ref Point pti) { pti.X++; this.SetPos(pt.X, pt.Y); return pti; };
                        break;
                    case typeMove.RightLeft:
                        dMove = delegate (int n, ref Point pti) { pti.X--; this.SetPos(pt.X, pt.Y); return pti; };
                        break;
                    case typeMove.UpDown:
                        dMove = delegate (int n, ref Point pti) { pti.Y++; this.SetPos(pt.X, pt.Y); return pti; };
                        break;

                    case typeMove.DownLeft:
                        dMove = delegate (int n, ref Point pti) {
                            RotateTransform rotateTransform = new RotateTransform(n, 0, 0);
                            curr_img.RenderTransform = rotateTransform;
                            return pti;
                        };
                        break;
                    case typeMove.DownRight:
                        dMove = delegate (int n, ref Point pti) {
                            RotateTransform rotateTransform = new RotateTransform(-n,+ 120, 0);
                            curr_img.RenderTransform = rotateTransform;
                            return pti;
                        };
                        break;
                    case typeMove.LeftDown:
                        dMove = delegate (int n, ref Point pti) {
                            RotateTransform rotateTransform = new RotateTransform(-n, 120,  120);
                            curr_img.RenderTransform = rotateTransform;
                            return pti;
                        };
                        break;
                    case typeMove.LeftUp:
                        dMove = delegate (int n, ref Point pti) {
                            RotateTransform rotateTransform = new RotateTransform(n,   120, 0);
                            curr_img.RenderTransform = rotateTransform;
                            return pti;
                        };
                        break;
                    case typeMove.RightDown:
                        dMove = delegate (int n, ref Point pti) {
                            RotateTransform rotateTransform = new RotateTransform(n, 0,  120);
                            curr_img.RenderTransform = rotateTransform;
                            return pti;
                        };
                        break;
                    case typeMove.RightUp:
                        dMove = delegate (int n, ref Point pti) {
                            RotateTransform rotateTransform = new RotateTransform(-n, 0, 0);
                            curr_img.RenderTransform = rotateTransform;
                            return pti;
                        };
                        break;
                    case typeMove.UpLeft:
                        dMove = delegate (int n, ref Point pti) {
                            RotateTransform rotateTransform = new RotateTransform(-n, 0,  120);
                            curr_img.RenderTransform = rotateTransform;
                            return pti;
                        }; break;
                    case typeMove.UpRight:
                        dMove = delegate (int n, ref Point pti) {
                            RotateTransform rotateTransform = new RotateTransform(n,  120,  120);
                            curr_img.RenderTransform = rotateTransform;
                            return pti;
                        }; break;
                }

                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        switch (moveInfo.type)
                        {
                            case typeMove.DownLeft:
                                PositionByDirection(MapEngine.Direction.Down);
                                break;
                            case typeMove.DownRight:
                                PositionByDirection(MapEngine.Direction.Down);
                                break;
                            case typeMove.LeftDown:
                                PositionByDirection(MapEngine.Direction.Left);
                                break;
                            case typeMove.LeftUp:
                                PositionByDirection(MapEngine.Direction.Left);
                                break;
                            case typeMove.RightDown:
                                PositionByDirection(MapEngine.Direction.Right);
                                break;
                            case typeMove.RightUp:
                                PositionByDirection(MapEngine.Direction.Right);
                                break;
                            case typeMove.UpLeft:
                                PositionByDirection(MapEngine.Direction.Up);
                                break;
                            case typeMove.UpRight:
                                PositionByDirection(MapEngine.Direction.Up);
                                break;
                            case typeMove.RightLeft:
                                PositionByDirection(MapEngine.Direction.Left);
                                break;
                            case typeMove.LeftRight:
                                PositionByDirection(MapEngine.Direction.Right);
                                break;
                            case typeMove.DownUp:
                                PositionByDirection(MapEngine.Direction.Up);
                                break;
                            case typeMove.UpDown:
                                PositionByDirection(MapEngine.Direction.Down);
                                break;
                        }
                        SetPos(moveInfo.pt.X, moveInfo.pt.Y);

                }));
                


                int Start = moveInfo.start;
                int End = moveInfo.end;
                int Sign = moveInfo.sign;


                for (int n = Start; n != End; n += Sign)
                {
                   System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        dMove(n, ref pt);
                    }
                    ));
                    System.Threading.Thread.Sleep(10);
                }

            } while (queue_car.Count > 0);

            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                AddCarSignal(CarSignals.FINISH);
            }));
        }




    }
}
