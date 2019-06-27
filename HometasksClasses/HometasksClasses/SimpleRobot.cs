using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using GameMaps;

namespace HometasksClasses
{


    public enum RobotCommands
    {
        //MoveLeft,
        //MoveRight,
        //MoveUp,
        //MoveDown,
        Move,
        Destroy,
        ChangeHp,
        Win
    }

    public class RobotAction
    {
        public RobotCommands Command { get; }
        public object Data { get; }
        public RobotAction(RobotCommands _rCommand, object _data = null)
        {
            Command = _rCommand;
            Data = _data;
        }
    }

    public class SimpleRobot
    {

        #region Объявления
        public SimpleRobot(UniversalMap_Wpf _map, RobotMap _cMap, Coordinate c, int _hp = 20) :
            this(_map, _cMap, c.X, c.Y, _hp)
        {
        }

        public SimpleRobot(UniversalMap_Wpf _map, RobotMap _cMap, int _x, int _y, int _hp = 20)
        {
            map = _map;
            cMap = _cMap;
            X = _x;
            Y = _y;
            HP = _hp;
            IsOk = true;

            rules.Add(' ', new List<RobotAction> { new RobotAction(RobotCommands.Move) });
            rules.Add('r', new List<RobotAction> { new RobotAction(RobotCommands.Move) });
            rules.Add('*', new List<RobotAction> { new RobotAction(RobotCommands.Destroy) });
            rules.Add('e', new List<RobotAction> {
                new RobotAction(RobotCommands.Move),
                new RobotAction(RobotCommands.Win)
            });

            DrawCircle(X, Y, Brushes.Magenta);

        }



        Dictionary<char, List<RobotAction>> rules = new Dictionary<char, List<RobotAction>>();

        int HP;

        public bool IsOk { get; private set; }

        Random r = new Random();


        private UniversalMap_Wpf map;
        RobotMap cMap;

        public int X { get; private set; }
        public int Y { get; private set; }


        int nextX, nextY;

        #endregion

        void DrawCircle(int x, int y, Brush color)
        {
            map.DrawCircle(x * map.CellSize + map.CellSize / 2, y * map.CellSize + map.CellSize / 2,
    map.CellSize / 2 - 3, color, color);
        }


        void RemoveCircle(int x, int y)
        {
            map.RemoveCircle(x * map.CellSize + map.CellSize / 2, y * map.CellSize + map.CellSize / 2,
    map.CellSize / 2 - 3);
        }



        public void AddRule(char symbol, List<RobotAction> action)
        {
            rules.Add(symbol, action);
        }


        #region Движение робота
        public void MoveLeft()
        {
            if (IsOk)
            {
                nextX = X - 1;
                nextY = Y;
                Execute(rules[cMap.GetCell(nextX, nextY)]);
            }
        }

        public void MoveRight()
        {
            if (IsOk)
            {
                nextX = X + 1;
                nextY = Y;
                Execute(rules[cMap.GetCell(nextX, nextY)]);
            }
        }

        public void MoveUp()
        {
            if (IsOk)
            {
                nextX = X;
                nextY = Y - 1;
                Execute(rules[cMap.GetCell(nextX, nextY)]);
            }
        }

        public void MoveDown()
        {
            if (IsOk)
            {
                nextX = X;
                nextY = Y + 1;
                Execute(rules[cMap.GetCell(nextX, nextY)]);
            }
        }


        #endregion

        #region Выполнение команд
        void Execute(List<RobotAction> act)
        {
            for (int i = 0; i < act.Count; i++)
            {
                ExecuteSingleAction(act[i]);
            }
        }

        void ExecuteSingleAction(RobotAction act)
        {
            switch(act.Command)
            {
                case RobotCommands.Move:
                    Move();
                    break;
                case RobotCommands.Destroy:
                    Die();
                    break;
                case RobotCommands.Win:
                    IsOk = false;
                    RemoveCircle(X, Y);
                    DrawCircle(X, Y, Brushes.Yellow);
                    break;
            }

        }

        void Move()
        {
            DrawCircle(nextX, nextY, Brushes.Magenta);
            RemoveCircle(X, Y);
            X = nextX;
            Y = nextY;
        }

        void Die()
        {
            RemoveCircle(X, Y);
            DrawCircle(X, Y, Brushes.Black);
            IsOk = false;
        }
        #endregion

    }

    public enum ObstaclesFormation
    {
        SeparatedBlocks
    }

    public enum ExitPlacement
    {
        RandomCorner,
        Random,
        Fixed
    }

    public enum RobotPlacement
    {
        LeftCol,
        RightCol,
        UpperRow,
        LowerRow,
        Random,
        Fixed
    }

}
