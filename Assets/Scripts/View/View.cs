using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class View : MonoBehaviour
{
	public RectTransform Title;
	public RectTransform MenuUI;
	public RectTransform GameUI;

	public GameObject GameOverUI;
   

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
}
