using System;
using GameLogics;

namespace ConsoleClient {
	public class LocalGameController : BaseGameController {
		public LocalGameController(int size, params string[] players) {
			_state = new GameState(size, players);
		}

		public override void Run() {
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

		void Update() {
			DrawField();
			DrawTurnInfo();
			while ( true ) {
				var intent = AskForIntent();
				if ( intent != null ) {
					_state = Logics.ExecuteIntent(_state, intent);
					return;
				}
			}
		}
	}
}
