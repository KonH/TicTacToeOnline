using System;
using GameLogics;
using Xunit;

namespace GameLogicsTests {
	public class GameStateInit {

		[Fact]
		public void ValidInit() {
			var obj = new GameState(3, "X", "O");
			Assert.NotNull(obj);
		}

		[Fact]
		public void CellsInitialized() {
			var obj = new GameState(3, "X", "O");
			for ( var x = 0; x < obj.Field.Size; x++ ) {
				for ( var y = 0; y < obj.Field.Size; y++ ) {
					Assert.NotNull(obj.Field.GetCellAt(x, y));
				}
			}
		}

		[Fact]
		public void NoPlayers() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(4);
			});
		}

		[Fact]
		public void NoEnoughPlayers() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(4, "X");
			});
		}

		[Fact]
		public void InvalidPlayers() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(4, null, null);
			});
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(4, "X", "");
			});
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(4, "X", null);
			});
			Assert.Throws<ArgumentException>(() => {
				string[] nullArray = null;
				var obj = new GameState(4, nullArray);
			});
		}

		[Fact]
		public void DuplicatePlayers() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(4, "X", "X");
			});
		}

		[Fact]
		public void NoSize() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(0, "X", "O");
			});
		}

		[Fact]
		public void InvalidSize() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(-3, "X", "O");
			});
		}
	}
}
