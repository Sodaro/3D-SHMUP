using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int _points;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PickUpCollectible()
	{
        EventManager.RaiseOnPointsAdded(_points);
        //fail safe incase fired multiple times
        _points = 0;
        Destroy(gameObject);
	}
}
