using UnityEngine;

public class WinScreen : MonoBehaviour
{
	public void LoadMainMenu()
	{
		SceneHandler.Instance.LoadMainMenu();
	}
	public void ReplayLevel()
	{
		SceneHandler.Instance.LoadScene(BuildScene.Level1);
	}
	public void ExitGame()
	{
		SceneHandler.Instance.ExitGame();
	}
}
