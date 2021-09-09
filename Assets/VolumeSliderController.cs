using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

	Vector2 _moveAxis = Vector2.zero;


	protected override void Awake()
	{
		_slider.onValueChanged.AddListener(SetValueOnInputField);
		_slider.onValueChanged.AddListener(delegate { _audioManager.SetVolume(_audioGroup, _slider.value); });
		_inputField.onSubmit.AddListener(UpdateSliderPosition);

		_previewAudioSource = GetComponent<AudioSource>();

		if (_previewAudioSource == null)
			return;

		/*
			* Assigning eventtriggers runtime solution with help from this thread:
			* https://forum.unity.com/threads/assigning-functions-to-event-triggers-at-runtime.263468/ 
		*/

		EventTrigger trigger = _slider.gameObject.AddComponent<EventTrigger>();

		EventTrigger.Entry downEntry = new EventTrigger.Entry();
		downEntry.eventID = EventTriggerType.PointerDown;
		downEntry.callback = new EventTrigger.TriggerEvent();
		downEntry.callback.AddListener(OnDownCallback);
		trigger.triggers.Add(downEntry);

		EventTrigger.Entry upEntry = new EventTrigger.Entry();
		upEntry.eventID = EventTriggerType.PointerUp;
		upEntry.callback = new EventTrigger.TriggerEvent();
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

	public void OnNavigate(InputAction.CallbackContext context)
	{
		_moveAxis = context.ReadValue<Vector2>();
		//Debug.Log("onnavigate");
		
	}


	private void Update()
	{
		//if (!_isSelected)
		//	return;

		_slider.value += _moveAxis.x * Time.deltaTime;
		//float horizontal = Input.GetAxisRaw("Horizontal");
		//if (horizontal != 0)
		//{
		//	_slider.value += horizontal * Time.deltaTime;
		//}
	}
}
