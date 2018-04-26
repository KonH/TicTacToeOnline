using System;
namespace GameLogics {
	public sealed class GameResult {
		public string Winner { get; }

		public bool IsWin  => !string.IsNullOrEmpty(Winner);
		public bool IsDraw => string.IsNullOrEmpty(Winner);

		public GameResult(string winner) {
			Winner = winner;
		}
	}
}
