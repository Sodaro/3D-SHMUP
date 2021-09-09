using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperClasses;

public interface IWeapon
{
	public void ApplyModifier(float value, Enums.WeaponModifierType modifierType);
	public void RemoveModifier(Enums.WeaponModifierType modifierType);
	public bool IsShooting { get; }
	//public void StartShooting(Action<int> onHitCallBack);
	public void StartShooting();
	public void StopShooting();
}
