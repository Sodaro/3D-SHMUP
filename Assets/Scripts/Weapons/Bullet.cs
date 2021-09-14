using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
	public Bullet()
	{
	}

	protected override void DisableObject()
	{
		base.DisableObject();
		BulletPool.Instance.AddBackToPool(this);
	}

	public override void Fire(Vector3 direction, Vector3 startVelocity)
	{
		base.Fire(direction, startVelocity);
		_timedCoroutine = StartCoroutine(DisableAfterSeconds(5f));
	}
}
