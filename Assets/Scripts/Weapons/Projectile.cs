using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
	[SerializeField] private float _force = 100f;
	private Rigidbody _rigidbody;
	private Renderer _renderer;

	[SerializeField] private AudioClip _hitSound;
	[SerializeField] private SpawnableSound _spawnableSound;


	[SerializeField] private float _damage = 5;
	private float _damageModifier = 1f;

	public event EventHandler onProjectileDisabled;

	protected Coroutine _timedCoroutine;

	public bool IsActive => gameObject.activeInHierarchy;


	//Disable after seconds, to remove old floating bullets that are still somehow rendered by camera and thus not invisible
	protected IEnumerator DisableAfterSeconds(float time)
	{
		yield return new WaitForSeconds(time);
		DisableObject();
	}

	public virtual void ApplyColor(Color color)
	{
		_renderer.material.color = color;
	}

	public virtual void ApplyDamageModifier(float modifier)
	{
		_damageModifier = modifier;
	}
	public virtual void Fire(Vector3 direction, Vector3 startVelocity)
	{
		//Debug.Log($"{direction}");
		//_rigidbody.AddForce(direction * _force, ForceMode.Impulse);
		_rigidbody.velocity = startVelocity + (direction * _force);
		
	}

	//private void DestroyBullet(int points)
	//{

	//	Destroy(gameObject);
	//}

	protected virtual void DisableObject()
	{
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		if (_timedCoroutine != null)
			StopCoroutine(_timedCoroutine);
		gameObject.SetActive(false);

		
		//EventManager.RaiseOnBulletDisabled(this);
	}

	protected virtual void OnBecameInvisible()
	{
		DisableObject();
	}

	protected void Awake()
	{
		_renderer = GetComponentInChildren<Renderer>();
		_rigidbody = GetComponent<Rigidbody>();
	}


	protected virtual void OnTriggerEnter(Collider other)
	{
		IHealth health = other.GetComponent<IHealth>();
		if (health == null)
			health = other.GetComponentInParent<IHealth>();

		health?.TakeDamage(_damage * _damageModifier);
		SpawnableSound sound = Instantiate(_spawnableSound.gameObject).GetComponent<SpawnableSound>();
		sound.PlayAndDestroy(_hitSound);
		DisableObject();
	}
}
