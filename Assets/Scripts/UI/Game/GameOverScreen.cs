using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
	public void ExitToMainMenu()
	{
		SceneHandler.Instance.LoadMainMenu();
	}
	public void RestartLevel()
	{
		SceneHandler.Instance.RestartScene();
	}

	public void QuitGame()
	{
		SceneHandler.Instance.ExitGame();
	}
}
