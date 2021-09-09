using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MusicPlayer : MonoBehaviour
{
	AudioSource _audioSource;
	//[SerializeField] private GameObject _sliderObject;
	//[SerializeField] private Slider _slider;
	//[SerializeField] private TMP_InputField _inputField;

	private void Awake()
	{
		//_audioSource = GetComponent<AudioSource>();
		//_inputField.onSubmit.AddListener(SetVolumeFromInputField);
		//_slider = _sliderObject.GetComponent<Slider>();
	}

	public void SetVolumeFromInputField(string value)
	{
		float val = float.Parse(value);
		if (val < 0)
		{
			val = 0;
		}
		if (val > 100)
		{
			val = 100;
		}

		val /= 100f;
		SetVolume(val);
	}

	public void SetVolume(float value)
	{
		//_inputField.text = string.Format("{0:0}", value*100);

		value = Mathf.Clamp01(value);
		//_audioSource.volume = value;
		//_slider.value = value;
	}
}
