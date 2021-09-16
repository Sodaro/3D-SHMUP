using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
	[SerializeField] private float _force = 100f;
	private Rigidbody _rigidbody;
	private Renderer _renderer;

	[SerializeField] protected AudioClip _hitSound;
	[SerializeField] protected SpawnableSound _spawnableSound;


	[SerializeField] protected float _damage = 5;
	protected float _damageModifier = 1f;

	protected Coroutine _timedCoroutine = null;

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
		_rigidbody.velocity = startVelocity + (direction * _force);
	}

	protected virtual void DisableObject()
	{
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		if (_timedCoroutine != null)
			StopCoroutine(_timedCoroutine);
		gameObject.SetActive(false);
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

	protected virtual void OnCollisionEnter(Collision other)
	{
		SpawnableSound sound = Instantiate(_spawnableSound.gameObject, transform.position, Quaternion.identity).GetComponent<SpawnableSound>();
		sound.PlayAndDestroy(_hitSound);
		DisableObject();
	}


	protected virtual void OnTriggerEnter(Collider other)
	{
		IHealth health = other.GetComponent<IHealth>();
		if (health == null)
			health = other.GetComponentInParent<IHealth>();

		health?.TakeDamage(_damage * _damageModifier);
		SpawnableSound sound = Instantiate(_spawnableSound.gameObject, transform.position, Quaternion.identity).GetComponent<SpawnableSound>();
		sound.PlayAndDestroy(_hitSound);
		DisableObject();
	}
}
