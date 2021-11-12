using UnityEngine;
public class GameManager:MonoBehaviour
{
	public Food[] Foods;
	public Color[] Colors;
	public Ctrl ctrl;
	private Food _currentFood = null;


	public void CreateFood()
	{
		int index = Random.Range(0, Foods.Length);
		//ff:生成的坐标不应该在蛇身上
		int colume = Random.Range(0, Model.MAX_COLUMNS);
		int row = Random.Range(0, Model.MAX_ROWS);

		_currentFood = Instantiate(Foods[index], ctrl.model._map[colume,row]);
	}
}

