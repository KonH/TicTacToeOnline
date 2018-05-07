using Zenject;

public class GameInstaller : MonoInstaller {
	
	public override void InstallBindings() {
		AddGameController();
	}

	void AddGameController() {
		var mode = Container.Resolve<ModeController>().Mode;
		if ( mode == GameMode.Network ) {
			Container.Bind(
			typeof(IInitializable), typeof(IGameController)
			).To<NetworkGameController>().AsSingle().NonLazy();
		} else {
			Container.Bind(
			typeof(IInitializable), typeof(IGameController)
			).To<LocalGameController>().AsSingle().NonLazy();
		}
	}
}
