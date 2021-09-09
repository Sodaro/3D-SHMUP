using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
	private static BulletObjectPool _instance;

	public static BulletObjectPool Instance => _instance;

	[SerializeField]
	[Tooltip("The object that will be instantiated.")]
	private PlasmaBullet _gameObjectToPool;

	[SerializeField]
	[Tooltip("The initial size of the pool.")]
	private int _initialPoolSize;

	private Queue<PlasmaBullet> _freeList;

	[SerializeField]
	[Tooltip("Toggle whether the pool can expand if required.")]
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

		EventManager.onBulletDisabled += OnBulletBecameDisabled;
	}
	private void Start()
	{
		_freeList = new Queue<PlasmaBullet>();
		for (int i = 0; i < _initialPoolSize; i++)
		{
			PlasmaBullet instance = Instantiate(_gameObjectToPool.gameObject).GetComponent<PlasmaBullet>();
			instance.gameObject.SetActive(false);
			_freeList.Enqueue(instance);
		}
	}

	void OnBulletBecameDisabled(PlasmaBullet bullet)
	{
		_freeList.Enqueue(bullet);
	}

	public PlasmaBullet GetPooledObject()
	{
		//find an inactive object
		if (_freeList.Count != 0)
			return _freeList.Dequeue();
		if (!_canExpand)
			return null;

		PlasmaBullet instance = Instantiate(_gameObjectToPool.gameObject).GetComponent<PlasmaBullet>();
		instance.gameObject.SetActive(false);
		_freeList.Enqueue(instance);
		return instance;



	}
}
