using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Sensor : MonoBehaviour
{

    public GameObjectEvent _playerEnteredEvent;
	private void OnTriggerEnter(Collider other)
	{
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player == null)
            return;

        
        _playerEnteredEvent.Invoke(other.gameObject);

	}
}
