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
using GameMaps.Layouts;
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
        TextArea_Vertical info;
        static public Game game = new Game(2);
        public MainWindow()
        {
            UGameObjectBase.game = game;
            Behavior.game = game;
            InitializeComponent();
            Lay = LayoutsFactory.GetLayout(LayoutType.Vertical, this.Content);
            MapInfo = new CellMapInfo(50, 31, 30, 5);
            game.Map = MapCreator.GetUniversalMap(this, MapInfo);
            game.Map.Mouse.SetMouseSingleLeftClickHandler(game.setMovementGoalByClick);
            Lay.Attach(game.Map, 0);
            //game.Map.DrawGrid();
            unitsPanel = new InventoryPanel(game.Map.Library, game.Map.CellSize);
            
            Lay.Attach(unitsPanel, 1);
            unitsPanel.SetBackground(Brushes.Wheat);
            game.Map.SetMapBackground(Brushes.Black);

            info = new TextArea_Vertical();
            Lay.Attach(info, 1);
            info.AddTextBlock("Resources");
            
            AddPictures();
            unitsPanel.AddItem("allyLightTank", "tank1", "Light Tank");
            unitsPanel.SetMouseClickHandler(CheckInventoryClick);
            unitsPanel.AddItem("allyMediumTank", "MediumTank", "Medium Tank");
            unitsPanel.AddItem("scavenger", "scavenger", "scavenger");
            game.timer.AddAction(ShowResources, 1000);
            
            game.AddBase(game.Map.XAbsolute / 2, game.Map.YAbsolute / 2, "base");
            game.CreateTank("scavenger", 500, 500);
           // game.AddObject("SimpleFlyer", new GOParams { X = game.Map.XAbsolute, Y = game.Map.YAbsolute });
            //game.CreateTank("enemyLightTank", 1300, 500);
           
            game.CreateTank("Baneblade",100, 200);
            game.CreateTank("enemyLightTank", 5, 500);
            
        
        }
        void AddPictures()
        {
            game.Map.Library.ImagesFolder = new PathInfo { Path = "..\\..\\images", Type = PathType.Relative };
            ReadPictures("platform", "Red", 4);
            ReadPictures("flyer", "Red", 3);
            ReadPictures("flyer", "Sand", 4);
            ReadPictures("Missile", "Red", 1);
            game.Map.Library.AddPicture("base", "fanning_unit.png");
            ReadPictures("exp", "", 9);
            ReadPictures("tower", "Sand", 8);
            ReadPictures("tower", "Red", 3);
            ReadPictures("platform", "Sand", 9);
            ReadPictures("exp4-", "", 16);
            ReadPictures("blast", "", 9);
            game.Map.Library.AddPicture("MediumTank", "MediumTank.png");
            game.Map.Library.AddPicture("scavenger", "scavenger.png");
            game.Map.Library.AddPicture("tank1", "tank1.png");
            game.Map.Library.AddPicture("platformRedDestroyed3", "platformRedDestroyed3.png");
            string[] s = new string[9];
            for (int i = 1; i <= 9; i++)
            {
                s[i - 1] = "exp" + i.ToString();
            }
            AnimationDefinition a = new AnimationDefinition();
            a.AddEqualFrames(50, s);
            game.Map.Library.AddAnimation("explosion", a);
        }
        void ShowResources()
        {
            info.SetText("Resources", game.totalResources.ToString() + " scrap");
        }
        void ReadPictures(string Name,string Type,int number)
        {
            for (int i = 1; i <= number; i++)
            {
                game.Map.Library.AddPicture(Name + Type + i.ToString(), Name + Type + i.ToString() + ".png");
            }
        } 

        void CheckInventoryClick(string s)
            //TODO: проверить колво ресурсов
        {
            switch (s)
            {
                case "allyLightTank":
                    unitsPanel.SetItemBackground("allyLightTank", Brushes.LightGreen);
                    
                    game.CreateTank("allyLightTank", (int)game.Base.Par.X, (int)game.Base.Par.Y);
                    break;
                case "allyMediumTank":
                    unitsPanel.SetItemBackground("allyMediumTank", Brushes.LightGreen);
                    
                    game.CreateTank("allyMediumTank", (int)game.Base.Par.X, (int)game.Base.Par.Y);
                    break;
                case "scavenger":
                    unitsPanel.SetItemBackground("scavenger", Brushes.LightGreen);
                    
                    game.CreateTank("scavenger", (int)game.Base.Par.X, (int)game.Base.Par.Y);
                    break;
            }

        }

       
    }
    
} 
