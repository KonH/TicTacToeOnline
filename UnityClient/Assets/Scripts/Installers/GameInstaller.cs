using Zenject;

public class GameInstaller : MonoInstaller {
	
	public override void InstallBindings() {
		AddGameController();
	}

	void AddGameController() {
		Container.Bind(
			typeof(IInitializable), typeof(IGameController)
		).To<LocalGameController>().AsSingle().NonLazy();
	}
}
