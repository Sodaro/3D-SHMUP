using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform[] targetPoints;
    Transform currentTargetPoint;
    int currentTargetIndex = 0;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 1f;
    private Vector3 _velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        currentTargetPoint = targetPoints[currentTargetIndex];
    }

    public Vector3 GetVelocity()
	{
        return _velocity;
	}

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, currentTargetPoint.position) < 2f)
		{
            if (currentTargetIndex < targetPoints.Length - 1)
			{
                currentTargetIndex++;
                currentTargetPoint = targetPoints[currentTargetIndex];
            }      
            else
                return;
		}
        Vector3 direction = (currentTargetPoint.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        _velocity = transform.forward * moveSpeed * Time.deltaTime;
        transform.position += _velocity;
    }
}
