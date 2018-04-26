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

		public static bool IsIntentValid(GameState state, Intent intent) {
			if ( (state != null) && (intent != null) ) {
				return
					IsCellClaimable(state, intent.PosX, intent.PosY) &&
					IsPlayerActive(state, intent.Player);
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
	}
}
