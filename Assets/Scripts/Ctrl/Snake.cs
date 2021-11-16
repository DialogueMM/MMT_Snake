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
	private GameManager _gameManager;
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
			//ff:执行蛇移动操作
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
	private void Spawn(Vector3 position)
	{
		GameObject g = Instantiate(SnakeBody, _ctrl.gameManager.BlockHolder);
		g.transform.position = position;
		Body.Add(g);
		Debug.Log("Spawn !");
	}

	public void AddBody(Vector3 pos, int bodyNum = 1)
	{
		for (int i = 0; i < bodyNum; i++)
		{
			Spawn(pos);
		}
	}
	private void Move()
	{
		if(_direction == Direction.UP)
		{
			Body[0].transform.position += Vector3.up;
			if (!_ctrl.model.IsValidMapPosition(Body[0].transform))
			{
				Body[0].transform.position -= Vector3.up;
				_isPause = true;
			}
			else
			{
				MoveBody();
			}
		}
		else if(_direction == Direction.DOWN)
		{
			Body[0].transform.position += Vector3.down;
			if (!_ctrl.model.IsValidMapPosition(Body[0].transform))
			{
				Body[0].transform.position += Vector3.down;
				_isPause = true;
			}
			else
			{
				MoveBody();
			}
		}
		else if (_direction == Direction.LEFT)
		{
			Body[0].transform.position += Vector3.left;
			if (!_ctrl.model.IsValidMapPosition(Body[0].transform))
			{
				Body[0].transform.position -= Vector3.left;
				_isPause = true;
			}
			else
			{
				MoveBody();
			}
		}
		else if (_direction == Direction.RIGHT)
		{
			Body[0].transform.position += Vector3.right;
			if (!_ctrl.model.IsValidMapPosition(Body[0].transform))
			{
				Body[0].transform.position -= Vector3.right;
				_isPause = true;
			}
			else
			{
				MoveBody();
			}
		}
		
	}
	private void MoveBody()
	{
		for (int i = Body.Count-1; i > 0 ; i--)
		{
			Body[i] = Body[i-1];
		}
	}
}