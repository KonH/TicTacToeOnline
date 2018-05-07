using GameLogics;

public struct Network_StateUpdated {
	public GameState State { get; }

	public Network_StateUpdated(GameState state) {
		State = state;
	}
}
