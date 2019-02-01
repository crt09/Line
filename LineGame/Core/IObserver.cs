namespace LineGame.Core {
	public interface IObserver {
		void Notify(IObservable sender);
	}
}