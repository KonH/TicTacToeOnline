using System.Collections.Generic;

namespace GameLogics {
	public sealed class GameState {
		public Field        Field   { get; }
		public List<Player> Players { get; }

		public GameState(int fieldSize, params string[] players) {
			Guard.NotNull(players);
			Guard.NonLess(players.Length, 2);
			Guard.NoDuplicates(players);

			Field   = new Field(fieldSize);
			Players = new List<Player>(players.Length);
			foreach ( var playerName in players ) {
				Players.Add(new Player(playerName));
			}
		}
	}
}
