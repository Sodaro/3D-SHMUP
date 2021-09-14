using MyUtilities;
using System.Collections;
using UnityEngine;

public class PlasmaGun : MonoBehaviour, IWeapon
{
	[SerializeField] private CameraMover _cameraMover;
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private float _roundsPerMinute = 60f;

	private float _timeBetweenShots;
	private float _timeOfLastShot;

	private float _attackRateModifier = 1f;
	private float _damageModifier = 1f;

	private Coroutine _shootingRoutine;
	private AudioSource _audio;

	private Transform _transform;

	private const float SECONDS_PER_MINUTE = 60f;

	private Color _color = Color.blue;

	public bool IsShooting {get; private set;} = false;

	public void StartShooting()
	{
		if (IsShooting)
			return;
		//17:05:02 + 4f - 17:05:06
		_shootingRoutine = StartCoroutine(Shooting(Mathf.Max(0f, _timeOfLastShot + (_timeBetweenShots) - Time.time)));
	}

	public void StopShooting()
	{
		StopCoroutine(_shootingRoutine);
		IsShooting = false;
	}

	private IEnumerator Shooting(float startDelay)
	{
		IsShooting = true;
		if (startDelay > 0f)
		{
			yield return new WaitForSeconds(startDelay);
		}
		while(true)
		{
			FireShot();
			_timeOfLastShot = Time.time;
			yield return new WaitForSeconds(_timeBetweenShots / _attackRateModifier);
		}
	}

	private void FireShot()
	{
		Projectile bullet = BulletPool.Instance.GetPooledObject();
		if (bullet == null)
			return;

		bullet.gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
		bullet.gameObject.SetActive(true);
		Vector3 direction = transform.forward;

		bullet?.Fire(direction, _cameraMover.Velocity);
		bullet?.ApplyDamageModifier(_damageModifier);
		bullet?.ApplyColor(_color);
		_audio.PlayOneShot(_audio.clip);
	}

	private void Awake()
	{
		_transform = transform;
		_timeBetweenShots = SECONDS_PER_MINUTE / _roundsPerMinute;
		_audio = GetComponent<AudioSource>();
	}

	public void ApplyModifier(float value, WeaponModifierType modifierType)
	{
		switch(modifierType)
		{
			case WeaponModifierType.AttackRate:
				_attackRateModifier = value;
				_color.g = 1;
				_color.b = 0;
				break;
			case WeaponModifierType.Damage:
				_damageModifier = value;
				_color.r = 1;
				_color.b = 0;
				break;
		}
	}

	public void RemoveModifier(WeaponModifierType modifierType)
	{
		switch(modifierType)
		{
			case WeaponModifierType.Damage:
				_damageModifier = 1f;
				_color.r = 0;
				break;
			case WeaponModifierType.AttackRate:
				_attackRateModifier = 1f;
				_color.g = 0;

				break;
		}
		if (_color == new Color(0,0,0,1))
			_color = Color.blue;
	}

}
