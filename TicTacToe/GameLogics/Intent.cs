using System;
namespace GameLogics {
	public sealed class Intent {
		public string Player { get; set; }
		public int    PosX   { get; set; }
		public int    PosY   { get; set; }

		public Intent() { }

		public Intent(string player, int posX, int posY) {
			Guard.NotNullOrEmpty(player);
			Guard.NonNegative(posX, posY);

			Player = player;
			PosX   = posX;
			PosY   = posY;
		}
	}
}
