using System;

namespace GameLogics {
	public static class Logics {
		static bool IsCellClaimable(GameState state, int x, int y) {
			var cells = state.Field.Cells;
			if ( (x >= 0) && (y >= 0) && (cells.GetLength(0) > x) && (cells.GetLength(1) > y) ) {
				return string.IsNullOrEmpty(cells[x, y].Owner); 
			}
			return false;
		}

		static bool IsPlayerActive(GameState state, string playerName) {
			return state.GetTurnOwner() == playerName;
		}

		static bool IsGameEnded(GameState state) {
			var result = TryGetResult(state);
			return (result != null);
		}

		public static bool IsIntentValid(GameState state, Intent intent) {
			if ( (state != null) && (intent != null) ) {
				return
					IsCellClaimable(state, intent.PosX, intent.PosY) &&
					IsPlayerActive(state, intent.Player) &&
					!IsGameEnded(state);
			}
			return false;
		}

		public static GameState ExecuteIntent(GameState state, Intent intent) {
			Guard.Success(IsIntentValid(state, intent));

			var newField = state.Field.ChangeOwner(intent.PosX, intent.PosY, intent.Player);
			var newTurn = state.Turn + 1;
			var newState = new GameState(newField, state.Players, newTurn);

			return newState;
		}

		// Is this can be generalized?
		// TODO: helpers in Field class, one-dim array usage

		static string CheckHorizontalWin(Cell[,] cells) {
			for ( var x = 0; x < cells.GetLength(1); x++ ) {
				var owner = cells[x, 0].Owner;
				for ( var y = 0; y < cells.GetLength(1); y++ ) {
					if ( cells[x, y].Owner != owner ) {
						owner = string.Empty;
						break;
					}
				}
				if ( !string.IsNullOrEmpty(owner) ) {
					return owner;
				}
			}
			return string.Empty;
		}

		static string CheckVerticalWin(Cell[,] cells) {
			for ( var y = 0; y < cells.GetLength(1); y++ ) {
				var owner = cells[0, y].Owner;
				for ( var x = 0; x < cells.GetLength(0); x++ ) {
					if ( cells[x, y].Owner != owner ) {
						owner = string.Empty;
						break;
					}
				}
				if ( !string.IsNullOrEmpty(owner) ) {
					return owner;
				}
			}
			return string.Empty;
		}

		static string CheckLeftDiagonalWin(Cell[,] cells) {
			var owner = cells[0, 0].Owner;
			for ( var i = 1; i < cells.GetLength(0); i++ ) {
				if ( cells[i, i].Owner != owner ) {
					return string.Empty;
				}
			}
			return owner;
		}

		static string CheckRightDiagonalWin(Cell[,] cells) {
			var owner = cells[0, cells.GetLength(0) - 1].Owner;
			for (var i = 1; i < cells.GetLength(0); i++) {
				if (cells[i, cells.GetLength(0) - 1 - i].Owner != owner) {
					return string.Empty;
				}
			}
			return owner;
		}

		static bool NoMoreUnclaimedCells(Cell[,] cells) {
			foreach ( var cell in cells ) {
				if ( string.IsNullOrEmpty(cell.Owner) ) {
					return false;
				}
			}
			return true;
		}

		static Func<Cell[,], string>[] Cases = {
			CheckHorizontalWin,
			CheckVerticalWin,
			CheckLeftDiagonalWin,
			CheckRightDiagonalWin
		};

		static string TryGetWinner(Cell[,] cells) {
			foreach ( var cs in Cases ) {
				var result = cs(cells);
				if ( !string.IsNullOrEmpty(result) ) {
					return result;
				}
			}
			return string.Empty;
		}

		public static GameResult TryGetResult(GameState state) {
			var winner = TryGetWinner(state.Field.Cells);
			if ( !string.IsNullOrEmpty(winner) ) {
				return new GameResult(winner);
			}
			if ( NoMoreUnclaimedCells(state.Field.Cells) ) {
				return new GameResult(string.Empty);
			}
			return null;
		}
	}
}
