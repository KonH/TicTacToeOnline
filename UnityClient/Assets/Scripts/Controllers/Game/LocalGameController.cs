using UDBase.Controllers.LogSystem;
using UDBase.Controllers.EventSystem;
using UDBase.Controllers.SceneSystem;
using GameLogics;

public class LocalGameController : BaseGameController {
	public LocalGameController(ILog log, IEvent events, IScene scene) : base(log, events, scene, new GameState(3, "X", "O")) { }

	public override bool IsTurnAvailable() => true;

	protected override void OnValidIntentCreated(Intent intent) {
		_state = Logics.ExecuteIntent(_state, intent);
		OnStateUpdated();
	}
}
