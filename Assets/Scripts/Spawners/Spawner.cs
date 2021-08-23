using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private AnimationCurve _distribution;
	[SerializeField] private GameObject[] _objectsToSpawn;

	[Header("Time")]
	[SerializeField, Tooltip("In seconds.")] private float _timeBeforeFirstSpawn;
	[SerializeField, Tooltip("In seconds.")] private float _timeBetweenSpawns;

	private Transform _transform;
	private Coroutine _spawnRoutine;

	private IEnumerator SpawnObjects()
	{
		yield return new WaitForSeconds(_timeBeforeFirstSpawn);

		while (true)
		{
			CreateObject();
			yield return new WaitForSeconds(_timeBetweenSpawns);
		}

		void CreateObject()
		{
			Vector3 spawnPosition = _transform.position;
			spawnPosition.x += Random.Range(-3, 3);
			spawnPosition.y += Random.Range(-3, 3);
			spawnPosition.z += Random.Range(-3, 3);
			GameObject go = _objectsToSpawn[Mathf.RoundToInt(_distribution.Evaluate(Random.value))];
			Instantiate(go, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360f), 0));
		}
	}

	private void OnEnable()
	{
		_spawnRoutine = StartCoroutine(SpawnObjects());
	}
	private void OnDisable()
	{
		StopCoroutine(_spawnRoutine);
	}

	private void Awake()
	{
		_transform = transform;
	}
}
