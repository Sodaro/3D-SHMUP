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

    // Update is called once per frame
    void Update()
    {
        Vector3 eulers = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, 0, eulers.z + Time.deltaTime * 100f);
    }

    public int PickUpCollectible()
	{
        Destroy(gameObject);
        return _points;
	}
}
