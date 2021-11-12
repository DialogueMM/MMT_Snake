using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : FSMState
{
	private void Awake()
	{
		stateID = StateID.Play;
		AddTransition(Transition.PauseButtonClick, StateID.Menu);
	}

	public override void DoBeforeEntering()
	{
		ctrl.view.ShowGameUI();
		ctrl.cameraManager.ZoomIn();

	}
	public override void DoBeforeLeaving()
	{
		ctrl.view.HideGameUI();
	}
	public void OnPauseBtnClick()
	{
		fsm.PerformTransition(Transition.PauseButtonClick);
	}
}
