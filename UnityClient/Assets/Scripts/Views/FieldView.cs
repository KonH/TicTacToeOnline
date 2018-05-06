using UnityEngine;
using UnityEngine.UI;
using UDBase.Controllers.LogSystem;
using UDBase.Controllers.EventSystem;
using Zenject;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(GridLayoutGroup))]
public class FieldView : MonoBehaviour, ILogContext {
	public CellView CellPrefab;

	RectTransform   _trans;
	GridLayoutGroup _grid;
	ILog            _log;
	IEvent          _event;
	IGameController _game;

	CellView[,] _cells;

	[Inject]
	public void Init(ILog log, IEvent events, IGameController game) {
		_trans = GetComponent<RectTransform>();
		_grid  = GetComponent<GridLayoutGroup>();
		_log   = log;
		_event = events;
		_game  = game;
	}

	void Start() {
		_event.Subscribe<GameState_Updated>(this, OnGameStateUpdated);
		InitGrid();
		InstantiateCells();
	}

	void OnDestroy() {
		_event.Unsubscribe<GameState_Updated>(OnGameStateUpdated);
	}

	void InitGrid() {
		var size = _game.FieldSize;
		_grid.constraintCount = size;
		var usableSize = _trans.sizeDelta.x - _grid.padding.left * 2;
		var cellSize = (usableSize - _grid.spacing.x * (size - 1)) / size;
		_grid.cellSize = Vector2.one * cellSize;
	}

	void InstantiateCells() {
		var size = _game.FieldSize;
		_cells = new CellView[size, size];
		for ( var y = 0; y < size; y++ ) {
			for ( var x = 0; x < size; x++ ) {
				var cell = Instantiate(CellPrefab, _trans);
				cell.Init(x, y, OnCellClick);
				_cells[x, y] = cell;
			}
		}
	}

	void OnCellClick(int x, int y) {
		_log.MessageFormat(this, "OnCellClick: ({0}, {1})", x, y);
		_game.OnCellClick(x, y);
	}

	void OnGameStateUpdated(GameState_Updated e) {
		var field = e.State.Field;
		for ( var x = 0; x < field.Size; x++ ) {
			for ( var y = 0; y < field.Size; y++ ) {
				_cells[x, y].UpdateText(field.GetCellAt(x, y).Owner);
			}
		}
	}
}
