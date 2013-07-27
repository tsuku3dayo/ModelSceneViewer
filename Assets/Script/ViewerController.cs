using UnityEngine;
using System.Collections;

public class ViewerController : MonoBehaviour {

	public GameObject cameraObject;
	public GameObject targetObject;
	public GameObject lightHighObject;
	public GameObject lightLowObject;
	public GameObject gridObject;
	public GameObject measureObject;	

	// 拡縮（カメラのズームで再現）
	public float zoomSpeedScrollWheel = 1.0f;
	public float zoomSpeedMouseButton = 0.1f;
	public float zoomMin = 1.0f;
	public float zoomMax = 10.0f;

	// 移動（ターゲットとカメラの方を移動させることで実現）
	public float moveSpeed = 5.0f;
	// 回転（カメラを回転＆移動させることで実現）
	public float rotationSpeed = 5.0f;

	// ターゲットとカメラの距離
	private float cameraTargetDistanceZ;		
	private Vector3 cameraTargetDistanceOrign;
	// カメラの回転角（オイラー角）
	private Vector3 cameraEulerAngles;
	private Vector3 cameraEulerAnglesOrign;
	
	void Start () {
		// デバッグ用マーカーを消す
		targetObject.SetActive(false);
		// 初期値の保存
		cameraTargetDistanceOrign = cameraObject.transform.position - targetObject.transform.position;
		cameraEulerAnglesOrign = cameraObject.transform.eulerAngles;
		// 変数に初期値を設定
		cameraTargetDistanceZ = cameraTargetDistanceOrign.z;
		cameraEulerAngles = Vector3.zero;
	}
	
	void Update () {
		// リセット
		if (Input.GetKeyUp("a")) {
			ResetPosition();
			return;
		}
		// 拡縮（カメラのズームで実現）
		if (Input.GetAxis("Mouse ScrollWheel") != 0.0f) {
			cameraTargetDistanceZ += Input.GetAxis("Mouse ScrollWheel") * zoomSpeedScrollWheel;
			if (cameraTargetDistanceZ < zoomMin) cameraTargetDistanceZ = zoomMin;
			if (cameraTargetDistanceZ > zoomMax) cameraTargetDistanceZ = zoomMax;
		}
		if (Input.GetMouseButton(1)) {
			cameraTargetDistanceZ -= Input.GetAxis("Mouse X") * zoomSpeedMouseButton;
			cameraTargetDistanceZ += Input.GetAxis("Mouse Y") * zoomSpeedMouseButton;
			if (cameraTargetDistanceZ < zoomMin) cameraTargetDistanceZ = zoomMin;
			if (cameraTargetDistanceZ > zoomMax) cameraTargetDistanceZ = zoomMax;
		}
		// 移動（ターゲットとカメラの方を移動させることで実現）
		float moveX = 0.0f;
		float moveY = 0.0f;
		if (Input.GetMouseButton(2)) {
			moveX = Input.GetAxis("Mouse X") * moveSpeed;
			moveY = Input.GetAxis("Mouse Y") * moveSpeed;
		}
		// 回転（カメラを回転＆移動させることで実現）
		if (Input.GetMouseButton(0)) {
			cameraEulerAngles.y += Input.GetAxis("Mouse X") * rotationSpeed;
			cameraEulerAngles.x += Input.GetAxis("Mouse Y") * rotationSpeed;
		}
		// ターゲットを移動
		if (moveX > 0 ) targetObject.transform.Translate(Vector3.right *  moveX * Time.deltaTime);
		if (moveX < 0 ) targetObject.transform.Translate(Vector3.left  * -moveX * Time.deltaTime);
		if (moveY < 0 ) targetObject.transform.Translate(Vector3.up    * -moveY * Time.deltaTime);
		if (moveY > 0 ) targetObject.transform.Translate(Vector3.down  *  moveY * Time.deltaTime);
		// カメラを原点で回転
		cameraObject.transform.position = Vector3.zero;
		cameraObject.transform.rotation = Quaternion.Euler(cameraEulerAngles.x, cameraEulerAngles.y, 0.0f);
		// カメラを移動
		cameraObject.transform.position = cameraObject.transform.rotation * (new Vector3(cameraTargetDistanceOrign.x, cameraTargetDistanceOrign.y, cameraTargetDistanceZ)) + targetObject.transform.position;
		cameraObject.transform.Rotate(cameraEulerAnglesOrign);
		// カメラに対して並行移動させるために、ターゲットをカメラの方へ向けておく
		targetObject.transform.LookAt(cameraObject.transform);		
	}

	void ResetPosition () {
		// カメラ、ターゲットの座標・回転角を初期化
		cameraObject.transform.position = cameraTargetDistanceOrign;
		cameraObject.transform.rotation = Quaternion.Euler(cameraEulerAnglesOrign);
		targetObject.transform.position = Vector3.zero;
		targetObject.transform.rotation = Quaternion.identity;
		// 変数に初期値を設定
		cameraTargetDistanceZ = cameraTargetDistanceOrign.z;
		cameraEulerAngles = Vector3.zero;
	}

	void ChangeLightHigh () {
		lightHighObject.SetActive(true);
		lightLowObject.SetActive(false);
	}
	
	void ChangeLightLow () {
		lightLowObject.SetActive(true);
		lightHighObject.SetActive(false);
	}

	void OnGrid () {
		gridObject.SetActive(true);
	}
	
	void OffGrid () {
		gridObject.SetActive(false);
	}
	
	void OnMeasure () {
		measureObject.SetActive(true);
	}
	
	void OffMeasure () {
		measureObject.SetActive(false);
	}
}
