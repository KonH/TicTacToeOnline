using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CellView : MonoBehaviour {
	public Text Text;

	public void Init(int x, int y, Action<int, int> clickCallback) {
		GetComponent<Button>().onClick.AddListener(() => clickCallback(x, y));
	}

	public void UpdateText(string text) {
		Text.text = text;
	}
}
