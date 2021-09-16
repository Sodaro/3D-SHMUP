using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int _points;
    public void PickUpCollectible()
	{
        EventManager.RaiseOnPointsAdded(_points);

        //fail safe incase fired multiple times
        _points = 0;
        Destroy(gameObject);
	}
}
