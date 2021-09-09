using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void OnPointsAdded(int amount);
    public static event OnPointsAdded onPointsAdded;

    public static void RaiseOnPointsAdded(int amount)
	{
        if (onPointsAdded != null)
            onPointsAdded(amount);
	}


    public delegate void OnBulletDisabled(PlasmaBullet bullet);
    public static event OnBulletDisabled onBulletDisabled;

    public static void RaiseOnBulletDisabled(PlasmaBullet bullet)
    {
        if (onBulletDisabled != null)
            onBulletDisabled(bullet);
    }


}
