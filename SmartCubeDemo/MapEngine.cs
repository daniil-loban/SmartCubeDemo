using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.XPath;

namespace SmartCubeDemo
{


    public class SmartCubesEventArgs : EventArgs
    {
        public MapEngine.SmartCubeSet cubeSet { get; set; }
    }

    public class MapEngine: IFileManager
    {   
        public enum TypeMap { None = 0, Cross = 15, StartToDown = 2, StartToLeft = 4, StartToRight = 1, StartToUp = 8, FinishToDown = 2, FinishToLeft = 4, FinishToRight = 1, FinishToUp = 8, TurnDownRight = 3, TurnDownLeft = 6, TurnUpLeft = 12, TurnUpRight = 9, ForwardHorizontal = 5, ForwardVertical = 10 }

        public enum SmartCubeSet { Forward, Back, Left, Right, None }
        public event EventHandler<SmartCubesEventArgs> AddCubeEventHandler;

        public void AddCubeSignal()
        {
            EventHandler<SmartCubesEventArgs> handler = AddCubeEventHandler;

            if (elements.Count <= 0) return;

            if (handler != null)
            {
                SmartCubesEventArgs e = new SmartCubesEventArgs() { cubeSet = relativeDirection };
                handler(this, e);
            }
        }

        public Point GetPos(int index_element)
        {
            if (elements.Count > index_element)
                return elements[index_element].GetPos();
            else
                return new Point() { X=0,Y=0};
        }

        public class Cell
        {
            public List<CellElement> coll;
            public Cell()
            {

                coll = new List<CellElement>() { new CellElement() };
            }

            public CellElement GetLast()
            {
                return this.coll[this.coll.Count - 1];
            }
        }

        public struct CellElement
        {
            public TypeMap type;
            public SmartElement element;
        }

        public enum Direction { All = 15, None = 0, Down = 2, Left = 4, Right = 1, Up = 8 };
        public enum Orientation { Down = 0, Left = 1, Right = 2, Up = 3, Vertiacal = 0, Horizontal = 1, TurnDownRight = 0, TurnDownLeft = 1, TurnLeftUp = 2, TurnRightUp = 3 };


        private int height;
        private int widht;
        private Point pos;
        public Direction direction;

        double offsetX;
        double offsetY;

        Point posChoiser;

        List<SmartElement> elements;
        Canvas parent;
        Canvas choiser;
        SmartCubeSet relativeDirection;

        private List<List<Cell>> grid;
        public SmartElement car;


        public MapEngine(Canvas parent, Canvas choiser)
        {

            this.parent = parent;
            this.choiser = choiser;
            offsetX = 0;
            offsetY = 0;
            elements = new List<SmartElement>();

            initGrid();
        }

        public void Clear()
        {
            offsetX = 0;
            offsetY = 0;

            parent.Children.RemoveRange(1, parent.Children.Count - 1);
            elements.Clear();
            grid.Clear();
            initGrid();
        }

        private void initGrid()
        {
            List<Cell> listcell = new List<Cell>() { new Cell() { coll = new List<CellElement>() { new CellElement() } } };
            grid = new List<List<Cell>>() { listcell };
            direction = 0;
            widht = 1;
            height = 1;

            direction = Direction.None;
            relativeDirection = SmartCubeSet.None;

            posChoiser.X = pos.X = 0;
            posChoiser.Y = pos.Y = 0;
            SetPositionChoiser();
        }


        private int CreateStartIfNotExist(Orientation orient)
        {
            if (elements.Count != 0) return 1;

            direction = Direction.None;
            AddElement(Resource.Type.MapStart, (int)orient);
            return 0;

        }

