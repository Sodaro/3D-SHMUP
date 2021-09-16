using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if (UNITY_EDITOR)

[CustomEditor(typeof(WaypointPlacementTool))]
public class WaypointLineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //Called whenever the inspector is drawn for this object.
        DrawDefaultInspector();

        if (GUILayout.Button("Append New Waypoint"))
        {
            WaypointPlacementTool t = target as WaypointPlacementTool;
            t.CreateNewWaypoint(true);
        }

        if (GUILayout.Button("Insert New Waypoint"))
        {
            WaypointPlacementTool t = target as WaypointPlacementTool;
            t.CreateNewWaypoint(false);
        }


        if (GUILayout.Button("Waypoint LookAt Next"))
        {
            WaypointPlacementTool t = target as WaypointPlacementTool;
            for (int i = 0; i < t.gameObjects.Count - 1; i++)
            {
                t.gameObjects[i].transform.LookAt(t.gameObjects[i + 1].transform);
            }
        }
    }



    //Draw the approximate area the player can move within in the level
    //Further development would use beziers and make the camera follow that line instead of drawing a line purely as debug

    private void OnSceneGUI()
    {
        // get the chosen game object
        WaypointPlacementTool t = target as WaypointPlacementTool;

        if (t == null || t.gameObjects == null)
            return;

        int width = 12;
        int height = 6;

		for (int i = 0; i < t.gameObjects.Count-1; i++)
		{
            //matrix of current waypoint
            Matrix4x4 firstMatrix = t.gameObjects[i].transform.localToWorldMatrix;

            //matrix of next waypoint
            Matrix4x4 secondMatrix = t.gameObjects[i+1].transform.localToWorldMatrix;

            Matrix4x4 midMatrix = firstMatrix;

            Vector3 firstPos = t.gameObjects[i].transform.position;
            Vector3 secondPos = t.gameObjects[i+1].transform.position;



            //set positions of mid matrix to be midpoint of the first two matrices
            midMatrix[0, 3] = (firstPos.x + secondPos.x) / 2;
            midMatrix[1, 3] = (firstPos.y + secondPos.y) / 2;
            midMatrix[2, 3] = (firstPos.z + secondPos.z) / 2;

            //set handle matrix to be the mid point matrix
            Handles.matrix = midMatrix;

            //positions from matrices
            Vector3 p1 = new Vector3(firstMatrix[0, 3], firstMatrix[1, 3], firstMatrix[2, 3]);
            Vector3 p2 = new Vector3(secondMatrix[0, 3], secondMatrix[1, 3], secondMatrix[2, 3]);

            Handles.DrawWireCube(Vector3.zero, new Vector3(width, height, (p2 - p1).magnitude));
		}

	}
}
#endif