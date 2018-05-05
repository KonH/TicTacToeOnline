using System;
using GameLogics;

namespace ConsoleClient {
	public abstract class BaseGameController : IGameController {
		protected GameState _state;

		public abstract void Run();

		protected void DrawField() {
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

		protected void DrawTurnInfo() {
			var turnOwner = _state.GetTurnOwner();
			Console.WriteLine();
			Console.WriteLine($"Turn: '{turnOwner}'");
		}

		protected Intent AskForIntent() {
			Console.WriteLine("y:");
			var y = ReadInt() - 1;
			Console.WriteLine("x:");
			var x = ReadInt() - 1;
			try {
				var intent = new Intent(_state.GetTurnOwner(), x, y);
				if ( Logics.IsIntentValid(_state, intent) ) {
					return intent;
				}
			} catch (Exception e) {
				Console.WriteLine($"AskForIntent: Exception: {e}");
			}
			return null;
		}

		protected void End(GameResult result) {
			var msg = result.IsWin ? $"Winner: '{result.Winner}'" : "Draw";
			Console.WriteLine(msg);
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
