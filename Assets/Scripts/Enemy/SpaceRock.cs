using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRock : MonoBehaviour, IHealth
{
	
	float _currentHealth = 10f;
	[SerializeField] private int _points = 10;
	public void HealDamage(float amount)
	{
		_currentHealth += amount;
	}

	public void TakeDamage(float amount)
	{
		_currentHealth -= amount;
		if (_currentHealth <= 0)
		{
			EventManager.RaiseOnPointsAdded(_points);
			Destroy(gameObject);
		}
	}
}
