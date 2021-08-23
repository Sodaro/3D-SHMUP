using Player;
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

	[SerializeField] private float _damage = 5;

	enum OwnerType { Enemy, Player};
	[SerializeField] OwnerType _owner;

	public void Fire(Vector3 direction, Action<int> onHitCallBack)
	{
		_onHitCallback = onHitCallBack;
		_rigidbody.AddForce(direction * _force, ForceMode.Impulse);
	}

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


	private void OnTriggerEnter(Collider other)
	{
		if (_owner == OwnerType.Player)
		{
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			if (ReferenceEquals(enemy, null))
			{
				DestroyBullet(0);
			}
			else
			{	
				enemy.ReduceHealth(_damage);
				DestroyBullet(enemy.Hit());
			}
				
		}
		else
		{
			PlayerController player = other.GetComponent<PlayerController>();
			if (ReferenceEquals(player, null))
			{
				Destroy(gameObject);
			}
			else
			{
				player.ReduceHealth(_damage);
				Destroy(gameObject);
			}
				
		}
	}
}
