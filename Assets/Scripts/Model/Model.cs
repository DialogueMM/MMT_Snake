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

    public bool IsValidMapPosition(Transform t)
	{
        if (!IsInsideMap(t)) 
            return false;
        Vector2 pos = t.position;
        if (_map[(int)pos.x, (int)pos.y] != null)
            return false;
        else
            return true;
	}
    private bool IsInsideMap(Transform t)
	{
        return t.position.x >= 0 && t.position.x < MAX_COLUMNS && t.position.y >= 0 && t.position.y < MAX_ROWS;
    }
}
