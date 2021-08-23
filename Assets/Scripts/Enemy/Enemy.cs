using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private int _points = 5;
	[SerializeField] private float _maxHealth = 20;
	private float _currentHealth;

	private void Awake()
	{
		_currentHealth = _maxHealth;
	}

	public void ReduceHealth(float amount)
	{
		_currentHealth -= amount;
		if (_currentHealth <= 0)
			Destroy(gameObject);
	}

	/// Return amount of points
	public int Hit()
	{
		//Destroy(gameObject);
		return _points;
	}
}
