using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GameServer.Repositories;
using GameLogics;

namespace GameServer.Controllers {
	[Route("api/[controller]")]
	public class GameController : Controller {
		static List<string> _playerNames = new List<string> {
			"X",
			"O"
		};

		readonly ILogger        _logger;
		readonly GameRepository _repository;

		public GameController(ILoggerFactory loggerFactory, GameRepository repository) {
			_logger     = loggerFactory.CreateLogger<GameController>();
			_repository = repository;
		}

		string GenerateSecret() {
			return Guid.NewGuid().ToString();
		}

		[HttpPost("reset")]
		public void Reset() {
			_repository.State = null;
			_repository.Players.Clear();
		}

		[HttpPost("connect")]
		public ConnectResponse Connect() {
			_logger.LogDebug("Connect");
			var newIndex = _repository.Players.Count;
			if ( newIndex < _playerNames.Count ) {
				var playerName = _playerNames[newIndex];
				var playerSecret = GenerateSecret();
				_repository.Players.Add(new PlayerInfo(playerName, playerSecret));
				_logger.LogDebug($"New player: '{playerName}' = '{playerSecret}'");
				TryStart();
				return new ConnectResponse(playerName, playerSecret, true);
			}
			return new ConnectResponse("", "", false);
		}

		[HttpGet("state")]
		public GameState GetState() {
			_logger.LogDebug("GetState");
			return _repository.State;
		}

		[HttpPost("intent")]
		public IActionResult PostIntent([FromHeader]string playerSecret, [FromBody]Intent intent) {
			 _logger.LogDebug($"PostIntent: '{intent}'");
			var state = _repository.State;
			if ( !Logics.IsIntentValid(state, intent) ) {
				return BadRequest("Invalid intent");
			}
			if ( !IsPlayerSecretValid(intent.Player, playerSecret, out var error) ) {
				return BadRequest(error);
			}
			_repository.State = Logics.ExecuteIntent(state, intent);
			_logger.LogDebug("PostIndent: success");
			return Ok();
		}

		bool IsPlayerSecretValid(string playerName, string playerSecret, out string error) {
			if ( string.IsNullOrEmpty(playerSecret) ) {
				error = "No player secret header";
				return false;
			}
			var playerInfo = _repository.Players.Where(p => p.Name == playerName).FirstOrDefault();
			if ( playerInfo == null ) {
				error = "Unknown player";
				return false;
			}
			if ( playerSecret != playerInfo.Secret ) {
				error = "Invalid secred header";
				return false;
			}
			error = string.Empty;
			return true;
		}

		void TryStart() {
			if ( (_repository.State == null) && (_repository.Players.Count >= _playerNames.Count) ) {
				Start();
			}
		}

		void Start() {
			_logger.LogDebug("Start");
			_repository.State = new GameState(3, _repository.Players.Select(p => p.Name).ToArray());
		}
	}
}