        private int CreateCrossIfNeed(Direction newDirect)
        {


            if (posChoiser.Y <= height - 1 && posChoiser.X <= widht - 1)

                if (grid[(int)posChoiser.Y][(int)posChoiser.X].GetLast().element == null)
                {

                }
                else
                {



                    if (newDirect == Direction.Up && posChoiser.Y == 0)
                    {
                        InsertRow(0);
                        ArrangeMaps();
                        pos.Y += 1;
                        posChoiser.Y += 1;

                    }

                    if (newDirect == Direction.Left && posChoiser.X == 0)
                    {
                        InsertCol(0);
                        ArrangeMaps();
                        pos.X += 1;
                        posChoiser.X += 1;

                    }

                    AddElement(Resource.Type.MapCross, 0);
                    direction = newDirect;
                    ArrangeMaps();



                    return 0;
                }
            return 1;
        }

        public void LeftArrow_Click()
        {



            if (CreateStartIfNotExist(Orientation.Left) == 0)
                return;


            if (direction == Direction.Left)
            {
                relativeDirection = SmartCubeSet.Forward;

                if (CreateCrossIfNeed(Direction.Left) == 0)
                    return;

                AddElement(Resource.Type.MapForward, (int)Orientation.Horizontal);

            }
            else if (direction == Direction.Up)
            {
                relativeDirection = SmartCubeSet.Left;

                if (CreateCrossIfNeed(Direction.Left) == 0)
                    return;

                AddElement(Resource.Type.MapTurn, (int)Orientation.TurnDownLeft);

            }
            else if (direction == Direction.Down)
            {
                relativeDirection = SmartCubeSet.Right;

                if (CreateCrossIfNeed(Direction.Left) == 0)
                    return;


                AddElement(Resource.Type.MapTurn, (int)Orientation.TurnLeftUp);

            }


        }

        public void FinArrow_Click()
        {
            if (elements.Count == 0)
                return;

            switch (direction)
            {

                case Direction.Left:
                    AddElement(Resource.Type.MapEnd, (int)Orientation.Left);
                    break;
                case Direction.Right:
                    AddElement(Resource.Type.MapEnd, (int)Orientation.Right);
                    break;
                case Direction.Up:
                    AddElement(Resource.Type.MapEnd, (int)Orientation.Up);
                    break;
                case Direction.Down:
                    AddElement(Resource.Type.MapEnd, (int)Orientation.Down);
                    break;
            }
            direction = Direction.None;
            relativeDirection = SmartCubeSet.None;
            choiser.Visibility = Visibility.Hidden;
            car.curr_img.Visibility = Visibility.Visible;
        }

        public void RightArrow_Click()
        {

            if (CreateStartIfNotExist(Orientation.Right) == 0)
                return;


            if (direction == Direction.Right)
            {
                relativeDirection = SmartCubeSet.Forward;

                if (CreateCrossIfNeed(Direction.Right) == 0)
                    return;

                AddElement(Resource.Type.MapForward, (int)Orientation.Horizontal);

            }
            else if (direction == Direction.Up)
            {
                relativeDirection = SmartCubeSet.Right;

                if (CreateCrossIfNeed(Direction.Right) == 0)
                    return;

                AddElement(Resource.Type.MapTurn, (int)Orientation.TurnDownRight);

            }
            else if (direction == Direction.Down)
            {
                relativeDirection = SmartCubeSet.Left;

                if (CreateCrossIfNeed(Direction.Right) == 0)
                    return;

                AddElement(Resource.Type.MapTurn, (int)Orientation.TurnRightUp);

            }

        }
        public void UpArrow_Click()
        {
            if (CreateStartIfNotExist(Orientation.Up) == 0)
                return;



            if (direction == Direction.Right)
            {
                relativeDirection = SmartCubeSet.Left;

                if (CreateCrossIfNeed(Direction.Up) == 0)
                    return;

                AddElement(Resource.Type.MapTurn, (int)Orientation.TurnLeftUp);

            }
            else if (direction == Direction.Up)
            {
                relativeDirection = SmartCubeSet.Forward;

                if (CreateCrossIfNeed(Direction.Up) == 0)
                    return;

                AddElement(Resource.Type.MapForward, (int)Orientation.Vertiacal);

            }
            else if (direction == Direction.Left)
            {
                relativeDirection = SmartCubeSet.Right;

                if (CreateCrossIfNeed(Direction.Up) == 0)
                    return;

                AddElement(Resource.Type.MapTurn, (int)Orientation.TurnRightUp);

            }


        }
        public void DownArrow_Click()
        {
            if (CreateStartIfNotExist(Orientation.Down) == 0)
                return;


            if (direction == Direction.Right)
            {
                relativeDirection = SmartCubeSet.Right;

                if (CreateCrossIfNeed(Direction.Down) == 0)
                    return;


                AddElement(Resource.Type.MapTurn, (int)Orientation.TurnDownLeft);

            }
            else if (direction == Direction.Down)
            {
                relativeDirection = SmartCubeSet.Forward;

                if (CreateCrossIfNeed(Direction.Down) == 0)
                    return;


                AddElement(Resource.Type.MapForward, (int)Orientation.Vertiacal);

            }
            else if (direction == Direction.Left)
            {
                relativeDirection = SmartCubeSet.Left;

                if (CreateCrossIfNeed(Direction.Down) == 0)
                    return;

                AddElement(Resource.Type.MapTurn, (int)Orientation.TurnDownRight);

            }
        }


