using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState
{
	private void Awake()
	{
		stateID = StateID.Menu;
		AddTransition(Transition.StartButtonClick, StateID.Play);
	}
	public override void DoBeforeEntering()
	{
		ctrl.view.ShowMenuUI();
		ctrl.cameraManager.ZoomOut();
	}
	public override void DoBeforeLeaving()
	{
		ctrl.view.HideMenuUI();
	}

	public void OnStartButtonClick()
	{
		fsm.PerformTransition(Transition.StartButtonClick);
	}

	public void OnRankButtonClick()
	{
		ctrl.view.ShowRankUI();
	}
	public void OnSettingButtonClick()
	{
		ctrl.view.ShowSettingUI();
	}

	public void OnRestartButtonClick()
	{
		//ff:Çå³ýÊý¾Ý
		fsm.PerformTransition(Transition.StartButtonClick);
		ctrl.gameManager.StartGame();
	}
	public void OnQuitButtonClick()
	{
		Application.Quit();
	}
}
