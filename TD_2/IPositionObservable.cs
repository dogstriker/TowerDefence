namespace TowerDefence
{
    public interface IPositionObservable
    {
        void RegisterObserver(IPositionObserver ob);
        void RemoveObserver(IPositionObserver ob);
        void NotifyObservers();
    }

}
