using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
	public void ResumeLevel()
	{
		EventManager.RaiseOnGamePause(false);
	}
	public void ExitToMainMenu()
	{
		Time.timeScale = 1;
		SceneHandler.Instance.LoadMainMenu();
	}
	public void RestartLevel()
	{
		Time.timeScale = 1;
		SceneHandler.Instance.RestartScene();
	}

	public void QuitGame()
	{
		SceneHandler.Instance.ExitGame();
	}
}
