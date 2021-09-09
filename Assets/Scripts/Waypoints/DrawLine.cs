using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawLine : MonoBehaviour
{
    // an array of game objects which will have a
    // line drawn to in the Scene editor
    public List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] GameObject _waypointPrefab;

    public void CreateNewWaypoint()
	{
        if (gameObjects.Count > 0)
            gameObjects.Add(Instantiate(_waypointPrefab, gameObjects[gameObjects.Count-1].transform.position, Quaternion.identity, transform));
        else
            gameObjects.Add(Instantiate(_waypointPrefab, transform));
    }
}