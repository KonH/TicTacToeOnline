using System;

namespace GameLogics {
	public static class Logics {
		static bool IsCellClaimable(GameState state, int x, int y) {
			var field = state.Field;
			var cell = field.GetCellAt(x, y);
			return ( cell != null ) ?
				string.IsNullOrEmpty(cell.Owner) : false;
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

		static string CheckHorizontalWin(Field field) {
			for ( var x = 0; x < field.Size; x++ ) {
				var owner = field.GetCellAt(x, 0).Owner;
				for ( var y = 0; y < field.Size; y++ ) {
					if ( field.GetCellAt(x, y).Owner != owner ) {
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

		static string CheckVerticalWin(Field field) {
			for ( var y = 0; y < field.Size; y++ ) {
				var owner = field.GetCellAt(0, y).Owner;
				for ( var x = 0; x < field.Size; x++ ) {
					if ( field.GetCellAt(x, y).Owner != owner ) {
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

		static string CheckLeftDiagonalWin(Field field) {
			var owner = field.GetCellAt(0, 0).Owner;
			for ( var i = 1; i < field.Size; i++ ) {
				if ( field.GetCellAt(i, i).Owner != owner ) {
					return string.Empty;
				}
			}
			return owner;
		}

		static string CheckRightDiagonalWin(Field field) {
			var owner = field.GetCellAt(0, field.Size - 1).Owner;
			for (var i = 1; i < field.Size; i++) {
				if (field.GetCellAt(i, field.Size - 1 - i).Owner != owner) {
					return string.Empty;
				}
			}
			return owner;
		}

		static bool NoMoreUnclaimedCells(Field field) {
			for ( var x = 0; x < field.Size; x++ ) {
				for ( var y = 0; y < field.Size; y++ ) {
					if ( string.IsNullOrEmpty(field.GetCellAt(x, y).Owner) ) {
						return false;
					}
				}
			}
			return true;
		}

		static Func<Field, string>[] Cases = {
			CheckHorizontalWin,
			CheckVerticalWin,
			CheckLeftDiagonalWin,
			CheckRightDiagonalWin
		};

		static string TryGetWinner(Field field) {
			foreach ( var cs in Cases ) {
				var result = cs(field);
				if ( !string.IsNullOrEmpty(result) ) {
					return result;
				}
			}
			return string.Empty;
		}

		public static GameResult TryGetResult(GameState state) {
			var winner = TryGetWinner(state.Field);
			if ( !string.IsNullOrEmpty(winner) ) {
				return new GameResult(winner);
			}
			if ( NoMoreUnclaimedCells(state.Field) ) {
				return new GameResult(string.Empty);
			}
			return null;
		}
	}
}
