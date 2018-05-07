using UDBase.Controllers.LogSystem;
using UDBase.Controllers.EventSystem;
using UDBase.Controllers.SceneSystem;
using GameLogics;

public class NetworkGameController : BaseGameController {
	public NetworkGameController(ILog log, IEvent events, IScene scene) : base(log, events, scene, null) { }

	protected override void OnValidIntentCreated(Intent intent) {
		// TODO
	}
}
