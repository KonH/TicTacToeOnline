using UnityEngine;
using UnityEngine.UI;
using UDBase.UI.Common;
using UDBase.Controllers.LogSystem;
using UDBase.Controllers.SceneSystem;
using Zenject;

public class MenuController : MonoBehaviour, ILogContext {
	public GameObject NetworkConnectionOverlay;
	public GameObject NetworkErrorOverlay;
	public Button     LocalPlayButton;
	public Button     NetworkPlayButton;

	UIManager      _ui;
	ModeController _mode;
	ILog           _log;
	IScene         _scene;

	[Inject]
	public void Init(UIManager ui, ModeController mode, ILog log, IScene scene) {
		_ui    = ui;
		_mode  = mode;
		_log   = log;
		_scene = scene;

		LocalPlayButton.onClick.AddListener(OnLocalPlayButton);
		NetworkPlayButton.onClick.AddListener(OnNetworkPlayButton);
	}

	void OnLocalPlayButton() {
		_log.Message(this, "OnLocalPlayButton");
		_mode.SelectGameMode(GameMode.Local);
		StartGame();
	}

	void OnNetworkPlayButton() {
		_log.Message(this, "OnNetworkPlayButton");
		ShowNetworkErrorOverlay();
	}

	void StartGame() {
		_scene.LoadScene("Game");
	}

	void ShowConnectionOverlay() {
		_ui.ShowOverlay(NetworkConnectionOverlay, null);
	}

	void ShowNetworkErrorOverlay() {
		_ui.ShowOverlay(NetworkErrorOverlay, null);
	}
}
