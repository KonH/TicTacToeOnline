using System;
namespace GameLogics {
	public class ConnectResponse {
		public string PlayerName   { get; set; }
		public string PlayerSecret { get; set; }
		public bool   Connected    { get; set; }

		public ConnectResponse() { }			

		public ConnectResponse(string playerName, string playerSecret, bool connected) {
			PlayerName   = playerName;
			PlayerSecret = playerSecret;
			Connected    = connected;
		}
	}
}
