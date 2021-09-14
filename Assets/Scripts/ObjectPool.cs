using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{

	public static ObjectPool<T> Instance { get; private set; }

	[SerializeField]
	[Tooltip("The object that will be instantiated.")]
	private T _prefab;

	[SerializeField]
	[Tooltip("The initial size of the pool.")]
	private int _initialPoolSize;

	private Queue<T> _freeList;

	[SerializeField]
	[Tooltip("Toggle whether the pool can expand if required.")]
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

		//EventManager.onProjectileDisabled += OnProjectileDisabled;
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
		//find an inactive object
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
