using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuInputHandler : MonoBehaviour
{
    [SerializeField] private VolumeSliderController _masterSlider;
    [SerializeField] private VolumeSliderController _musicSlider;
    [SerializeField] private VolumeSliderController _sfxSlider;

    
    [SerializeField] private EventSystem _eventSystem;


    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    


    private void Awake()
	{
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
	}

    private void OnStartButtonClicked()
	{
        SceneHandler.Instance.LoadNextScene();
	}

    private void OnExitButtonClicked()
	{
        SceneHandler.Instance.ExitGame();
	}


    public void OnNavigate(InputAction.CallbackContext callback)
	{
        Vector2 moveAxis = callback.ReadValue<Vector2>();

        if (_eventSystem.currentSelectedGameObject == _masterSlider.gameObject)
            _masterSlider.ChangeSliderValue(moveAxis);

        if (_eventSystem.currentSelectedGameObject == _musicSlider.gameObject)
            _musicSlider.ChangeSliderValue(moveAxis);

        if (_eventSystem.currentSelectedGameObject == _sfxSlider.gameObject)
            _sfxSlider.ChangeSliderValue(moveAxis);
        
        //Debug.Log($"navigate event");
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
