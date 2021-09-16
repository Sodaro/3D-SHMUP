using MyUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : WeaponBase
{
	protected override Projectile GetProjectileFromPool()
	{
		return ObjectPool<Rocket>.Instance.GetPooledObject();
	}
}
