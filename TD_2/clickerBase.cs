using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public abstract class clickerBase
    {
        public abstract void Clicked(UGameObjectBase g);
        
    }
    public class storeLeftClick : clickerBase
    {

        public override void Clicked(UGameObjectBase g)
        {
            MainWindow.game.ClickedObj = g;
        }
    }
}
