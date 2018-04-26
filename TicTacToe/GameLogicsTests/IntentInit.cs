using System;
using GameLogics;
using NUnit.Framework;

namespace GameLogicsTests {
	[TestFixture]
	public class IntentInit {

		[Test]
		public void ValidInit() {
			var obj = new Intent("X", 1, 1);
			Assert.NotNull(obj);
		}

		[Test]
		public void InvalidUser() {
			Assert.Throws<ArgumentException>(() => {
				var obj = new Intent("", 1, 1);
			});
			Assert.Throws<ArgumentException>(() => {
				var obj = new Intent(null, 1, 1);
			});
		}

		[Test]
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
