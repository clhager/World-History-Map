using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float cameraSpeed = 1.0f;
	public float zoomSpeed = 1.0f;
	public float defaultZoom = 5.5f;

	public float minZoom = 1.5f;
	public float maxZoom = 5.0f;

	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = gameObject.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		/* Camera Scroll */
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		defaultZoom += scroll * zoomSpeed;
		if (defaultZoom < minZoom) {
			defaultZoom = minZoom;
		}
		if (defaultZoom > maxZoom) {
			defaultZoom = maxZoom;
		}
		mainCamera.orthographicSize = defaultZoom;

		/* Camera Movement */
		float moveX = transform.position.x + cameraSpeed * defaultZoom * Input.GetAxis ("Horizontal");
		float moveY = transform.position.y + cameraSpeed * defaultZoom * Input.GetAxis ("Vertical");

		float bottom = minY + defaultZoom;
		float top = maxY - defaultZoom;
		if (moveX < minX) {
			moveX = maxX - (minX - moveX);
		}
		if (moveX > maxX) {
			moveX = minX + (moveX - maxX);
		}
		if (moveY < bottom) {
			moveY = bottom;
		}
		if (moveY > top) {
			moveY = top;
		}
		transform.position = new Vector3 (moveX, moveY, -10);
	}


}
