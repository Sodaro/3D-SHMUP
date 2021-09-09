using HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IWeapon
{
	public bool IsShooting => throw new NotImplementedException();

	public void ApplyModifier(float value, Enums.WeaponModifierType modifierType)
	{
		throw new NotImplementedException();
	}

	public void RemoveModifier(Enums.WeaponModifierType modifierType)
	{
		throw new NotImplementedException();
	}

	//public void StartShooting(Action<int> onHitCallBack)
	public void StartShooting()
	{
		Debug.Log("Laser goes pew");
	}

	public void StopShooting()
	{
		Debug.Log("Laser goes errr");
	}
}
