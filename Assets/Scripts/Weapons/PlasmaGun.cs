using MyUtilities;
using System.Collections;
using UnityEngine;

public class PlasmaGun : WeaponBase
{
	protected override Projectile GetProjectileFromPool()
	{
		return ObjectPool<Bullet>.Instance.GetPooledObject();
	}
}
