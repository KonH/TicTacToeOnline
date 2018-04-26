using System;
namespace GameLogics {
	public sealed class Player {
		public string Name { get; }

		public Player(string name) {
			Guard.NotNullOrEmpty(name);
			Name = name;
		}
	}
}
