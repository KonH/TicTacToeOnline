using System;
using GameLogics;

namespace ConsoleClient {
	public class GameController {
		GameState _state;

		public GameController(int size, params string[] players) {
			_state = new GameState(size, players);
		}

		public void Run() {
			while ( true ) {
				var result = Logics.TryGetResult(_state);
				if ( result != null ) {
					End(result);
					return;
				} else {
					Update();
				}
			}
		}

		void End(GameResult result) {
			var msg = result.IsWin ? $"Winner: '{result.Winner}'" : "Draw";
			Console.WriteLine(msg);
		}

		void DrawField() {
			var field = _state.Field;
			for ( var y = 0; y < field.Size; y++ ) {
				for ( var x = 0; x < field.Size; x++ ) {
					var owner = field.GetCellAt(x, y).Owner;
					Console.Write(string.IsNullOrEmpty(owner) ? "_" : owner);
					Console.Write(" ");
				}
				Console.WriteLine();
			}
		}

		void Update() {
			DrawField();
			var turnOwner = _state.GetTurnOwner();
			Console.WriteLine();
			Console.WriteLine($"Turn: '{turnOwner}'");
			while ( true ) {
				Console.WriteLine("y:");
				var y = ReadInt() - 1;
				Console.WriteLine("x:");
				var x = ReadInt() - 1;
				try {
					var intent = new Intent(turnOwner, x, y);
					if ( Logics.IsIntentValid(_state, intent) ) {
						_state = Logics.ExecuteIntent(_state, intent);
						return;
					}
				} catch {}
			}
		}

		int ReadInt() {
			while ( true ) {
				var str = Console.ReadLine();
				if ( int.TryParse(str, out var value) ) {
					return value;
				}
			}
		}
	}
}
