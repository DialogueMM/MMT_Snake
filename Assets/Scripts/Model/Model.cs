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
    public int[,] _map = new int[MAX_COLUMNS, MAX_ROWS];

    private int _score = 0;
    public int Score
	{
		get { return _score; }
	}

    private int _highScore = 0;
    public int HighScore
    {
        get { return _highScore; }
    }

    private bool _isGameOver = false;
    public bool IsGameOver
	{
        get { return _isGameOver; }
	}

	private void Awake()
	{
        LoadData();
	}

	public bool IsValidMapPosition(Vector3 pos,List<GameObject> body)
	{
        if (!IsInsideMap(pos) || IsSnakeBody(body, pos))
        {
            _isGameOver = true;
            SaveData();
            return false;
        }
		
        return true;
	}

	private bool IsSnakeBody(List<GameObject> body, Vector3 dir)
	{
		foreach (GameObject obj in body)
		{
			if (obj.transform.position == dir)
				return true;
		}
		return false;
	}
    private bool IsInsideMap(Vector3 pos)
	{
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0 && pos.y < MAX_ROWS;
    }

    public void UpdateScore(int score)
	{
        _score += score;
        if(_score>_highScore)
		{
            _highScore = _score;
		}
	}

    public void LoadData()
	{
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
	}

    public void SaveData()
	{
        PlayerPrefs.SetInt("HighScore", _highScore);
	}

    public void Restart()
	{
        _score = 0;
        _isGameOver = false;
        LoadData();
    }
}
