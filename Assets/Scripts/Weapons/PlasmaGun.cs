using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : MonoBehaviour, IWeapon
{
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private float _roundsPerMinute = 60f;

	private float _timeBetweenShots;
	private float _timeOfLastShot;
	private Coroutine _shootingRoutine;
	private AudioSource _audio;

	private Transform _transform;

	private const float SECONDS_PER_MINUTE = 60f;

	public bool IsShooting {get; private set;} = false;

	public void StartShooting(Action<int> onHitCallBack)
	{
		if (IsShooting)
			return;

		//17:05:02 + 2f - 17:05:06
		_shootingRoutine = StartCoroutine(Shooting(Mathf.Max(0f, _timeOfLastShot + _timeBetweenShots - Time.time), onHitCallBack));
	}

	public void StopShooting()
	{
		StopCoroutine(_shootingRoutine);
		IsShooting = false;
	}

	private IEnumerator Shooting(float startDelay, Action<int> onHitCallBack)
	{
		IsShooting = true;
		if (startDelay > 0f)
		{
			yield return new WaitForSeconds(startDelay); //smart to use the delay rather than just a second or w/e
		}
		while(true)
		{
			FireShot(onHitCallBack);
			_timeOfLastShot = Time.time;
			yield return new WaitForSeconds(_timeBetweenShots);
		}

	}


	private void FireShot(Action<int> onHitCallBack)
	{
		Vector3 direction = transform.forward;
		Instantiate(_bulletPrefab, _transform.position, transform.rotation)?.GetComponent<Plasmabullet>()?.Fire(direction, onHitCallBack);
		_audio.PlayOneShot(_audio.clip);
	}

	private void Awake()
	{
		_transform = transform;
		_timeBetweenShots = SECONDS_PER_MINUTE / _roundsPerMinute;
		_audio = GetComponent<AudioSource>();
	}
}
