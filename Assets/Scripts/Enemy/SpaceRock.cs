using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRock : MonoBehaviour, IHealth
{
	private AudioSource _audioSource;
	private float _currentHealth = 10f;
	[SerializeField] private int _points = 10;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void HealDamage(float amount)
	{
		_currentHealth += amount;
	}

	public void TakeDamage(float amount)
	{
		//already dead
		if (_currentHealth <= 0)
			return;

		_currentHealth -= amount;
		if (_currentHealth <= 0)
		{
			EventManager.RaiseOnPointsAdded(_points);
			_audioSource.Play();
			Destroy(gameObject, _audioSource.clip.length);
		}
	}
}
