using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using GameLogics;
using Newtonsoft.Json;

namespace ConsoleClient {
	public class NetworkGameController : BaseGameController {
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

		public override void Run() {
			RunAsync().Wait();
		}

		async Task RunAsync() {
			var isConnected = await TryConnect();
			if ( isConnected ) {
				await UpdateAsync();
			}
		}

		async Task UpdateAsync() {
			while ( true ) {
				_state = await UpdateState();
				if ( _state == null ) {
					await Task.Delay(500);
					continue;
				}
				var result = Logics.TryGetResult(_state);
				if ( result != null ) {
					End(result);
					return;
				}
				if ( _state.GetTurnOwner() != _playerName ) {
					await Task.Delay(500);
					continue;
				}
				while ( true ) {
					DrawField();
					DrawTurnInfo();
					var intent = AskForIntent();
					if ( intent != null ) {
						await SendIntentAsync(intent);
						break;
					}
				}
			}
		}

		HttpRequestMessage CreateRequest(HttpMethod method, string endPoint) {
			var url = $"{_url}/{endPoint}";
			var req = new HttpRequestMessage(method, url);
			if ( !string.IsNullOrEmpty(_playerSecret) ) {
				req.Headers.Add("PlayerSecret", _playerSecret);
			}
			return req;
		}

		async Task<Response<T>> SendRequestAsync<T>(HttpMethod method, string endPoint) where T : class {
			var success = false;
			T   content = null;
			try {
				var req = CreateRequest(method, endPoint);
				var resp = await _client.SendAsync(req);
				if ( resp.IsSuccessStatusCode ) {
					var respText = await resp.Content.ReadAsStringAsync();
					content = JsonConvert.DeserializeObject<T>(respText);
					if ( content != null ) {
						success = true;
					}
				} else {
					Console.WriteLine($"SendRequestAsync: error: '{req.RequestUri}' => {resp.StatusCode}");
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

		async Task<GameState> UpdateState() {
			var resp = await SendRequestAsync<GameState>(HttpMethod.Get, "state");
			if ( resp.Success ) {
				return resp.Content;
			}
			return _state;
		}

		async Task SendIntentAsync(Intent intent) {
			var req = CreateRequest(HttpMethod.Post, "intent");
			req.Content = new StringContent(JsonConvert.SerializeObject(intent), Encoding.UTF8, "application/json");
			var resp = await _client.SendAsync(req);
			if ( !resp.IsSuccessStatusCode ) {
				Console.WriteLine($"SendIntentAsync: error: '{req.RequestUri}' => '{resp.StatusCode}'");
			}
		}
	}
}
