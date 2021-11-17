using UnityEngine;
public class GameManager:MonoBehaviour
{
	public Food[] Foods;
	public Transform BlockHolder;
	public Snake snake;
	private Ctrl ctrl;
	private bool _isPause = true;
	private Food _currentFood = null;
	
	private void Awake()
	{
		ctrl = GetComponent<Ctrl>();
	}

	private void Update()
	{
		if (_isPause)
			return;
		if (_currentFood == null)
		{
			SpawnFood();
		}
		snake.gameObject.SetActive(true);
	}

	public void StartGame()
	{
		_isPause = false;
	}

	public void SpawnFood()
	{
		int index = Random.Range(0, Foods.Length);
		//ff:生成的坐标不应该在蛇身上
		

		_currentFood = Instantiate(Foods[index], BlockHolder);
		int colume, row;
		RandomPosition(out colume, out row);
		_currentFood.transform.position = new Vector3(colume, row);
	}

	private void RandomPosition(out int colume ,out int row)
	{
		colume = Random.Range(0, Model.MAX_COLUMNS);
		row = Random.Range(0, Model.MAX_ROWS);
		if(IsInsideSnakeBody(new Vector3(colume,row)))
		{
			RandomPosition(out colume,out row);
		}
	}

	private bool IsInsideSnakeBody(Vector3 pos)
	{
		foreach (GameObject obj in snake.Body)
		{
			if (obj.transform.position == pos)
				return true;
		}
		return false;
	}

	public void EatFood(Vector3 pos)
	{
		snake.AddBody(pos);
		ctrl.model.UpdateScore(_currentFood.Score);
		ctrl.view.UpdateGameUI(ctrl.model.Score, ctrl.model.HighScore);
		Destroy(_currentFood.gameObject);
		_currentFood = null;
	}

	public void CheckFoodPosition(Vector3 pos)
	{
		if(_currentFood.transform.position == pos)
		{
			EatFood(pos);
		}
	}
}

