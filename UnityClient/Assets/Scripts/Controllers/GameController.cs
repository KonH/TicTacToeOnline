using UDBase.Controllers.LogSystem;
using UDBase.Controllers.EventSystem;
using Zenject;
using GameLogics;

public class GameController: IInitializable, ILogContext {
	readonly ILog   _log;
	readonly IEvent _event;

	public int FieldSize => _state.Field.Size;
	public int Players   => _state.Players.Count;

	GameState _state;

	public GameController(ILog log, IEvent events) {
		_log   = log;
		_event = events;

		_state = new GameState(3, "X", "O");
	}

	public void Initialize() {
		_log.MessageFormat(this, "Init: fieldSize: {0}, players: {1}", FieldSize, Players);
	}

	public void OnCellClick(int x, int y) {
		var intent = new Intent(_state.GetTurnOwner(), x, y);
		if ( Logics.IsIntentValid(_state, intent) ) {
			_state = Logics.ExecuteIntent(_state, intent);
			OnStateUpdated();
		}
	}

	void OnStateUpdated() {
		_event.Fire(new GameState_Updated(_state));
	}
}
