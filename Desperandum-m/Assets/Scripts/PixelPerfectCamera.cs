using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode()]
public class PixelPerfectCamera : MonoBehaviour {
	public float PixelPerUnit = 32f;
	public float PixelScale = 1f;
	private Camera _camera;

	private void Awake() {
		_camera = GetComponent<Camera> ();
	}

	private void OnValidate() {
		_camera.orthographicSize = (Screen.height) / (PixelPerUnit * PixelScale) * 0.5f;
	}
}
