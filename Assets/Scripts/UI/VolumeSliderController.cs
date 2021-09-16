using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VolumeSliderController : Selectable
{

	[SerializeField] private Slider _slider;
	[SerializeField] private TMP_InputField _inputField;

	[Space]
	[Header("-------Audio Manager-------")]

	[SerializeField] private AudioGroup _audioGroup;
	[SerializeField] private AudioManager _audioManager;

	[Space]
	[Header("-------Optional-------")]

	[SerializeField]
	[Tooltip("The source for the preview sound to be played when slider is held.")]
	private AudioSource _previewAudioSource;

	private bool _isSelected = false;

	private float _valueChangeAmount = 0;


	protected override void Awake()
	{
		string key = _audioManager.GetMixerGroupKey(_audioGroup);
		if (PlayerPrefs.HasKey(key))
		{
			float storedValue = PlayerPrefs.GetFloat(key);
			_slider.value = MyUtilities.AudioConverter.ConvertDBToFloat(PlayerPrefs.GetFloat(key));
			SetValueOnInputField(_slider.value);
		}

		_slider.onValueChanged.AddListener(SetValueOnInputField);
		_slider.onValueChanged.AddListener(delegate { _audioManager.SetVolumeLinear(_audioGroup, _slider.value); });
		_inputField.onSubmit.AddListener(UpdateSliderPosition);

		_previewAudioSource = GetComponent<AudioSource>();

		if (_previewAudioSource == null)
			return;

		/*
			* Assigning eventtriggers runtime solution with help from this thread:
			* https://forum.unity.com/threads/assigning-functions-to-event-triggers-at-runtime.263468/ 
		*/

		EventTrigger trigger = _slider.gameObject.AddComponent<EventTrigger>();

		EventTrigger.Entry downEntry = new EventTrigger.Entry
		{
			eventID = EventTriggerType.PointerDown,
			callback = new EventTrigger.TriggerEvent()
		};
		downEntry.callback.AddListener(OnDownCallback);
		trigger.triggers.Add(downEntry);

		EventTrigger.Entry upEntry = new EventTrigger.Entry
		{
			eventID = EventTriggerType.PointerUp,
			callback = new EventTrigger.TriggerEvent()
		};
		upEntry.callback.AddListener(OnUpCallback);
		trigger.triggers.Add(upEntry);

		_slider.onValueChanged.AddListener(delegate { PlayWithoutInterrupt(); });
	}

	public void PlayWithoutInterrupt()
	{
		if (_previewAudioSource.isPlaying)
			return;

		_previewAudioSource.PlayOneShot(_previewAudioSource.clip);
	}


	private void OnDownCallback(BaseEventData baseEventData)
	{
		_previewAudioSource.Play();
	}

	private void OnUpCallback(BaseEventData baseEventData)
	{
		_previewAudioSource.Stop();
	}
	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		_isSelected = true;
	}

	public override void OnDeselect(BaseEventData eventData)
	{
		base.OnDeselect(eventData);
		_isSelected = false;
		_valueChangeAmount = 0;
	}

	public void ChangeSliderValue(InputAction.CallbackContext context)
	{
		if (_isSelected)
			_valueChangeAmount = context.ReadValue<Vector2>().x;
		//_slider.value += amount * Time.deltaTime;
	}



	void UpdateSliderPosition(string value)
	{
		//value 0-100, fires only on submit pressed
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
		_slider.value = val;
	}

	void SetValueOnInputField(float value)
	{
		//value range : 0.0001 - 1, fires when slider is dragged
		value *= 100;
		_inputField.text = ((int)value).ToString();
	}


	private void Update()
	{
		if (!_isSelected)
			return;

		_slider.value += _valueChangeAmount * Time.deltaTime;
	}
}
