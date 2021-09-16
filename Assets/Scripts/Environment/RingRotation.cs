using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    void Update()
    {
        //Vector3 eulerAngles = transform.rotation.eulerAngles;
        //eulerAngles.z = Mathf.Sin(Time.time) * 180;
        transform.localRotation = Quaternion.Euler(0,0, Mathf.Sin(Time.time) * 180);
    }
}