        public List<TypeMap> GetAllowedNext()
        {
            List<TypeMap> ret = new List<TypeMap>();
            foreach (TypeMap suit in Enum.GetValues(typeof(TypeMap)))
            {
                // ...
            }
            return ret;
        }

        private Cell NewEmptyCell()
        {

            return new Cell() { coll = new List<CellElement>() { new CellElement() { type = TypeMap.None } } };
        }

        private CellElement MergeCell(CellElement c1, CellElement c2)
        {
 
            return new CellElement() { type = c1.type | c2.type, element = c2.element };
        }




        private int GetHeight()
        {
            return grid.Count();
        }

        private int GetWidth()
        {
            return grid[0].Count();
        }

        private void InsertRow(int index = 0)
        {

            
            List<Cell> row = new List<Cell>(); 

            int w = GetWidth();
            for (int i = 0; i < w; ++i)
                row.Add(NewEmptyCell());
            grid.Insert(index, row);
            ++height;
        }

        private void AddRow()
        {
            Cell cell = new Cell();
            List<Cell> row = new List<Cell>() { cell };

            int w = GetWidth();
            for (int i = 0; i < w; ++i)
                row.Add(NewEmptyCell());
            grid.Add(row);
            ++height;
        }


        private void InsertCol(int index = 0)
        {
            int h = GetHeight();
            for (int i = 0; i < h; ++i)
                grid[i].Insert(index, NewEmptyCell());

            ++widht;
        }

        private void AddCol()
        {
            int h = GetHeight();
            for (int i = 0; i < h; ++i)
                grid[i].Add(NewEmptyCell());

            ++widht;
        }

        public void LoadProject(string Name)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string dir = (new FileInfo(path)).DirectoryName;
            string fullname = Path.Combine(dir, "Projects", Name);
            XmlReader reader = XmlReader.Create(fullname);
            while (!reader.EOF)
                {
                reader.Read();
                
                switch (reader.Name)
                {
                    case "Element":
                        if (reader.NodeType.ToString() == "Element")
                            Console.WriteLine("ELEMENT Type={0} ImageIdx={1}", reader.GetAttribute("Type"), reader.GetAttribute("ImageIdx"));

                    break;
                    case "Map":
                        if (reader.NodeType.ToString() =="Element")
                            Console.WriteLine("MAP Height={0} Width{1}", reader.GetAttribute("Height"), reader.GetAttribute("Width"));
                        break;
                    case "Cell":
                        if (reader.NodeType.ToString() == "Element")
                            Console.WriteLine("Cell X={0} Y={1} Data={2}", reader.GetAttribute("X"), reader.GetAttribute("Y"), reader.GetAttribute("Data"));

                        break;
                    case "List":
                        if (reader.NodeType.ToString() == "Element")
                            Console.WriteLine("List Data={0}", reader.GetAttribute("Data"));

                        break;
                }
            }
        }


   

        private void SetCell(int x, int y, CellElement val)
        {
            grid[y][x].coll.Add(MergeCell(grid[y][x].GetLast(), val));
        }

