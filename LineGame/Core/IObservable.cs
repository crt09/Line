namespace LineGame.Core {
	internal interface IObservable {
		void AddObserver(IObserver observer);
		void RemoveObserver(IObserver observer);
		void NotifyAllObservers();
	}
}