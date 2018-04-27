using UDBase.Controllers.LogSystem;
using Zenject;

public class GameController: IInitializable, ILogContext {
	readonly ILog _log;

	int _fieldSize = 3;
	int _players   = 2;

	public GameController(ILog log) {
		_log = log;
	}

	public void Initialize() {
		_log.MessageFormat(this, "Init: fieldSize: {0}, players: {1}", _fieldSize, _players);
	}
}
