using UDBase.Installers;
using UDBase.Controllers.LogSystem;
using Zenject;

public class ProjectInstaller : UDBaseInstaller {
	public UnityLog.Settings LogSettings;

	public override void InstallBindings() {
		AddUnityLogger(LogSettings);
		AddEvents();
		AddDirectSceneLoader();
		AddNetUtils();
		AddModeController();
		AddNetworkController();
	}

	void AddModeController() {
		Container.Bind<ModeController>().To<ModeController>().AsSingle();
	}

	void AddNetworkController() {
		Container.Bind<NetworkController>().To<NetworkController>().AsSingle();
	}
}
