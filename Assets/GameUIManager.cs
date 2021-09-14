using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _pausePanel;

	private void Awake()
	{
		EventManager.onGamePause += OnPause;
		EventManager.onGameOver += OnGameOver;
	}

	private void OnGameOver()
	{
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
	}

	private void OnPause(bool pause)
	{
		if (pause)
		{
			Time.timeScale = 0;
			_pausePanel.SetActive(true);
		}
		else
		{
			Time.timeScale = 1;
			_pausePanel.SetActive(false);
		}
	}
}
