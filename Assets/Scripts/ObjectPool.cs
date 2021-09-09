using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic Object Pool
public class ObjectPool : MonoBehaviour
{
	private static ObjectPool _instance;

	public static ObjectPool Instance => _instance;

	[SerializeField][Tooltip("The object that will be instantiated.")]
	private GameObject _gameObjectToPool;

	[SerializeField][Tooltip("The initial size of the pool.")]
	private int _initialPoolSize;

	private List<GameObject> _objectPool;

	[SerializeField][Tooltip("Toggle whether the pool can expand if required.")]
	private bool _canExpand = false;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
		}
	}


	private void Start()
	{
		GameObject instance;
		_objectPool = new List<GameObject>();
		for (int i = 0; i < _initialPoolSize; i++)
		{
			instance = Instantiate(_gameObjectToPool);
			instance.SetActive(false);
			_objectPool.Add(instance);
		}
	}

	public GameObject GetPooledObject()
	{
		//find an inactive object
		for (int i = 0; i < _objectPool.Count; i++)
		{
			if (!_objectPool[i].activeInHierarchy)
			{
				return _objectPool[i];
			}
		}
		
		if (!_canExpand)
			return null;

		//expand pool
		GameObject instance = Instantiate(_gameObjectToPool);
		instance.SetActive(false);
		_objectPool.Add(instance);
		return instance;
		
	}
}
