using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sensor _sensor;
    [SerializeField] Transform _upperHalf;
    [SerializeField] Transform _lowerHalf;

    [SerializeField] private float _delay;
    [SerializeField] private float _delayBetweenSequence;

    [SerializeField] private float _rotationDuration = 1f;
    [SerializeField] private float _moveDuration = 1f;
    [SerializeField] private float _moveDistance;

    [SerializeField] private float _rotationDegrees;

    private MeshRenderer[] _renderers;

    [SerializeField] private bool _isOpen;
    private void Awake()
    {
        _renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in _renderers)
            renderer.materials[1].EnableKeyword("_EMISSION");

        if (_sensor == null)
            return;

        _sensor._playerEnteredEvent.AddListener(delegate 
        {
            if (_isOpen)
                CloseDoor();
            else
                OpenDoor();
        });
    }

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
    }

    public void OpenDoor()
	{
        if (!_isOpen)
            StartCoroutine(RotateAndOpen(_delay, _delayBetweenSequence));
    }

    public void CloseDoor()
	{
        if (_isOpen)
            StartCoroutine(CloseAndRotate(_delay, _delayBetweenSequence));
    }

    void MoveHalvesToOpenPositions()
	{
        _upperHalf.localRotation = Quaternion.Euler(0, 0, _upperHalf.transform.localRotation.eulerAngles.z + _rotationDegrees);
        _lowerHalf.localRotation = Quaternion.Euler(0, 0, _lowerHalf.transform.localRotation.eulerAngles.z + _rotationDegrees);

        _upperHalf.position = transform.position + _upperHalf.up * _moveDistance;
        _lowerHalf.position = transform.position + _lowerHalf.up * _moveDistance;
    }

    float easeInQuart(float x)
    {
        //credits easings.net
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

        yield return StartCoroutine(RotateZ(_rotationDuration, _rotationDegrees));
        yield return new WaitForSeconds(delayBetweenSequence);
        yield return StartCoroutine(OpenDoor(_moveDuration, _moveDistance));
        _isOpen = true;
        
    }
    IEnumerator CloseAndRotate(float startDelay, float delayBetweenSequence)
	{

        if (startDelay > 0)
            yield return new WaitForSeconds(startDelay);

        yield return StartCoroutine(CloseDoor(_moveDuration, _moveDistance));
        yield return new WaitForSeconds(delayBetweenSequence);
        yield return StartCoroutine(RotateZ(_rotationDuration, -(_rotationDegrees)));
        _isOpen = false;
    }

    IEnumerator RotateZ(float duration, float degrees)
	{
        float f = 0;
        
        Quaternion upperStartQuaternion = _upperHalf.localRotation;
        Quaternion lowerStartQuaternion = _lowerHalf.localRotation;
        Quaternion upperTargetQuaternion = Quaternion.Euler(0,0, upperStartQuaternion.eulerAngles.z + degrees);
        Quaternion lowerTargetQuaternion = Quaternion.Euler(0,0, lowerStartQuaternion.eulerAngles.z + degrees);

        while (f < duration)
        {
            _upperHalf.localRotation = Quaternion.Lerp(upperStartQuaternion, upperTargetQuaternion, f / duration);
            _lowerHalf.localRotation = Quaternion.Lerp(lowerStartQuaternion, lowerTargetQuaternion, f / duration);

            f += Time.deltaTime;
            yield return null;
        }
        _upperHalf.localRotation = upperTargetQuaternion;
        _lowerHalf.localRotation = lowerTargetQuaternion;
    }

    IEnumerator OpenDoor(float duration, float distance)
    {
        float f = 0;

        Vector3 upperTargetPos = _upperHalf.up * distance;
        Vector3 lowerTargetPos = _lowerHalf.up * distance;

        while (f < duration)
        {


            //Move objects based on their localrotation away from parent position

            Vector3 upperPos = upperTargetPos * easeInQuart(f / duration);
            Vector3 lowerPos = lowerTargetPos * easeInQuart(f / duration);

            foreach (var renderer in _renderers)
            {
                renderer.materials[1].SetColor("_EmissionColor", Color.Lerp(Color.black, Color.green, f / duration));
            }

            _upperHalf.position = transform.position + upperPos;
            _lowerHalf.position = transform.position + lowerPos;

            f += Time.deltaTime;
            yield return null;
        }
    }


    IEnumerator CloseDoor(float duration, float distance)
	{
        float f = 0;

        Vector3 upperTargetPos = -_upperHalf.up * distance;
        Vector3 lowerTargetPos = -_lowerHalf.up * distance;

        Vector3 upperStartPos = _upperHalf.position;
        Vector3 lowerStartPos = _lowerHalf.position;

        while (f < duration)
		{

            Vector3 upperPos = upperTargetPos * easeInQuart(f / duration);
            Vector3 lowerPos = lowerTargetPos * easeInQuart(f / duration);

            foreach (var renderer in _renderers)
            {
                renderer.materials[1].SetColor("_EmissionColor", Color.Lerp(Color.green, Color.black, f/duration));
            }

            if (upperPos != Vector3.zero)
                _upperHalf.position = upperStartPos + upperPos;
            if (lowerPos != Vector3.zero)
                _lowerHalf.position = lowerStartPos + lowerPos;

            f += Time.deltaTime;
            yield return null;
        }
	}
}
