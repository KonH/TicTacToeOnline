using UDBase.Controllers.LogSystem;
using UDBase.Controllers.SceneSystem;
using UDBase.Controllers.EventSystem;
using GameLogics;

public abstract class BaseGameController : IGameController {

	public int        FieldSize => _state.Field.Size;
	public int        Players   => _state.Players.Count;
	public GameResult Result    => Logics.TryGetResult(_state);

	protected readonly ILog   _log;
	protected readonly IEvent _event;
	protected readonly IScene _scene;

	protected GameState _state;

	public BaseGameController(ILog log, IEvent events, IScene scene, GameState state) {
		_log   = log;
		_event = events;
		_scene = scene;
		_state = state;
	}

	public void Initialize() {
		_log.MessageFormat(this, "Init: fieldSize: {0}, players: {1}", FieldSize, Players);
	}

	public void OnCellClick(int x, int y) {
		var intent = new Intent(_state.GetTurnOwner(), x, y);
		if ( Logics.IsIntentValid(_state, intent) ) {
			OnValidIntentCreated(intent);
		}
	}

	protected abstract void OnValidIntentCreated(Intent intent);

	protected void OnStateUpdated() {
		_event.Fire(new GameState_Updated(_state));
	}

	public void GoBackToMenu() {
		_scene.LoadScene("MainMenu");
	}
}
