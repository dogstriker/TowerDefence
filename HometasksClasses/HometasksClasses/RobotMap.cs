using System;
using System.Windows.Media;
using GameMaps;

namespace HometasksClasses
{
    public class RobotMap
    {

        char[,] cMap;
        UniversalMap_Wpf map;
        Random r = new Random();

        int ex, ey;

        public RobotMap(UniversalMap_Wpf _map)
        {
            map = _map;
            cMap = new char[map.XCells, map.YCells];
            for (int i = 1; i < map.XCells - 1; i++)
            {
                for (int j = 1; j < map.YCells - 1; j++)
                {
                    cMap[i, j] = ' ';
                }
            }

            for (int i = 0; i < map.XCells; i++)
            {
                cMap[i, 0] = '*';
                cMap[i, map.YCells - 1] = '*';
            }

            for(int i = 0; i < map.YCells; i++)
            {
                cMap[0, i] = '*';
                cMap[map.XCells - 1, i] = '*';
            }
        }

        public RobotMap(UniversalMap_Wpf _map, char[,] _cMap)
        {
            map = _map;
            cMap = _cMap;
        }

        public char GetCell(int x, int y)
        {
            char c = '\0';
            if (x >= 0 && x < map.XCells && y >= 0 && y < map.YCells)
                c = cMap[x, y];
            return c;
        }

        #region Размещение выхода
        public void SetExit(ExitPlacement exitPlacement, int x = 1, int y = 1)
        {
            switch (exitPlacement)
            {
                case ExitPlacement.RandomCorner:
                    exitRandomCorner();
                    break;
                case ExitPlacement.Random:
                    exitRandom();
                    break;
                case ExitPlacement.Fixed:
                    ex = x; ey = y;
                    break;
            }
            cMap[ex, ey] = 'e';
            DrawRectangleInCell(ex, ey, Brushes.Lime);
        }

        private void exitRandomCorner()
        {
            var t = r.Next(0, 100);
            if (t < 25)
            {
                ex = 1; ey = 1;
            }
            else if (t < 50)
            {
                ex = map.XCells - 2; ey = 1;
            }
            else if (t < 75)
            {
                ex = map.XCells - 2; ey = map.YCells - 2;
            }
            else
            {
                ex = 1; ey = map.YCells - 2;
            }
            cMap[ex, ey] = 'e';
        }

        void exitRandom()
        {
            int N = 0;
            int rx, ry;
            while (true)
            {
                rx = r.Next(1, map.XCells - 1);
                ry = r.Next(1, map.YCells - 1);
                N++;
                if (cMap[rx, ry] == ' ' || N > 10000) break;
            }
        }


        #endregion

        #region Размещение робота
        int rx = 0, ry = 0;

        public Coordinate SetRobot(RobotPlacement robotPlacement)
        {
            int N = 0;
            
            switch (robotPlacement)
            {
                case RobotPlacement.LeftCol:
                    while (true)
                    {
                        rx = 1; ry = r.Next(1, map.YCells - 1);
                        N++;
                        if (cMap[rx, ry] == ' ' || N > 10000) break;
                    }
                    break;
                case RobotPlacement.RightCol:
                    while (true)
                    {
                        rx = map.XCells - 2; ry = r.Next(1, map.YCells - 1);
                        N++;
                        if (cMap[rx, ry] == ' ' || N > 10000) break;
                    }
                    break;
                case RobotPlacement.UpperRow:
                    while (true)
                    {
                        rx = r.Next(1, map.XCells - 1); ry = 1;
                        N++;
                        if (cMap[rx, ry] == ' ' || N > 10000) break;
                    }
                    break;
                case RobotPlacement.LowerRow:
                    while (true)
                    {
                        rx = r.Next(1, map.XCells - 1); ry = map.YCells - 2;
                        N++;
                        if (cMap[rx, ry] == ' ' || N > 10000) break;
                    }
                    break;
                case RobotPlacement.Random:
                    while (true)
                    {
                        rx = r.Next(1, map.XCells - 1); ry = r.Next(1, map.YCells - 1);
                        N++;
                        if (cMap[rx, ry] == ' ' || N > 10000) break;
                    }
                    break;
            }
            var c = new Coordinate(rx, ry);
            cMap[rx, ry] = 'r';
            return c;
        }

        
        public void RemoveRSymbol()
        {
    
        }
        #endregion

        #region Размещение препятствий

        public void PlaceObstacles(ObstaclesFormation formation, object par = null)
        {
            switch (formation)
            {
                case ObstaclesFormation.SeparatedBlocks:
                    placeSeparatedBlocks();
                    break;
            }

            for (int i = 0; i < map.XCells; i++)
            {
                for (int j = 0; j < map.YCells; j++)
                {
                    if (cMap[i, j] == '*')
                    {
                        DrawRectangleInCell(i, j, Brushes.Gray);
                    }
                }
            }


        }

        private void placeSeparatedBlocks()
        {
            int len = (map.XCells + map.YCells) / 6;
            // линии
            for (int i = len; i >= 1; i--)
            {
                for (int j = 1; j <= len - i + 1; j++)
                {
                    placeLinearObstacle(i, 1);
                }
            }

            // квадраты и прямоугольники
            for (int i = len; i > 1; i--)
            {
                for (int j = i; j > 1; j--)
                {
                    for (int k = 1; k < len - i + 1; k++)
                    {
                        placeLinearObstacle(i, j);
                    }
                }
            }
        }


        private bool checkRectangle(int x1, int y1, int x2, int y2)
        {
            if (x1 > 1) x1--;
            if (y1 > 1) y1--;
            if (x2 < map.XCells - 2) x2++;
            if (y2 < map.YCells - 2) y2++;
            bool b = true;
            try
            {
                for (int i = x1; i <= x2; i++)
                {
                    for (int j = y1; j <= y2; j++)
                    {
                        if (cMap[i, j] != ' ')
                        {
                            b = false;
                            break;
                        }
                    }
                    if (!b) break;
                }
            }
            catch
            {
                b = false;
            }
            return b;
        }

        private void placeLinearObstacle(int l, int h)
        {
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            int N = 0; // количество попыток
            int dx, dy;
            if (r.Next(0, 100) < 50)
            {
                dx = h - 1; dy = l - 1;
            }
            else
            {
                dx = l - 1; dy = h - 1;
            }

            while (true)
            {
                x1 = r.Next(1, map.XCells - 1);
                y1 = r.Next(1, map.YCells - 1);
                x2 = x1 + dx;
                y2 = y1 + dy;
                N++;
                if (x1 == -1)
                    N++;
                if (x2 < map.XCells && y2 < map.YCells && checkRectangle(x1, y1, x2, y2)
                    || N > 1000) break;
            }

            if (N <= 1000)
            {
                for (int i = x1; i <= x2; i++)
                {
                    for (int j = y1; j <= y2; j++)
                    {
                        cMap[i, j] = '*';
                    }
                }
            }

        }
        #endregion
      
        #region Рисование

        private void DrawRectangleInCell(int x, int y, Brush color)
        {
            map.DrawRectangle(x * map.CellSize + 2, y * map.CellSize + 2, map.CellSize - 4, map.CellSize - 4, color, color);
        }
        #endregion
    }

}
