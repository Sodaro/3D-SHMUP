using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _text;

	private int _score = 0;

	private void ChangeScore(int amount)
	{
		_score += amount;
		_score = Mathf.Clamp(_score, 0, 9999);

		string number = _score.ToString();
		if (_score < 1000)
			number = "0" + number;
		if (_score < 100)
			number = "0" + number;
		if (_score < 10)
			number = "0" + number;


		_text.text = number;
	}


	private void OnEnable()
	{
		EventManager.onPointsAdded += ChangeScore;
	}
	private void OnDisable()
	{
		EventManager.onPointsAdded -= ChangeScore;
	}
}
