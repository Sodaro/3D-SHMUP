using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class EnemySensor : MonoBehaviour
{

    public GameObjectEvent _playerEnteredEvent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null)
            return;

        _playerEnteredEvent.Invoke(other.gameObject);

	}
}
