using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCamera : MonoBehaviour
{
    GameObject _cameraToMirror;
    CameraMover _cameraMover;
    [SerializeField] float _scaleFactor = 100;
	private void Awake()
	{
        _cameraToMirror = Camera.main.gameObject;
        _cameraMover = _cameraToMirror.GetComponent<CameraMover>();
	}
    private void Update()
    {
        transform.position += _cameraMover.Velocity / _scaleFactor * Time.deltaTime;
        //transform.rotation = Quaternion.Slerp(transform.rotation, _cameraToMirror.transform.rotation, 1/_scaleFactor);
        transform.rotation = _cameraToMirror.transform.rotation;
    }
}
