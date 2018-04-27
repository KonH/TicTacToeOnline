using GameLogics;

public struct GameState_Updated {
	public GameState State { get; }

	public GameState_Updated(GameState state) {
		State = state;
	}
}
