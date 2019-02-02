namespace LineGame.Core {
	internal interface IObserver {
		void Notify(IObservable sender);
	}
}