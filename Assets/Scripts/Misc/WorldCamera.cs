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
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _cameraMover.Velocity / _scaleFactor * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, _cameraToMirror.transform.rotation, 1/_scaleFactor);
    }
}
