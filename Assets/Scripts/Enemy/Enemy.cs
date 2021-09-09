using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour, IHealth
{
	[SerializeField] EnemySensor _sensor;

	[SerializeField] private int _points = 20;

	[SerializeField] private float _rotationSpeed = 30f;
	[SerializeField] private float _moveSpeed = 5f;

	[SerializeField] private float _maxHealth = 20;
	private float _currentHealth;

	private StateMachine _stateMachine;
	private GameObject _target;

	private void Awake()
	{
		_currentHealth = _maxHealth;
		_stateMachine = new StateMachine();
		_stateMachine.Setup(new DormantState(this, _stateMachine));
		_sensor._playerEnteredEvent.AddListener(OnPlayerEnterSensor);
	}

	private void Update()
	{
		_stateMachine.CurrentState.Update();
	}

	private void OnPlayerEnterSensor(GameObject player)
	{
		_target = player;
		_stateMachine.ChangeState(new HuntingState(this, _stateMachine));
	}

	public void MoveTowardsTarget()
	{
		transform.position += transform.forward * _moveSpeed * Time.deltaTime;
	}

	public void RotateTowardsTarget()
	{

		//answer by asafsitner on https://answers.unity.com/questions/254130/how-do-i-rotate-an-object-towards-a-vector3-point.html

		//find the vector pointing from our position to the target
		Vector3 _direction = (_target.transform.position - transform.position).normalized;

		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(_direction);

		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _rotationSpeed);
	}

	/// Return amount of points
	public int Hit()
	{
		//Destroy(gameObject);
		return _points;
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
}
