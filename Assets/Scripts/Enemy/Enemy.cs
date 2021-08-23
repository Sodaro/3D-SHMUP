using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private int _points = 5;


	/// Return amount of points
	public int Hit()
	{
		Destroy(gameObject);
		return _points;
	}
}
