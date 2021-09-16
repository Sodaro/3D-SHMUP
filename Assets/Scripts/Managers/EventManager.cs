using UnityEngine;

public class EventManager : MonoBehaviour
{
    //Made with help using https://www.gamedeveloper.com/business/how-to-use-c-events-in-unity


    /// <summary>
    /// This class declares several events and delegates, and functions for raising these events.
    /// Use case: The scorehandler subscribes to onPointsAdded event, when an enemy is destroyed it calls RaiseOnPointsAdded(points),
    /// which the scorehandler then adds to the UI without being coupled to the enemy.
    /// </summary>

    public delegate void OnPointsAdded(int amount);
    public static event OnPointsAdded onPointsAdded;

    public delegate void OnGameOver();
    public static event OnGameOver onGameOver;

    public delegate void OnGamePause(bool value);
    public static event OnGamePause onGamePause;

    public delegate void OnTutorialFinish();
    public static event OnTutorialFinish onTutorialFinish;

    public static void RaiseOnPointsAdded(int amount)
	{
        if (onPointsAdded != null)
            onPointsAdded(amount);
	}

    public static void RaiseOnGameOver()
	{
        if (onGameOver != null)
            onGameOver();
	}

    public static void RaiseOnGamePause(bool isPaused)
    {
        if (onGamePause != null)
            onGamePause(isPaused);
    }
    
    public static void RaiseOnTutorialFinish()
	{
        if (onTutorialFinish != null)
            onTutorialFinish();
	}
}
