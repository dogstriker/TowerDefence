using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public interface IBehavior
    {
        void Act();
        string Name { get; }
        void SetUnit(UGameObjectBase g, string behaviorName);
        void Init(params object[] par);
        void RemoveNested();
    }
}
