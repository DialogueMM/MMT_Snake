using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class View : MonoBehaviour
{
	public RectTransform Title;
	public RectTransform MenuUI;
	public RectTransform GameUI;

	public Text StartButtonText;

	public GameObject GameOverUI;
	public GameObject RankUI;
	public GameObject SettingUI;
	public GameObject MuteObj;
	public GameObject RestartButton;

    public void ShowMenuUI()
	{
		MenuUI.gameObject.SetActive(true);
		MenuUI.DOAnchorPosX(340f, 0.8f);
		Title.gameObject.SetActive(true);
		Title.DOAnchorPosX(340f, 0.8f);
	}

    public void HideMenuUI()
	{
		MenuUI.DOAnchorPosX(-200f, 0.5f).OnComplete(delegate
		{
			MenuUI.gameObject.SetActive(false);
		});
		Title.DOAnchorPosX(-230f, 0.5f).OnComplete(delegate
		{
			Title.gameObject.SetActive(false);
		});
	}

	public void ShowGameUI()
	{
		GameUI.gameObject.SetActive(true);
		GameUI.DOAnchorPosX(275f, 0.8f);
	}
	public void HideGameUI()
	{
		GameUI.DOAnchorPosX(-275f, 0.8f).OnComplete(delegate 
		{
			GameUI.gameObject.SetActive(false);
		});
	}

	public void ShowRankUI()
	{
		RankUI.SetActive(true);
		//ff:增加数据
	}

	public void OnRankUIClick()
	{
		RankUI.SetActive(false);
	}

	public void ShowSettingUI()
	{
		SettingUI.SetActive(true);
	}
	public void OnSettingUIClick()
	{
		SettingUI.SetActive(false);
	}
	public void SetMuteActive(bool isActive)
	{
		MuteObj.SetActive(isActive);
	}
	public void SetStartToContinue()
	{
		StartButtonText.text = "Continue";
	}

	public void ShowRestartButton()
	{
		RestartButton.SetActive(true);
	}
}
