using System;
using GameLogics;
using Xunit;

namespace GameLogicsTests {
	public class IntentInit {

		[Fact]
		public void ValidInit() {
			var obj = new Intent("X", 1, 1);
			Assert.NotNull(obj);
		}

		[Fact]
		public void InvalidUser() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new Intent("", 1, 1);
			});
			Assert.Throws<ArgumentException>(() => {
				var obj = new Intent(null, 1, 1);
			});
		}

		[Fact]
		public void InvalidPosition() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new Intent("X", -1, 1);
			});
			Assert.Throws<ArgumentException>(() => {
				var obj = new Intent("X", 1, -1);
			});
			Assert.Throws<ArgumentException>(() => {
				var obj = new Intent("X", -1, -1);
			});
		}
	}
}
