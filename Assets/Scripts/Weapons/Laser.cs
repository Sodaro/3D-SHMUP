using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IWeapon
{
	public bool IsShooting => throw new NotImplementedException();

	public void StartShooting(Action<int> onHitCallBack)
	{
		Debug.Log("Laser goes pew");
	}

	public void StopShooting()
	{
		Debug.Log("Laser goes errr");
	}
}
