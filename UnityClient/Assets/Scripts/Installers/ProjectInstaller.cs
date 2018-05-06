using UDBase.Installers;
using UDBase.Controllers.LogSystem;
using Zenject;

public class ProjectInstaller : UDBaseInstaller {
	public UnityLog.Settings LogSettings;

	public override void InstallBindings() {
		AddUnityLogger(LogSettings);
		AddEvents();
		AddDirectSceneLoader();
		AddModeController();
	}

	void AddModeController() {
		Container.Bind<ModeController>().To<ModeController>().AsSingle();
	}
}
