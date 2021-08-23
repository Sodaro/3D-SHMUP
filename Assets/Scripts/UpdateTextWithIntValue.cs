using System.Globalization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateTextWithIntValue : MonoBehaviour
{
    private TextMeshProUGUI _text;

	public void SetText(int value)
	{
		_text.text = value.ToString(CultureInfo.InvariantCulture);
	}

    private void Awake()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}
}
