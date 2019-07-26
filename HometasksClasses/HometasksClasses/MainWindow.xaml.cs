using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using GameMaps;

namespace HometasksClasses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int count = 0;
        string direction = "left";
        RobotQuestMap q;
        public MainWindow()
        {
            InitializeComponent();
            q = RobotQuests.GetQuest(this, "task1");
            q.SetSolution(Solution);
        }

        ///q.Get___Cell
        ///q.Move__

        void Solution()
        {
            
            if (count == 0)
            {
                if (q.GetUpperCell() == ' '||q.GetUpperCell()=='e')
                {
                    q.MoveUp();
                }
                else 
                {
                    count = 1;
                }
            }
            if (count == 1)
            {
                if (direction == "left")
                {
                    if (q.GetLeftCell() == ' ' || q.GetLeftCell() == 'e')
                    {
                        q.MoveLeft();
                        if (q.GetUpperCell() == ' ' || q.GetUpperCell() == 'e')
                        {
                            direction = "up";
                        }
                    }
                    else 
                    {
                        direction = "down";
                    }
                }
                else if (direction == "up")
                {
                    if (q.GetUpperCell() == ' ' || q.GetUpperCell() == 'e')
                    {
                        q.MoveUp();
                        if (q.GetRightCell() == ' ' || q.GetRightCell() == 'e')
                        {
                            direction = "right";
                        }
                    }
                    else
                    {
                        direction = "left";
                    }
                }
                else if (direction == "right")
                {
                    if (q.GetRightCell() == ' ' || q.GetRightCell() == 'e')
                    {
                        q.MoveRight();
                        if (q.GetLowerCell() == ' ' || q.GetLowerCell() == 'e')
                        {
                            direction = "down";
                        }
                    }
                    else
                    {
                        direction = "up";
                    }
                }
                else
                {
                    if (q.GetLowerCell() == ' ' || q.GetLowerCell() == 'e')
                    {
                        q.MoveDown();
                        if (q.GetLeftCell() == ' ' || q.GetLeftCell() == 'e')
                        {
                            direction = "left";
                        }
                    }
                    else
                    {
                        direction = "right";
                    }
                }
                    

            }
        }


    }
}
