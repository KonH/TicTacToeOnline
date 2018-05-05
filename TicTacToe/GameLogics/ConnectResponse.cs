using System;
namespace GameLogics {
	public class ConnectResponse {
		public string PlayerName   { get; }
		public string PlayerSecret { get; }
		public bool   Connected    { get; }

		public ConnectResponse(string playerName, string playerSecret, bool connected) {
			PlayerName   = playerName;
			PlayerSecret = playerSecret;
			Connected    = connected;
		}
	}
}
