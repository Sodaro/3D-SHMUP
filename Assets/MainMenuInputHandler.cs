using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuInputHandler : MonoBehaviour
{

    //TODO: FIX VOLUME SLIDERS TO RESPOND TO NEW INPUT
    [SerializeField] private VolumeSliderController _masterSlider;
    [SerializeField] private VolumeSliderController _musicSlider;
    [SerializeField] private VolumeSliderController _sfxSlider;

	private void Awake()
	{
		
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
