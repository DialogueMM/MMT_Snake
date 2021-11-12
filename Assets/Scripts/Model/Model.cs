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
    Ctrl _ctrl;

}
