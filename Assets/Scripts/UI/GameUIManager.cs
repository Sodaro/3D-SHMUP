using UnityEngine;
using UnityEngine.EventSystems;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _pausePanel;

	[SerializeField] private GameObject _pauseButtonToSelect;
	[SerializeField] private GameObject _gameOverButtonToSelect;

	[SerializeField] private EventSystem _eventSystem;

	private void OnEnable()
	{
		EventManager.onGamePause += OnPause;
		EventManager.onGameOver  += OnGameOver;
	}

	private void OnDisable()
	{
		EventManager.onGamePause -= OnPause;
		EventManager.onGameOver  -= OnGameOver;
	}

	private void OnGameOver()
	{
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
		_eventSystem.SetSelectedGameObject(_gameOverButtonToSelect);
	}

	private void OnPause(bool pause)
	{
		if (pause)
		{
			Time.timeScale = 0;
			_pausePanel.SetActive(true);
			_eventSystem.SetSelectedGameObject(_pauseButtonToSelect);
		}
		else
		{
			Time.timeScale = 1;
			_pausePanel.SetActive(false);
		}
	}
}
