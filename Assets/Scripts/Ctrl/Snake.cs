using System.Collections.Generic;
using UnityEngine;
public class Snake: MonoBehaviour
{
	[HideInInspector]
	public List<GameObject> Body = new List<GameObject>();
	public GameObject SnakeBody;
	Ctrl _ctrl;
	private bool _isPause = false;
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
		Body.Add(gameObject);
		_ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Ctrl>();
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
	private void Spawn(Vector3 dir)
	{
		GameObject g = Instantiate(SnakeBody, _ctrl.gameManager.BlockHolder);
		g.transform.position = dir;
		Body.Add(g);
		Debug.Log("Spawn !");
	}

	public void AddBody(Vector3 dir, int bodyNum = 1)
	{
		for (int i = 0; i < bodyNum; i++)
		{
			Spawn(dir);
		}
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
		if (!_ctrl.model.IsValidMapPosition(Body[0].transform.position + dir))
		{
			_isPause = true;
		}
		else
		{
			_ctrl.gameManager.CheckFoodPosition(Body[0].transform.position + dir);
			MoveBody(dir);
		}
	}

	private void MoveBody(Vector3 dir)
	{
		for (int i = Body.Count - 1; i > 0; i--)
		{
			Body[i].transform.position = Body[i - 1].transform.position;
		}
		Body[0].transform.position += dir;
	}
}