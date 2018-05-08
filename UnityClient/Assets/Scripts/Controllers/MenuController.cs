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
		_event.Subscribe<Network_Error>          (this, OnNetworkError);
		_event.Subscribe<Network_StateUpdated>   (this, OnNetworkStateUpdated);


		LocalPlayButton.onClick.AddListener(OnLocalPlayButton);
		NetworkPlayButton.onClick.AddListener(OnNetworkPlayButton);
	}

	void OnDestroy() {
		_event.Unsubscribe<Network_ConnectComplete>(OnNetworkConnectComplete);
		_event.Unsubscribe<Network_Error>          (OnNetworkError);
		_event.Unsubscribe<Network_StateUpdated>   (OnNetworkStateUpdated);
	}

	void OnLocalPlayButton() {
		_log.Message(this, "OnLocalPlayButton");
		_mode.SelectGameMode(GameMode.Local);
		StartGame();
	}

	void OnNetworkPlayButton() {
		_log.Message(this, "OnNetworkPlayButton");
		_mode.SelectGameMode(GameMode.Network);
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

	void OnNetworkError() {
		_ui.HideAll();
		ShowNetworkErrorOverlay();
	}

	void OnNetworkConnectComplete(Network_ConnectComplete e) {
		if ( !e.Success ) {
			OnNetworkError();
		}
	}

	void OnNetworkError(Network_Error e) {
		OnNetworkError();
	}

	void OnNetworkStateUpdated(Network_StateUpdated e) {
		if ( e.State != null ) {
			StartGame();
		}
	}
}
