using System;
using System.Net.Http;
using System.Threading.Tasks;
using GameLogics;
using Newtonsoft.Json;

namespace ConsoleClient {
	public class NetworkGameController : IGameController {
		class Response<T> {
			public bool Success { get; }
			public T    Content { get; }

			public Response(bool success, T content) {
				Success = success;
				Content = content;
			}
		}

		readonly string     _url;
		readonly HttpClient _client;

		string _playerName;
		string _playerSecret;

		public NetworkGameController(string url) {
			_url    = url;
			_client = new HttpClient();
		}

		public void Run() {
			RunAsync().Wait();
		}

		async Task RunAsync() {
			var isConnected = await TryConnect();
			if ( isConnected ) {
				
			}
		}

		async Task<Response<T>> SendRequestAsync<T>(HttpMethod method, string endPoint) where T : class {
			var success = false;
			T   content = null;
			try {
				var url = $"{_url}/{endPoint}";
				var req = new HttpRequestMessage(HttpMethod.Post, url);
				if ( !string.IsNullOrEmpty(_playerSecret) ) {
					req.Headers.Add("PlayerSecret", _playerSecret);
				}
				var resp = await _client.SendAsync(req);
				if ( resp.IsSuccessStatusCode ) {
					var respText = await resp.Content.ReadAsStringAsync();
					content = JsonConvert.DeserializeObject<T>(respText);
					if ( content != null ) {
						success = true;
					}
				} else {
					Console.WriteLine($"SendRequestAsync: error: '{url}' => {resp.StatusCode}");
				}
			} catch ( Exception e) {
				Console.WriteLine($"SendRequestAsync: exception: {e}");
			}
			return new Response<T>(success, content);
		}

		async Task<bool> TryConnect() {
			var resp = await SendRequestAsync<ConnectResponse>(HttpMethod.Post, "connect");
			if ( resp.Success ) {
				var connectResp = resp.Content;
				if ( connectResp.Connected ) {
					if ( !string.IsNullOrEmpty(connectResp.PlayerName) && !string.IsNullOrEmpty(connectResp.PlayerSecret) ) {
						_playerName = connectResp.PlayerName;
						_playerSecret = connectResp.PlayerSecret;
						Console.WriteLine($"Connected to server as '{_playerName}'");
						return true;
					} else {
						Console.WriteLine("Invalid server response");
					}
				} else {
					Console.WriteLine("Server is busy");
				}
			}
			Console.WriteLine("Cannot connect to server");
			return false;
		}
	}
}
