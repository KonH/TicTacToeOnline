using Zenject;

public class GameInstaller : MonoInstaller {
	GameMode _mode;

	[Inject]
	public void Init(ModeController mode) {
		_mode = mode.Mode;
	}

	public override void InstallBindings() {
		AddGameController();
	}

	void AddGameController() {
		if ( _mode == GameMode.Network ) {
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
