using System.Globalization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateTextWithIntValue : MonoBehaviour
{
    private TextMeshProUGUI _text;

	public void SetText(int value)
	{
		string number = value.ToString();
		if (value < 1000)
			number = "0" + number;
		if (value < 100)
			number = "0" + number;
		if (value < 10)
			number = "0" + number;

		_text.text = number;
	}

    private void Awake()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}
}
