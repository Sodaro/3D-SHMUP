using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    Transform _currentWaypoint;
    int _currentTargetIndex = 0;
    [SerializeField] private float _moveSpeed = 20f;
    [SerializeField] private float _rotationSpeed = 1f;
    private Vector3 _velocity = Vector3.zero;

	private void Awake()
	{
        EventManager.onTutorialFinish += StartMoving;
	}

    void StartMoving()
	{
        _currentWaypoint = _waypoints[_currentTargetIndex];
    }

    public Vector3 Velocity => _velocity;

    // Update is called once per frame
    void Update()
    {
        if (_currentWaypoint == null)
            return;

        if (Vector3.Distance(transform.position, _currentWaypoint.position) < 2f)
		{
            if (_currentTargetIndex < _waypoints.Length - 1)
			{
                _currentTargetIndex++;
                _currentWaypoint = _waypoints[_currentTargetIndex];
            }      
            else
                return;
		}
        Vector3 direction = (_currentWaypoint.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        _velocity = transform.forward * _moveSpeed;
        transform.position += _velocity * Time.deltaTime;
    }

}
