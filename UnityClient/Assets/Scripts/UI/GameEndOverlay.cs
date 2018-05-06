using UnityEngine;
using UnityEngine.UI;
using Zenject;
using GameLogics;

public class GameEndOverlay : MonoBehaviour {
	public Text Text;

	[Inject]
	public void Init(IGameController game) {
		var result = game.Result;
		if ( result != null ) {
			Text.text = result.IsDraw ? "Draw!" : $"{result.Winner} is winner!";
		}
	}
}