        private void ClearCell(int x, int y, int val)
        {
            grid[y][x] = NewEmptyCell();
        }


        public void AddMapElement(TypeMap type, SmartElement sme)
        {
            int nx = (int)pos.X;
            int ny = (int)pos.Y;

            switch (direction)
            {
                case Direction.None:
                    grid[(int)pos.Y][(int)pos.Y].coll.Add(new CellElement() { element = sme, type = type });
                    direction = (Direction)(type);
                    break;
                case Direction.Right:
                    ++nx;
                    if (nx > widht - 1)
                    {
                        AddCol();
                    }
                    direction = (Direction)((int)type - (int)Direction.Left);
                    SetCell(nx, ny, new CellElement() { element = sme, type = type });
                    pos.X = nx;
                    break;

                case Direction.Down:
                    ny++;
                    if (ny > height - 1)
                    {
                        AddRow();
                    }
                    direction = (Direction)((int)type - (int)Direction.Up);
                    SetCell(nx, ny, new CellElement() { element = sme, type = type });
                    pos.Y = ny;
                    break;

                case Direction.Left:
                    --nx;
                    if (nx < 0)
                    {
                        InsertCol(0);
                        pos.X = 0;
                        SetCell(0, ny, new CellElement() { element = sme, type = type });
                    }
                    else
                    {

                        pos.X = nx;
                        SetCell(nx, ny, new CellElement() { element = sme, type = type });
                    }
                    direction = (Direction)((int)type - (int)Direction.Right);


                    break;

                case Direction.Up:
                    --ny;
                    if (ny < 0)
                    {

                        InsertRow(0);
                        pos.Y = 0;
                        SetCell(nx, 0, new CellElement() { element = sme, type = type });
                    }
                    else
                    {
                        pos.Y = ny;
                        SetCell(nx, ny, new CellElement() { element = sme, type = type });
                    }
                    direction = (Direction)((int)type - (int)Direction.Down);


                    break;

            }


        }



        //////////////////////////////////////////////////////////////////

        public void AddElement(Resource.Type type, int image_idx, int position = -1)
        {
            switch (type)
            {
                case Resource.Type.Car:
                    AddElement(new Car(image_idx), elements.Count - 1);

                    break;

                case Resource.Type.MapStart:
                    AddElement(new MapStart(image_idx), elements.Count - 1);

                    break;
                case Resource.Type.MapEnd:
                    AddElement(new MapEnd(image_idx), elements.Count - 1);

                    break;
                case Resource.Type.MapCross:
                    AddElement(new MapCross(image_idx), elements.Count - 1);
                    break;
                case Resource.Type.MapForward:
                    AddElement(new MapForward(image_idx), elements.Count - 1);
                    break;
                case Resource.Type.MapTurn:
                    AddElement(new MapTurn(image_idx), elements.Count - 1);
                    break;

            }


        }

        public Direction GetStartDirection()
        {
            Direction dir = Direction.None;
            if (elements.Count >= 1)
                switch (elements[0].GetImageIndex())
                {
                    case 0:
                        dir = Direction.Down;
                        break;
                    case 1:
                        dir = Direction.Left;
                        break;
                    case 2:
                        dir = Direction.Right;
                        break;
                    case 3:
                        dir = Direction.Up;
                        break;
                }
            return dir;
        }

        public void AddElement(SmartElement sme, int position = -1)
        {

            parent.Children.Add(sme.curr_img);
            if ((sme as Car) != null)
            {
                car = sme;
                Canvas.SetZIndex(car.curr_img, 99);
                Canvas.SetTop(car.curr_img, 0);
                Canvas.SetLeft(car.curr_img, 0);

                return;
            }
            elements.Add(sme);
            AddMapElement(sme.GetMapType(), sme);
            ArrangeMaps();

            if(((SmartElement)(sme)).TypeID() != Resource.Type.MapEnd)
                AddCubeSignal();

        }


