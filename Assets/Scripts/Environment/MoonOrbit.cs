using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 1f;

    private void Update()
    {
        transform.RotateAround(transform.parent.position, transform.up, _rotationSpeed * Time.deltaTime);
    }
}
 