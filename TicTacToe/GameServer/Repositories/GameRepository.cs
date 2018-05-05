using System.Collections.Generic;
using GameLogics;

namespace GameServer.Repositories {
	public class GameRepository {
		public GameState        State   { get; set; }
		public List<PlayerInfo> Players { get; set; } = new List<PlayerInfo>();
	}
}
