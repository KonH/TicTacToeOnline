using UDBase.Controllers.LogSystem;
using Zenject;
using GameLogics;

public interface IGameController : IInitializable, ILogContext {
	int        FieldSize { get; }
	GameResult Result    { get; }
	string     TurnOwner { get; }

	void OnCellClick(int x, int y);
	void GoBackToMenu();
}
