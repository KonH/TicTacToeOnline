using Zenject;

public class MenuInstaller : MonoInstaller {
	public MenuController Controller;

	public override void InstallBindings() {
		AddMenuController();
	}

	void AddMenuController() {
		Container.Bind<MenuController>().To<MenuController>().FromInstance(Controller);
	}
}
