using System;
using System.Collections.Generic;
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
using GameMaps;
namespace TowerDefence
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
   
    
    public partial class MainWindow : Window
    {

        IGameScreenLayout Lay;
        CellMapInfo MapInfo;
       
        InventoryPanel unitsPanel;
        string tankName;
        
        Game game = new Game(2);
        public MainWindow()
        {
            UGameObjectBase.game = game;
            Behavior.game = game;
            InitializeComponent();
            Lay = LayoutsFactory.GetLayout(LayoutType.Vertical_1, this.Content);
            MapInfo = new CellMapInfo(20, 16, 30, 5);
            game.Map = MapCreator.GetUniversalMap(this, MapInfo);
            Lay.Attach(game.Map, 0);
            game.Map.DrawGrid();
            unitsPanel = new InventoryPanel(game.Map.Library, game.Map.CellSize);
            Lay.Attach(unitsPanel, 1);
            unitsPanel.SetBackground(Brushes.Wheat);
            AddPictures();
            unitsPanel.AddItem("light tank", "tank1", "light tank");
            unitsPanel.SetMouseClickHandler(CheckInventoryClick);
            game.Map.Mouse.SetMouseSingleLeftClickHandler(PlaceTank);
            game.AddBase(game.Map.XAbsolute / 2, game.Map.YAbsolute / 2, "base");

            game.AddObject("SimpleFlyer", new GOParams { X = game.Map.XAbsolute, Y = game.Map.YAbsolute });
            
            
        }
        void AddPictures()
        {
            game.Map.Library.ImagesFolder = new PathInfo { Path = "..\\..\\images", Type = PathType.Relative };
            ReadPictures("platform", "Red", 4);
            ReadPictures("flyer", "Red", 3);
            ReadPictures("Missile", "Red", 1);
            game.Map.Library.AddPicture("base", "fanning_unit.png");
            ReadPictures("exp", "", 9);
            ReadPictures("tower", "Sand", 8);
            ReadPictures("tower", "Red", 3);
            ReadPictures("platform", "Sand", 8);
            game.Map.Library.AddPicture("tank1", "tank1.png");
            string[] s = new string[9];
            for (int i = 1; i <= 9; i++)
            {
                s[i - 1] = "exp" + i.ToString();
            }
            AnimationDefinition a = new AnimationDefinition();
            a.AddEqualFrames(50, s);
            game.Map.Library.AddAnimation("explosion", a);
        }
        
        void ReadPictures(string Name,string Type,int number)
        {
            for (int i = 1; i <= number; i++)
            {
                game.Map.Library.AddPicture(Name + Type + i.ToString(), Name + Type + i.ToString() + ".png");
            }
        }

        void CheckInventoryClick(string s)
        {
            switch (s)
            { 
                case "light tank":
                    unitsPanel.SetItemBackground("light tank",Brushes.LightGreen);
                    tankName = "light tank";
                    break;
            }

        }

        void PlaceTank(int x1, int y1, int x2, int y2)
        {
            if (!(tankName == ""))
            {
                switch (tankName)
                {
                    case "light tank":
                        //game.AddTank(x1, y1, new string[] { "platformSand1", "towerSand3" }, 2);
                        break;
                }
            }
        }
    }
    
} 
