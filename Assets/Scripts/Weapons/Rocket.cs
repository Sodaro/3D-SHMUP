using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
	IEnumerator ExplodeAfterSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Explode();
		DisableObject();
	}
	protected override void DisableObject()
	{
		base.DisableObject();
		RocketPool.Instance.AddBackToPool(this);
	}

	private void Explode()
	{

	}

	public override void Fire(Vector3 direction, Vector3 startVelocity)
	{
		base.Fire(direction, startVelocity);
		_timedCoroutine = StartCoroutine(ExplodeAfterSeconds(5f));
	}
}
