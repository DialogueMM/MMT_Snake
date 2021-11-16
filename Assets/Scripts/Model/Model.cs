using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    [HideInInspector]
    public const int MAX_ROWS = 16;
    [HideInInspector]
    public const int MAX_COLUMNS = 20;
    [HideInInspector]
    public Transform[,] _map = new Transform[MAX_COLUMNS, MAX_ROWS];

    public bool IsValidMapPosition(Vector3 dir)
	{
        if (!IsInsideMap(dir)) 
            return false;
        
        if (_map[(int)dir.x, (int)dir.y] != null)
            return false;
        else
            return true;
	}
    private bool IsInsideMap(Vector3 dir)
	{
        return dir.x >= 0 && dir.x < MAX_COLUMNS && dir.y >= 0 && dir.y < MAX_ROWS;
    }

}
