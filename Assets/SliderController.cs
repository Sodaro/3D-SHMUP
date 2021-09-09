using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderController : Selectable
{
	[SerializeField] private Slider _slider;
	private bool _isSelected = false;

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


	private void Update()
	{
		if (!_isSelected)
			return;

		float horizontal = Input.GetAxisRaw("Horizontal");
		if (horizontal != 0)
		{
			_slider.value += horizontal * Time.deltaTime;
		}
	}
}
