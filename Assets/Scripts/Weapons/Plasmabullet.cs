using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Plasmabullet : MonoBehaviour
{
	[SerializeField] private float _force = 2f;
	private Rigidbody _rigidbody;
	private Action<int> _onHitCallback;
	[SerializeField] private int _points = 5;

	public void Fire(Vector3 direction, Action<int> onHitCallBack)
	{
		_onHitCallback = onHitCallBack;
		_rigidbody.AddForce(direction * _force, ForceMode.Impulse);
	}

	//TODO: handle collision with enemies

	private void DestroyBullet(int points)
	{
		_onHitCallback.Invoke(_points);
		Destroy(gameObject);
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}


	private void OnCollisionEnter(Collision other)
	{
		Enemy enemy = other.gameObject.GetComponent<Enemy>();
		if (ReferenceEquals(enemy, null))
			DestroyBullet(0);
		else
			DestroyBullet(enemy.Hit());
	}
}
