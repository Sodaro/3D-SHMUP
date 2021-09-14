using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void OnPointsAdded(int amount);
    public static event OnPointsAdded onPointsAdded;

    //public delegate void OnProjectileDisabled(Projectile projectile);
    //public static event OnProjectileDisabled onProjectileDisabled;

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

    //public static void RaiseOnBulletDisabled(Projectile bullet)
    //{
    //    if (onProjectileDisabled != null)
    //        onProjectileDisabled(bullet);
    //}

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
