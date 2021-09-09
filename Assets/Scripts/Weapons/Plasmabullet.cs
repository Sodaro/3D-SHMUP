using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlasmaBullet : MonoBehaviour
{
	[SerializeField] private float _force = 2f;
	private Rigidbody _rigidbody;
	private SpriteRenderer _spriteRenderer;

	[SerializeField] private float _damage = 5;
	private float _damageModifier = 1f;

	private Coroutine _coroutine;

	public bool IsActive => gameObject.activeInHierarchy;
	//Disable after seconds, to remove old floating bullets that are still somehow rendered by camera and thus not invisible
	IEnumerator DisableAfterSeconds(float time)
	{
		yield return new WaitForSeconds(time);
		DisableObject();
	}

	public void ApplyColor(Color color)
	{
		_spriteRenderer.material.color = color;
	}

	public void ApplyDamageModifier(float modifier)
	{
		_damageModifier = modifier;
	}
	public void Fire(Vector3 direction)
	{
		_rigidbody.AddForce(direction * _force, ForceMode.Impulse);
		_coroutine = StartCoroutine(DisableAfterSeconds(5f));
	}

	//private void DestroyBullet(int points)
	//{

	//	Destroy(gameObject);
	//}

	public void DisableObject()
	{
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		if (_coroutine != null)
			StopCoroutine(_coroutine);
		gameObject.SetActive(false);
		EventManager.RaiseOnBulletDisabled(this);
	}

	private void OnBecameInvisible()
	{
		DisableObject();
	}

	private void Awake()
	{
		_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		_rigidbody = GetComponent<Rigidbody>();
	}


	private void OnTriggerEnter(Collider other)
	{
		IHealth health = other.GetComponent<IHealth>();
		health?.TakeDamage(_damage * _damageModifier);
		DisableObject();
	}
}