        public Point GetStartPosition()
        {
            Point ret = new Point() { X = 0, Y = 0 };
            bool bfind= false;
            if (elements.Count < 1) return ret;

            for (int i = 0; i < height && !bfind; i++)
            {
                for (int j = 0; j < widht && !bfind; j++)
                {

                    foreach (CellElement ce in grid[i][j].coll)
                    {
                        if (ce.element == elements[0])
                        {
                            ret = new Point { Y = i * 120, X = j * 120 };
                            bfind = true;
                            break;
                        }
                    }

                }
            }
            return ret;
        }



        private void ArrangeMaps()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Width:" + widht + " Height:" + height);
            Console.WriteLine("Childrens:" + (parent.Children.Count - 1));
            SetPositionChoiser();

            for (int i = 0; i < height; i++)
            {
                
                for (int j = 0; j < widht; j++)
                {
                    Console.Write('[');
                    foreach (CellElement ce in grid[i][j].coll)
                    {
                        Console.Write(ce.type + " ");

                        if (ce.element != null)
                            ce.element.SetPos(j * 120, i * 120);
                    }
                    Console.Write(']');

                }
                Console.WriteLine();
            }

        }

        private void SetPositionChoiser()
        {
            foreach (Image img in choiser.Children)
            {
                img.Visibility = Visibility.Visible;
            }

            Point posChoiserOffset = new Point(0, 0);
            posChoiser = new Point(this.pos.X, this.pos.Y);
            
            switch (direction)
            {
                case Direction.Left:
                    choiser.Children[2].Visibility = Visibility.Hidden;
                    posChoiserOffset.X = +20;
                    posChoiser.X--;
                    break;
                case Direction.Right:
                    choiser.Children[1].Visibility = Visibility.Hidden;
                    posChoiserOffset.X = -20;
                    posChoiser.X++;
                    break;
                case Direction.Up:
                    choiser.Children[3].Visibility = Visibility.Hidden;
                    posChoiserOffset.Y = +20;
                    posChoiser.Y--;
                    break;
                case Direction.Down:
                    choiser.Children[0].Visibility = Visibility.Hidden;
                    posChoiserOffset.Y = -20;
                    posChoiser.Y++;
                    break;
            }

            if (posChoiser.X < 0)
            {
                InsertCol();
                ++posChoiser.X;
                ++pos.X;
            }

            if (posChoiser.Y < 0)
            {
                InsertRow();
                ++posChoiser.Y;
                ++pos.Y;
            }
            Canvas.SetLeft(choiser, posChoiser.X * 120);
            Canvas.SetTop(choiser, posChoiser.Y * 120);


        }

        public bool MoveAll(int x, int y)
        {
            offsetX += x;
            offsetY += y;
            elements[0].Move(offsetX, offsetY);
            return true;
        }


        public void ReadCommands()
        {
            int i = 0;
            string res = "";
            foreach (SmartElement el in elements)
            {
                res += el.ToString() + ";";
                i++;
            }
            Console.WriteLine(res);
        }

        public void Save(StreamWriter sw = null)
        {
            if (sw == null) return;

            sw.WriteLine("\t<MapElements>");
                foreach (SmartElement el in elements)
                {
                    sw.WriteLine("\t\t<Element Type=\"" + (int)el.GetType() + "\" ImageIdx=\"" + (int)el.GetImageIndex() + "\"/>");
                }
                sw.WriteLine("\t</MapElements>");
                
                sw.WriteLine("\t<Map Height=\"" + height + "\" Width=\"" + widht + "\">");
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < widht; j++)
                    {
                    sw.Write("\t\t<Cell X=\"" + j + "\" Y=\"" + i + "\" Data=\"");
                        foreach (CellElement ce in grid[i][j].coll)
                        {
                            if (ce.element != null)
                            {
                                sw.Write(elements.IndexOf(ce.element) + "|");
                            }
                            else
                                sw.Write("NULL|");
                        };
                    sw.WriteLine ("\" />");
                    }
                }
                sw.WriteLine("\t</Map>");
        }

        public void Load(XPathDocument doc = null, XPathNavigator nav = null)
        {

            LoadMapElements(doc, nav);
            LoadMapInfo(doc, nav);
            LoadMapCell(doc, nav);

            ArrangeMaps();

            choiser.Visibility = Visibility.Hidden;
            car.curr_img.Visibility = Visibility.Visible;
        }
        
        public SmartElement CreateByInfo(Resource.Type typeMap, int index)
        {
            SmartElement ret = null;


            switch (typeMap)
            {
                case Resource.Type.MapCross:
                    ret = new MapCross(index); 
                    break;
                case Resource.Type.MapEnd:
                    ret = new MapEnd(index);
                    break;
                case Resource.Type.MapForward:
                    ret = new MapForward(index);
                    break;
                case Resource.Type.MapStart:
                    ret = new MapStart(index);
                    break;
                case Resource.Type.MapTurn:
                    ret = new MapTurn(index);
                    break;
            }


            return ret;
        }
        
        public void LoadMapElements(XPathDocument doc = null, XPathNavigator nav = null)
        {
            XPathExpression expr;
            expr = nav.Compile("Settings/MapElements/Element");
            XPathNodeIterator iterator = nav.Select(expr);

            while (iterator.MoveNext())
            {
                XPathNavigator nav2 = iterator.Current.Clone();
                int type;
                int index;

                int.TryParse(nav2.GetAttribute("Type", ""), out type);
                int.TryParse(nav2.GetAttribute("ImageIdx", ""), out index);
                SmartElement sme = CreateByInfo((Resource.Type)type, index);

                elements.Add(sme);

                parent.Children.Add(sme.curr_img);
            }
        }

        private void  setElementToCell(int x, int y, List<int> values)
        {
            foreach (int i in values)
            {
                CellElement ce = new CellElement();
                ce.element = elements.ElementAt(i);
                ce.type = elements.ElementAt(i).GetMapType();
                grid[y][x].coll.Add(ce);
 
            }
            ArrangeMaps();
        }


        public void LoadMapCell(XPathDocument doc = null, XPathNavigator nav = null)
        {
            XPathExpression expr;
            expr = nav.Compile("Settings/Map/Cell");
            XPathNodeIterator iterator = nav.Select(expr);
            

                List<int> values = new List<int>();
                while (iterator.MoveNext())
                {
                    XPathNavigator nav2 = iterator.Current.Clone();
                    Console.WriteLine("MapCell X=" + nav2.GetAttribute("X", "") + " Y=" + nav2.GetAttribute("Y", "") + " Data="+ nav2.GetAttribute("Data", ""));

                    int x;
                    int.TryParse(nav2.GetAttribute("X", ""), out x);

                    int y;
                    int.TryParse(nav2.GetAttribute("Y", ""), out y);

                    values.Clear();
                    List <string>  readdata = nav2.GetAttribute("Data", "").Split('|').ToList<string>();
                    readdata.RemoveAt(readdata.Count - 1);

                    if (readdata.Count() > 1)
                    {
                        foreach (string c in readdata)
                        {
                            int iv;
                            int.TryParse(c, out iv);
                            if (c != "NULL" && c!="")
                                values.Add(iv);
                            else
                                Console.WriteLine("NULL get out!");
                        }
                        setElementToCell(x, y, values);
                        
                    }
                }
        }

        private void GridResize(int height, int width)
        {
            
            for (int j = 1; j< width; ++j )
                InsertCol();

            for (int i = 1; i < height; ++i)
                InsertRow();
        }

        public void LoadMapInfo(XPathDocument doc = null, XPathNavigator nav = null)
        {
            XPathExpression expr;
            expr = nav.Compile("Settings/Map");
            XPathNodeIterator iterator = nav.Select(expr);
                while (iterator.MoveNext())
                {
                    XPathNavigator nav2 = iterator.Current.Clone();
                    Console.WriteLine("Map Height=" + nav2.GetAttribute("Height", "") + " Width=" + nav2.GetAttribute("Width", ""));

                    int height;
                    int width;

                    int.TryParse(nav2.GetAttribute("Height", ""), out height);
                    int.TryParse(nav2.GetAttribute("Width", ""), out width);

                    GridResize(height, width);
                }
        }
    }
}
