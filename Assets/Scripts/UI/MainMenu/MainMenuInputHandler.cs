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

    private PlayerInputActions _inputActions;
    
    private void Awake()
	{
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);

        _inputActions = new PlayerInputActions();
    }

	private void OnEnable()
	{
        _inputActions.UI.Navigate.started += _masterSlider.ChangeSliderValue;
        _inputActions.UI.Navigate.started += _musicSlider.ChangeSliderValue;
        _inputActions.UI.Navigate.started += _sfxSlider.ChangeSliderValue;

        _inputActions.UI.Navigate.canceled += _masterSlider.ChangeSliderValue;
        _inputActions.UI.Navigate.canceled += _musicSlider.ChangeSliderValue;
        _inputActions.UI.Navigate.canceled += _sfxSlider.ChangeSliderValue;

        _inputActions.UI.Navigate.Enable();
    }

	private void OnDisable()
	{
        _inputActions.UI.Navigate.started -= _masterSlider.ChangeSliderValue;
        _inputActions.UI.Navigate.started -= _musicSlider.ChangeSliderValue;
        _inputActions.UI.Navigate.started -= _sfxSlider.ChangeSliderValue;

        _inputActions.UI.Navigate.canceled -= _masterSlider.ChangeSliderValue;
        _inputActions.UI.Navigate.canceled -= _musicSlider.ChangeSliderValue;
        _inputActions.UI.Navigate.canceled -= _sfxSlider.ChangeSliderValue;

        _inputActions.UI.Navigate.Disable();
    }

	private void OnStartButtonClicked()
	{
        SceneHandler.Instance.LoadNextScene();
	}

    private void OnExitButtonClicked()
	{
        SceneHandler.Instance.ExitGame();
	}

    //change value of volume sliders when the button next to it receives horizontal input
 //   public void OnNavigate(InputAction.CallbackContext callback)
	//{
 //       Vector2 moveAxis = callback.ReadValue<Vector2>();

 //       if (_eventSystem.currentSelectedGameObject == _masterSlider.gameObject)
 //           _masterSlider.ChangeSliderValue(moveAxis);

 //       if (_eventSystem.currentSelectedGameObject == _musicSlider.gameObject)
 //           _musicSlider.ChangeSliderValue(moveAxis);

 //       if (_eventSystem.currentSelectedGameObject == _sfxSlider.gameObject)
 //           _sfxSlider.ChangeSliderValue(moveAxis);
        
	//}
}
