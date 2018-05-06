using UnityEngine;
using UnityEngine.UI;
using UDBase.Controllers.EventSystem;
using GameLogics;
using Zenject;

[RequireComponent(typeof(Text))]
public class TurnView : MonoBehaviour {
	IEvent          _events;
	IGameController _game;
	Text            _text;

	[Inject]
	public void Init(IGameController game, IEvent events) {
		_events = events;
		_game   = game;

		_text = GetComponent<Text>();

		_events.Subscribe<GameState_Updated>(this, OnStateUpdated);
	}

	void Start() {
		UpdateTurnOwner(_game.TurnOwner);
	}

	void OnDestroy() {
		_events.Unsubscribe<GameState_Updated>(OnStateUpdated);
	}

	void OnStateUpdated(GameState_Updated e) {
		UpdateTurnOwner(e.State.GetTurnOwner());
	}

	void UpdateTurnOwner(string owner) {
		_text.text = $"'{owner}' Turn";
	}
}
