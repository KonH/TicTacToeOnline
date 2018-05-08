using UDBase.Controllers.LogSystem;
using UDBase.Controllers.EventSystem;
using UDBase.Controllers.SceneSystem;
using GameLogics;

public class NetworkGameController : BaseGameController {
	NetworkController _network;

	public NetworkGameController(ILog log, IEvent events, IScene scene, NetworkController network) : base(log, events, scene, null) {
		_network = network;
		_state = _network.State;

		_event.Subscribe<Network_StateUpdated>(this, OnStateUpdated);
	}

	void OnStateUpdated(Network_StateUpdated e) {
		_state = e.State;
		_event.Fire<GameState_Updated>(new GameState_Updated(_state));
	}

	public override bool IsTurnAvailable() {
		return _network.PlayerName == _state.GetTurnOwner();
	}

	protected override void OnValidIntentCreated(Intent intent) {
		_network.SendIntent(intent);
	}
}
