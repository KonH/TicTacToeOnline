using UDBase.Installers;
using UDBase.Controllers.LogSystem;
using Zenject;

public class GameInstaller : UDBaseInstaller {
	public UnityLog.Settings LogSettings;

	public override void InstallBindings() {
		AddUnityLogger(LogSettings);
		AddEvents();
		AddGameController();
	}

	void AddGameController() {
		Container.Bind(
			typeof(IInitializable), typeof(GameController)
		).To<GameController>().AsSingle().NonLazy();
	}
}
