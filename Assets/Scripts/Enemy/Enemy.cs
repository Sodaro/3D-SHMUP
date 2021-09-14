using MyUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour, IHealth, IWeapon
{
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private float _roundsPerMinute = 60f;

	private float _timeBetweenShots;
	private float _timeOfLastShot;

	//private float _attackRateModifier = 1f;
	//private float _damageModifier = 1f;

	private Coroutine _shootingRoutine;
	private AudioSource _audioSource;

	[SerializeField] private Transform _bulletSpawnTransform;

	private const float SECONDS_PER_MINUTE = 60f;


	[SerializeField] Sensor _sensor;

	[SerializeField] private int _points = 20;

	[SerializeField] private float _rotationSpeed = 30f;
	[SerializeField] private float _moveSpeed = 5f;

	[SerializeField] private float _maxHealth = 20;
	private float _currentHealth;

	private StateMachine _stateMachine;
	private GameObject _target;

	private Vector3 _velocity;

	private void Awake()
	{
		_currentHealth = _maxHealth;
		_stateMachine = new StateMachine();
		_stateMachine.Setup(new DormantState(this, _stateMachine));
		_sensor._playerEnteredEvent.AddListener(OnPlayerEnterSensor);
		_audioSource = GetComponent<AudioSource>();

		_timeBetweenShots = SECONDS_PER_MINUTE / _roundsPerMinute;
	}

	private void Update()
	{
		_stateMachine.CurrentState.Update();
		//if (_stateMachine.CurrentState is HuntingState)
		//{
		//	if (HasPassedPlayer)
		//		_stateMachine.ChangeState(new DormantState(this, _stateMachine));
		//}
	}

	void OnBecameInvisible()
	{
		_stateMachine.ChangeState(new DormantState(this, _stateMachine));
	}

	private void OnPlayerEnterSensor(GameObject player)
	{
		_target = player;
		if (_stateMachine.CurrentState is DormantState)
			_stateMachine.ChangeState(new HuntingState(this, _stateMachine));
	}

	public void MoveTowardsTarget()
	{
		_velocity = transform.forward * _moveSpeed;
		transform.position += _velocity * Time.deltaTime;
	}

	public bool HasAllyInFront
	{
		get
		{
			if (Physics.Raycast(_bulletSpawnTransform.position, transform.forward, 20f, gameObject.layer))
			{
				return true;
			}
			return false;
		}
	}

	//public bool HasPassedPlayer
	//{
	//	get
	//	{
	//		return GetComponent<Renderer>().isVisible;
	//	}
	//}

	public void RotateTowardsTarget()
	{
		//find the vector pointing from our position to the target
		Vector3 _direction = (_target.transform.position - transform.position).normalized;

		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(_direction);

		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _rotationSpeed);
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

	public void HealDamage(float amount)
	{
		_currentHealth += amount;
	}



	public bool IsShooting { get; private set; } = false;

	public void StartShooting()
	{
		if (IsShooting)
			return;
		//17:05:02 + 4f - 17:05:06
		_shootingRoutine = StartCoroutine(Shooting(Mathf.Max(0f, _timeOfLastShot + (_timeBetweenShots) - Time.time)));
	}

	public void StopShooting()
	{
		if (_shootingRoutine == null)
			return;
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
		while (true)
		{
			FireShot();
			_timeOfLastShot = Time.time;
			yield return new WaitForSeconds(_timeBetweenShots);
		}
	}

	private void FireShot()
	{
		Projectile bullet = BulletPool.Instance.GetPooledObject();
		if (bullet == null)
			return;

		bullet.gameObject.transform.SetPositionAndRotation(_bulletSpawnTransform.position, _bulletSpawnTransform.rotation);
		bullet.gameObject.SetActive(true);
		Vector3 direction = transform.forward;

		bullet?.Fire(direction, _velocity);
		//bullet?.ApplyDamageModifier(_damageModifier);
		bullet?.ApplyColor(Color.cyan);
		_audioSource.PlayOneShot(_audioSource.clip);
	}

	public void ApplyModifier(float value, WeaponModifierType modifierType)
	{
		throw new System.NotImplementedException();
	}

	public void RemoveModifier(WeaponModifierType modifierType)
	{
		throw new System.NotImplementedException();
	}
}
