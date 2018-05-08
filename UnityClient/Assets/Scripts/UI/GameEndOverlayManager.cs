using UnityEngine;
using UDBase.Controllers.EventSystem;
using Zenject;
using GameLogics;
using UDBase.UI.Common;

public class GameEndOverlayManager : MonoBehaviour {
	public GameObject GameEndWindowPrefab;
	public GameObject NetworkErrorWindowPrefab;

	IGameController  _game;
	IEvent           _events;
	UIManager        _ui;

	[Inject]
	public void Init(IGameController game, IEvent events, UIManager ui) {
		_game   = game;
		_events = events;
		_ui     = ui;
		_events.Subscribe<GameState_Updated>(this, OnStateUpdated);
		_events.Subscribe<Network_Error>    (this, OnNetworkError);
	}

	void OnDestroy() {
		_events.Unsubscribe<GameState_Updated>(OnStateUpdated);
		_events.Unsubscribe<Network_Error>    (OnNetworkError);
	}

	void OnStateUpdated(GameState_Updated e) {
		var result = _game.Result;
		if ( result != null ) {
			_ui.ShowOverlay(GameEndWindowPrefab, () => _game.GoBackToMenu());
		}
	}

	void OnNetworkError(Network_Error e) {
			_ui.ShowOverlay(NetworkErrorWindowPrefab, () => _game.GoBackToMenu());
	}
}
