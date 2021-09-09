using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform upperHalf;
    [SerializeField] Transform lowerHalf;

    [SerializeField] private float _delay;
    [SerializeField] private float _duration;
    [SerializeField] private float _moveDistance;

    [SerializeField] private float _rotationDegrees;

    private MeshRenderer[] _renderers;

    [SerializeField] private bool _isOpen;

    public void OpenDoor()
	{
        if (!_isOpen)
            StartCoroutine(RotateAndOpen(2, 1));
    }

    public void CloseDoor()
	{
        if (_isOpen)
            StartCoroutine(CloseAndRotate(2, 1));
    }

	private void Awake()
	{
        _renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in _renderers)
            renderer.materials[1].EnableKeyword("_EMISSION");
	}

    void MoveHalvesToOpenPositions()
	{
        upperHalf.localRotation = Quaternion.Euler(0, 0, upperHalf.transform.localRotation.eulerAngles.z + _rotationDegrees);
        lowerHalf.localRotation = Quaternion.Euler(0, 0, lowerHalf.transform.localRotation.eulerAngles.z + _rotationDegrees);

        upperHalf.position = transform.position + upperHalf.up * _moveDistance;
        lowerHalf.position = transform.position + lowerHalf.up * _moveDistance;
    }
	// Start is called before the first frame update
	void Start()
    {
        if (_isOpen)
		{
            MoveHalvesToOpenPositions();
            foreach (var renderer in _renderers)
            {
                renderer.materials[1].SetColor("_EmissionColor", Color.green);
            }

        }
        //StartCoroutine(OpenDoor(_delay, _duration, _moveDistance));
    }

    float easeInQuart(float x)
    {
        return x* x * x* x;
    }
    float easeOutQuart(float x)
	{
        return 1 - ((1 - x) * (1 - x) * (1 - x) * (1 - x));
    }

    IEnumerator RotateAndOpen(float startDelay, float delayBetweenSequence)
	{

        if (startDelay > 0)
            yield return new WaitForSeconds(startDelay);

        yield return StartCoroutine(RotateZ(_duration, _rotationDegrees));
        yield return new WaitForSeconds(delayBetweenSequence);
        yield return StartCoroutine(OpenDoor(_duration, _moveDistance));
        _isOpen = true;
        
    }
    IEnumerator CloseAndRotate(float startDelay, float delayBetweenSequence)
	{

        if (startDelay > 0)
            yield return new WaitForSeconds(startDelay);

        yield return StartCoroutine(CloseDoor(_duration, _moveDistance));
        yield return new WaitForSeconds(delayBetweenSequence);
        yield return StartCoroutine(RotateZ(_duration, -(_rotationDegrees)));
        _isOpen = false;
    }

    IEnumerator RotateZ(float duration, float degrees)
	{
        float f = 0;
        
        Quaternion upperStartQuaternion = upperHalf.localRotation;
        Quaternion lowerStartQuaternion = lowerHalf.localRotation;
        Quaternion upperTargetQuaternion = Quaternion.Euler(0,0, upperStartQuaternion.eulerAngles.z + degrees);
        Quaternion lowerTargetQuaternion = Quaternion.Euler(0,0, lowerStartQuaternion.eulerAngles.z + degrees);

        while (f < duration)
        {
            upperHalf.localRotation = Quaternion.Lerp(upperStartQuaternion, upperTargetQuaternion, f / duration);
            lowerHalf.localRotation = Quaternion.Lerp(lowerStartQuaternion, lowerTargetQuaternion, f / duration);

            f += Time.deltaTime;
            yield return null;
        }
        upperHalf.localRotation = upperTargetQuaternion;
        lowerHalf.localRotation = lowerTargetQuaternion;
    }

    IEnumerator OpenDoor(float duration, float distance)
    {
        float f = 0;

        Vector3 upperTargetPos = upperHalf.up * distance;
        Vector3 lowerTargetPos = lowerHalf.up * distance;

        while (f < duration)
        {


            //Move objects based on their localrotation away from parent position

            Vector3 upperPos = upperTargetPos * easeInQuart(f / duration);
            Vector3 lowerPos = lowerTargetPos * easeInQuart(f / duration);

            foreach (var renderer in _renderers)
            {
                renderer.materials[1].SetColor("_EmissionColor", Color.Lerp(Color.black, Color.green, f / duration));
            }

            upperHalf.position = transform.position + upperPos;
            lowerHalf.position = transform.position + lowerPos;

            f += Time.deltaTime;
            yield return null;
        }
    }


    IEnumerator CloseDoor(float duration, float distance)
	{
        float f = 0;

        Vector3 upperTargetPos = -upperHalf.up * distance;
        Vector3 lowerTargetPos = -lowerHalf.up * distance;

        Vector3 upperStartPos = upperHalf.position;
        Vector3 lowerStartPos = lowerHalf.position;

        while (f < duration)
		{

            Vector3 upperPos = upperTargetPos * easeInQuart(f / duration);
            Vector3 lowerPos = lowerTargetPos * easeInQuart(f / duration);

            foreach (var renderer in _renderers)
            {
                renderer.materials[1].SetColor("_EmissionColor", Color.Lerp(Color.green, Color.black, f/duration));
            }

            if (upperPos != Vector3.zero)
                upperHalf.position = upperStartPos + upperPos;
            if (lowerPos != Vector3.zero)
                lowerHalf.position = lowerStartPos + lowerPos;

            f += Time.deltaTime;
            yield return null;
        }
	}
}
