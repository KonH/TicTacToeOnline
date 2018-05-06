using System.Collections.Generic;
using UDBase.Utils;
using UDBase.Controllers.LogSystem;
using UDBase.Controllers.EventSystem;
using GameLogics;
using Zenject;
using FullSerializer;

public class NetworkController : ILogContext, ITickable {
	readonly string _url = "http://127.0.0.1:8080/api/game";

	readonly ILog   _log;
	readonly IEvent _event;

	WebClient _client;

	Dictionary<string, string> _headers = new Dictionary<string, string>();

	string _playerName;
	string _playerSecret;

	public NetworkController(NetUtils netUtils, ILog log, IEvent events) {
		_log   = log;
		_event = events;

		_client = new WebClient(netUtils);
	}

	public void Tick() {
		// TODO
	}

	void Reset() {
		_playerName   = string.Empty;
		_playerSecret = string.Empty;
		_headers.Clear();
	}

	public void TryConnectToServer() {
		Reset();
		_client.SendPostRequest($"{_url}/connect", "", onComplete: OnConnectComplete);
	}

	void OnConnectComplete(NetUtils.Response response) {
		_log.MessageFormat(this, "OnConnectComplete: '{0}'", response.HasError ? response.Error : response.Text);
		if ( response.HasError ) {
			_event.Fire(new Network_ConnectComplete(false));
			return;
		}

		ConnectResponse resp = null;

		var fsData = fsJsonParser.Parse(response.Text);
		var serializer = new fsSerializer();
		var result = serializer.TryDeserialize<ConnectResponse>(fsData, ref resp);

		_log.MessageFormat(
			this,
			"Parsed response: connected: {0}, name: '{1}', secret: '{2}'",
			resp?.Connected, resp?.PlayerName, resp?.PlayerSecret
		);

		if ( result.Failed || (resp != null) && !resp.Connected ) {
			_event.Fire(new Network_ConnectComplete(false));
			return;
		}

		_playerName   = resp.PlayerName;
		_playerSecret = resp.PlayerSecret;

		_headers.Add("PlayerSecret", _playerSecret);

		_event.Fire(new Network_ConnectComplete(true));
	}

}
