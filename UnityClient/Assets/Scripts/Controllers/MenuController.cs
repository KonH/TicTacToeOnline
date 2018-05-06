using UnityEngine;
using UnityEngine.UI;
using UDBase.UI.Common;
using UDBase.Controllers.LogSystem;
using UDBase.Controllers.SceneSystem;
using UDBase.Controllers.EventSystem;
using Zenject;

public class MenuController : MonoBehaviour, ILogContext {
	public GameObject NetworkConnectionOverlay;
	public GameObject NetworkErrorOverlay;
	public Button     LocalPlayButton;
	public Button     NetworkPlayButton;

	UIManager         _ui;
	ModeController    _mode;
	NetworkController _network;
	ILog              _log;
	IScene            _scene;
	IEvent            _event;

	[Inject]
	public void Init(UIManager ui, ModeController mode, NetworkController network, ILog log, IScene scene, IEvent events) {
		_ui      = ui;
		_mode    = mode;
		_network = network;
		_log     = log;
		_scene   = scene;
		_event   = events;

		_event.Subscribe<Network_ConnectComplete>(this, OnNetworkConnectComplete);

		LocalPlayButton.onClick.AddListener(OnLocalPlayButton);
		NetworkPlayButton.onClick.AddListener(OnNetworkPlayButton);
	}

	void OnDestroy() {
		_event.Unsubscribe<Network_ConnectComplete>(OnNetworkConnectComplete);
	}

	void OnLocalPlayButton() {
		_log.Message(this, "OnLocalPlayButton");
		_mode.SelectGameMode(GameMode.Local);
		StartGame();
	}

	void OnNetworkPlayButton() {
		_log.Message(this, "OnNetworkPlayButton");
		ShowConnectionOverlay();
		_network.TryConnectToServer();
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

	void OnNetworkConnectComplete(Network_ConnectComplete e) {
		if ( !e.Success ) {
			_ui.HideAll();
			ShowNetworkErrorOverlay();
		}
	}
}
