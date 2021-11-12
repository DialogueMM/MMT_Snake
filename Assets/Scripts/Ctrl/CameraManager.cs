using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
	Camera _mainCamera;

	private void Awake()
	{
		_mainCamera = Camera.main;
	}

	public void ZoomIn()
	{
		_mainCamera.DOOrthoSize(9f, 0.8f);
	}
	public void ZoomOut()
	{
		_mainCamera.DOOrthoSize(12f, 0.8f);
	}
}
