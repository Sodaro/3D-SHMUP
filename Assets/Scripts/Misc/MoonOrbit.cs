using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _rotationSpeed = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.parent.position, transform.up, _rotationSpeed * Time.deltaTime);
    }
}
 