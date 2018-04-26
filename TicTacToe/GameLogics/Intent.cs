using System;
namespace GameLogics {
	public sealed class Intent {
		public string Player { get; }
		public int    PosX   { get; }
		public int    PosY   { get; }

		public Intent(string player, int posX, int posY) {
			Guard.NotNullOrEmpty(player);
			Guard.NonNegative(posX, posY);

			Player = player;
			PosX   = posX;
			PosY   = posY;
		}
	}
}
