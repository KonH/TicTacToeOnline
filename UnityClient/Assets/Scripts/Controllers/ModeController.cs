using UDBase.Controllers.LogSystem;

public enum GameMode {
	Local
}

public class ModeController : ILogContext {
	public GameMode Mode { get; private set; }

	ILog _log;

	public ModeController(ILog log) {
		_log = log;
	}

	public void SelectGameMode(GameMode mode) {
		_log.MessageFormat(this, "SelectGameMode: {0}", mode);
		Mode = mode;
	}
}
