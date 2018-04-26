using System;
using GameLogics;
using NUnit.Framework;

namespace GameLogicsTests {
	[TestFixture]
	public class GameStateInit {

		[Test]
		public void ValidInit() {
			var obj = new GameState(3, "X", "O");
			Assert.NotNull(obj);
		}

		[Test]
		public void CellsInitialized() {
			var obj = new GameState(3, "X", "O");
			for ( var x = 0; x < obj.Field.Size; x++ ) {
				for ( var y = 0; y < obj.Field.Size; y++ ) {
					Assert.NotNull(obj.Field.GetCellAt(x, y));
				}
			}
		}

		[Test]
		public void NoPlayers() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(4);
			});
		}

		[Test]
		public void NoEnoughPlayers() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(4, "X");
			});
		}

		[Test]
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

		[Test]
		public void DuplicatePlayers() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(4, "X", "X");
			});
		}

		[Test]
		public void NoSize() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(0, "X", "O");
			});
		}

		[Test]
		public void InvalidSize() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new GameState(-3, "X", "O");
			});
		}
	}
}
