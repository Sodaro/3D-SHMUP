using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{
	/*
	 * Converted my implementation of an object pool (consisting of only bullets) to one using Generics
	 * with the help of Jason Weimann's video: https://youtu.be/uxm4a0QnQ9E?t=1453
	*/

	/// <summary>
	/// This class uses the object pooling pattern to instantiate a specified amount of objects of a certain type
	/// and allows any object to get an object from the pool. Instead of destroying objects we disable them
	/// and return them to the pool for reuse.
	/// </summary>

	public static ObjectPool<T> Instance { get; private set; }

	[SerializeField]
	[Tooltip("The object that will be instantiated.")]
	private T _prefab;

	[SerializeField]
	[Tooltip("The initial size of the pool.")]
	private int _initialPoolSize;

	private Queue<T> _freeList;

	[SerializeField]
	[Tooltip("Toggle whether the pool can expand if needed.")]
	private bool _canExpand = false;

	protected void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}
	protected void Start()
	{
		_freeList = new Queue<T>();
		for (int i = 0; i < _initialPoolSize; i++)
		{
			T instance = Instantiate(_prefab);
			instance.gameObject.SetActive(false);
			_freeList.Enqueue(instance);
		}
	}

	public void AddBackToPool(T objectToAdd)
	{
		_freeList.Enqueue(objectToAdd);
	}

	protected void OnGameObjectDisabled(T disabledObject)
	{
		_freeList.Enqueue(disabledObject);
	}

	public T GetPooledObject()
	{
		if (_freeList.Count != 0)
			return _freeList.Dequeue();
		if (!_canExpand)
			return null;

		T instance = Instantiate(_prefab);
		instance.gameObject.SetActive(false);
		_freeList.Enqueue(instance);
		return instance;
	}
}
