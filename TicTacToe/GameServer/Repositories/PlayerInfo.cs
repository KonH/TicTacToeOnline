using System;

namespace GameServer.Repositories {
	public class PlayerInfo {
		public string Name   { get; }
		public string Secret { get; }

		public PlayerInfo(string name, string secret) {
			Name   = name;
			Secret = secret;
		}
	}
}
