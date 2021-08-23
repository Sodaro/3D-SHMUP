using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
	public bool IsShooting { get; }
	public void StartShooting(Action<int> onHitCallBack);
	public void StopShooting();
}
