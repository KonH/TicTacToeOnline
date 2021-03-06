﻿using System;
namespace GameLogics {
	public sealed class Player : IComparable<Player> {
		public string Name { get; set; }

		// Required for deserialization
		public Player() { }

		public Player(string name) {
			Guard.NotNullOrEmpty(name);
			Name = name;
		}

		public int CompareTo(Player other) {
			return string.CompareOrdinal(Name, other.Name);
		}
	}
}
