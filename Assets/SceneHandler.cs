using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BuildScene { MainMenu, Level1, Count}



public class SceneHandler : MonoBehaviour
{
    private static SceneHandler _instance;

    public static SceneHandler Instance => _instance;

	public void RestartScene()
	{
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
	}

    public void LoadMainMenu()
	{
        SceneManager.LoadScene(0);
	}

    public void LoadScene(BuildScene scene)
	{
        if (scene == BuildScene.Count)
            throw new System.Exception("INVALID SCENE PASSED TO SCENEHANDLER");

        SceneManager.LoadScene((int)scene);
	}

    public void LoadNextScene()
	{
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.buildIndex == (int)BuildScene.Count - 1)
            return;

        SceneManager.LoadScene(activeScene.buildIndex + 1);
    }

    public void ExitGame()
	{
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
			    Application.Quit();
        #endif
    }

    private void Awake()
    {
        //if (_instance != null && _instance != this)
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
