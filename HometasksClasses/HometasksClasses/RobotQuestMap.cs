using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using GameMaps;

namespace HometasksClasses
{
    public class RobotQuestMap
    {
        public RobotQuestMap(UniversalMap_Wpf wMap, RobotMap _rMap, SimpleRobot _rBot)
        {
            rBot = _rBot;
            rMap = _rMap;
            wMap.Keyboard.SetSingleKeyEventHandler(Launcher);
        }

        SimpleRobot rBot;

        RobotMap rMap;

        #region Команды
        public void MoveLeft()
        {
            rBot.MoveLeft();
        }

        public void MoveRight()
        {
            rBot.MoveRight();
        }

        public void MoveUp()
        {
            rBot.MoveUp();
        }

        public void MoveDown()
        {
            rBot.MoveDown();
        }

        public char GetLeftCell()
        {
            return rMap.GetCell(rBot.X - 1, rBot.Y);
        }

        public char GetRightCell()
        {
            return rMap.GetCell(rBot.X + 1, rBot.Y);
        }

        public char GetUpperCell()
        {
            return rMap.GetCell(rBot.X, rBot.Y - 1);
        }

        public char GetLowerCell()
        {
            return rMap.GetCell(rBot.X, rBot.Y + 1);
        }

        #endregion

        #region Запуск решения
        Action solution;
        public void SetSolution(Action _solution)
        {
            solution = _solution;
        }

        int nn = 0;
        void _tCallback(object state = null)
        {
            Debug.WriteLine("Invoked " + (++nn).ToString());

            if (solution != null)
            {
                Application.Current?.Dispatcher.Invoke(solution);
            }
        }
        bool isLaunched = false;

        private Timer executor;
        private void Launcher(Key k)
        {
            if (k == Key.Space)
            {
                if (!isLaunched)
                {
                    // solution();
                    executor = new Timer(_tCallback, null, 0, 300);
                    isLaunched = true;
                }
            }
        }
        #endregion

    }

}
