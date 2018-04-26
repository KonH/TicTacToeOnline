﻿using System.Collections.Generic;

namespace GameLogics {
	public sealed class GameState {
		public Field        Field   { get; }
		public List<Player> Players { get; }
		public int          Turn    { get; }

		internal GameState(Field field, List<Player> players, int turn) {
			Guard.NotNull(field);
			Guard.NotNull(players);
			Guard.NonLess(players.Count, 2);
			Guard.NoDuplicates(players);
			Guard.NonNegative(turn);

			Field   = field;
			Players = players;
			Turn    = turn;
		}

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

		public string GetTurnOwner() {
			return Players[Turn % Players.Count].Name;
		}
	}
}
