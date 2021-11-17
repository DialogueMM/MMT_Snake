using System.Collections.Generic;
using UnityEngine;
public class Snake: MonoBehaviour
{
	[HideInInspector]
	public List<GameObject> Body = new List<GameObject>();
	public GameObject SnakeBody;
	Ctrl _ctrl;
	private bool _isPause = true;
	private float _timer = 0;
	private float _stepTime = 0.5f;
	private Direction _direction = Direction.RIGHT;
	private enum Direction
	{
		UP,
		DOWN,
		RIGHT,
		LEFT
	}

	private void Awake()
	{
		_ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Ctrl>();
		Body.Add(gameObject);
	}

	private void Update()
	{
		if (_isPause)
			return;
		_timer += Time.deltaTime;
		if(_timer>_stepTime)
		{
			_timer = 0;
			Move();
		}
		InputControl();
	}

	private void InputControl()
	{
		if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
		{
			_direction = Direction.UP;
		}
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			_direction = Direction.LEFT;
		}
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			_direction = Direction.DOWN;
		}
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			_direction = Direction.RIGHT;
		}
	}
	public void AddBody(Vector3 pos)
	{
		GameObject g = Instantiate(SnakeBody, _ctrl.gameManager.BlockHolder);
		g.transform.position = pos;
		Body.Add(g);
		Debug.Log("Spawn !");
	}

	private void Move()
	{
		if (_direction == Direction.UP)
		{
			CheckMove(Vector3.up);
		}
		else if(_direction == Direction.DOWN)
		{
			CheckMove(Vector3.down);
		}
		else if (_direction == Direction.LEFT)
		{
			CheckMove(Vector3.left);
		}
		else if (_direction == Direction.RIGHT)
		{
			CheckMove(Vector3.right);
		}
	}

	private void CheckMove(Vector3 dir)
	{
		Vector3 newPos = Body[0].transform.position + dir;
		if (!_ctrl.model.IsValidMapPosition(newPos,Body))
		{
			PauseGame();
		}
		else
		{
			_ctrl.gameManager.CheckFoodPosition(newPos);
			MoveBody(dir);
		}
	}

	private void MoveBody(Vector3 pos)
	{
		for (int i = Body.Count - 1; i > 0; i--)
		{
			Body[i].transform.position = Body[i - 1].transform.position;
		}
		Body[0].transform.position += pos;
	}
	public void Restart()
	{
		GameObject head = Body[0];
		for (int i = 1; i < Body.Count; i++)
		{
			Destroy(Body[i].gameObject);
		}
		Body.Clear();
		head.transform.position = new Vector3(13, 13);
		_direction = Direction.RIGHT;
		Body.Add(head);
	}
	public void PauseGame()
	{
		_isPause = true;
	}
	public void Resume()
	{
		_isPause = false;
	}
}