using System.Collections;
using UnityEngine;

public class Rocket : Projectile
{
	[SerializeField] private float _secondsBeforeExplode = 3f;
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
		SpawnableSound sound = Instantiate(_spawnableSound.gameObject, transform.position, Quaternion.identity).GetComponent<SpawnableSound>();
		sound.PlayAndDestroy(_hitSound);

		int layerMask = LayerMask.GetMask("Default", "Enemy", "Player");
		Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, layerMask, QueryTriggerInteraction.Collide);
		foreach (Collider collider in colliders)
		{
			IHealth health = collider.GetComponent<IHealth>();
			if (health == null)
				health = collider.GetComponentInParent<IHealth>();

			health?.TakeDamage(_damage * _damageModifier);
		}
	}

	public override void Fire(Vector3 direction, Vector3 startVelocity)
	{
		base.Fire(direction, startVelocity);
		_timedCoroutine = StartCoroutine(ExplodeAfterSeconds(_secondsBeforeExplode));
	}

	protected override void OnCollisionEnter(Collision other)
	{
		Explode();
		DisableObject();
	}

	protected override void OnTriggerEnter(Collider other)
	{
		Explode();
		DisableObject();
	}
}
