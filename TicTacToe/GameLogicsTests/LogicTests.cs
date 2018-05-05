using System;
using GameLogics;
using Xunit;

namespace GameLogicsTests {
	public class LogicTests {

		[Fact]
		public void ThrowOnInvaldIntent() {
			Assert.False(Logics.IsIntentValid(null, null));
			Assert.Throws<ArgumentException>(() => Logics.ExecuteIntent(null, null));
		}

		[Fact]
		public void InvalidState() {
			Assert.False(Logics.IsIntentValid(null, new Intent("X", 0, 0)));
		}

		[Fact]
		public void InvalidIntent() {
			Assert.False(Logics.IsIntentValid(new GameState(3, "X", "O"), null));
		}

		[Fact]
		public void WrongPlayer() {
			// 1) "X"
			// 2) "O"
			Assert.False(
				Logics.IsIntentValid(
					new GameState(3, "X", "O"),
					new Intent("O", 0, 0)
				)
			);
		}

		[Fact]
		public void PlayersCycle() {
			// 1) "X"
			// 2) "O"
			var state = new GameState(3, "X", "O");
			state = Logics.ExecuteIntent(state, new Intent("X", 0, 0));
			state = Logics.ExecuteIntent(state, new Intent("O", 1, 1));
			state = Logics.ExecuteIntent(state, new Intent("X", 2, 2));
		}

		[Fact]
		public void TurnChanged() {
			var state = new GameState(3, "X", "O");
			var newState = Logics.ExecuteIntent(state, new Intent("X", 0, 0));
			Assert.True(newState.Turn > state.Turn);
		}

		[Fact]
		public void CellOwnerChanged() {
			var state = new GameState(3, "X", "O");
			Assert.True(state.Field.GetCellAt(0, 0).Owner != "X");
			var newState = Logics.ExecuteIntent(state, new Intent("X", 0, 0));
			Assert.True(newState.Field.GetCellAt(0, 0).Owner == "X");
		}

		[Fact]
		public void CellOwnerNotOverrided() {
			var state = new GameState(3, "X", "O");
			var newState = Logics.ExecuteIntent(state, new Intent("X", 0, 0));
			Assert.True(newState.Field.GetCellAt(0, 0).Owner == "X");
			Assert.False(Logics.IsIntentValid(newState, new Intent("O", 0, 0)));
		}
	}
}
