using GameLogics;
using Xunit;

namespace GameLogicsTests {
	public class EndTests {

		[Fact]
		public void WinSimple() {
			var state = new GameState(1, "X", "O");
			var newState = Logics.ExecuteIntent(state, new Intent("X", 0, 0));
			var result = Logics.TryGetResult(newState);
			Assert.NotNull(result);
			Assert.True(result.IsWin);
		}

		[Fact]
		public void CorrectWinner() {
			var state = new GameState(1, "X", "O");
			var newState = Logics.ExecuteIntent(state, new Intent("X", 0, 0));
			var result = Logics.TryGetResult(newState);
			Assert.True(result.Winner == "X");
		}

		// X O X
		// O O X
		// X X O
		Intent[] _drawIntents = new Intent[] {
			new Intent("X", 0, 0),
			new Intent("O", 0, 1),
			new Intent("X", 0, 2),
			new Intent("O", 1, 0),
			new Intent("X", 1, 2),
			new Intent("O", 1, 1),
			new Intent("X", 2, 0),
			new Intent("O", 2, 2),
			new Intent("X", 2, 1),
		};

		[Fact]
		public void Draw() {
			var state = new GameState(3, "X", "O");
			foreach ( var intent in _drawIntents ) {
				state = Logics.ExecuteIntent(state, intent);
			}
			var result = Logics.TryGetResult(state);
			Assert.NotNull(result);
			Assert.True(result.IsDraw);
		}

		[Fact]
		public void NoWinnerInDraw() {
			var state = new GameState(3, "X", "O");
			foreach (var intent in _drawIntents) {
				state = Logics.ExecuteIntent(state, intent);
			}
			var result = Logics.TryGetResult(state);
			Assert.True(string.IsNullOrEmpty(result.Winner));
		}

		GameState CheckWin(Intent[] intents, string winner) {
			var state = new GameState(3, "X", "O");
			foreach (var intent in intents) {
				state = Logics.ExecuteIntent(state, intent);
			}
			var result = Logics.TryGetResult(state);
			Assert.NotNull(result);
			Assert.True(result.IsWin);
			Assert.True(result.Winner == winner);
			return state;
		}

		// X X X
		// O O
		// 
		Intent[] _horizontalWin = new Intent[] {
			new Intent("X", 0, 0),
			new Intent("O", 1, 0),
			new Intent("X", 0, 1),
			new Intent("O", 1, 1),
			new Intent("X", 0, 2)
		};

		[Fact]
		public void HorizontalWin() {
			CheckWin(_horizontalWin, "X");
		}

		[Fact]
		public void NoIntentsAfterWin() {
			var state = CheckWin(_horizontalWin, "X");
			Assert.False(Logics.IsIntentValid(state, new Intent("O", 2, 0)));
		}

		// X O
		// X O
		// X
		Intent[] _verticalWin = new Intent[] {
			new Intent("X", 0, 0),
			new Intent("O", 0, 1),
			new Intent("X", 1, 0),
			new Intent("O", 0, 2),
			new Intent("X", 2, 0)
		};

		[Fact]
		public void VerticalWin() {
			CheckWin(_verticalWin, "X");
		}

		// X O
		// O X
		//     X
		Intent[] _diagonalWin1 = new Intent[] {
			new Intent("X", 0, 0),
			new Intent("O", 0, 1),
			new Intent("X", 1, 1),
			new Intent("O", 1, 0),
			new Intent("X", 2, 2)
		};

		[Fact]
		public void DiagonalWin() {
			CheckWin(_diagonalWin1, "X");
		}

		// O O X
		//   X
		// X
		Intent[] _diagonalWin2 = new Intent[] {
			new Intent("X", 0, 2),
			new Intent("O", 0, 0),
			new Intent("X", 1, 1),
			new Intent("O", 0, 1),
			new Intent("X", 2, 0)
		};

		[Fact]
		public void DiagonalWin2() {
			CheckWin(_diagonalWin2, "X");
		}
	}
}
