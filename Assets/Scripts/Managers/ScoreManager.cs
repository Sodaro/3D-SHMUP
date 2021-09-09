using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _text;

	private int _score = 0;
	//public void SetText(int value)
	//{
	//	_text.text = value.ToString(CultureInfo.InvariantCulture);
	//}

	private void Awake()
	{
		//_text = GetComponent<TextMeshProUGUI>();
	}

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
		
	}
}
