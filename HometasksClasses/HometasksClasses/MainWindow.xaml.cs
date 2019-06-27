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
            q.MoveUp();
        }


    }
}
