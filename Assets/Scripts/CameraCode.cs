using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour {
	public GameObject LerpPos;
	public GameObject LookAt;
	GameDataManager GDM;
	GameObject DL;
	// Use this for initialization
	void Start() {
		GDM = GameObject.Find ("GameManager").GetComponent<GameDataManager> ();
		DL = GameObject.Find ("Directional Light");
	}
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (Camera.main.transform.position, LerpPos.transform.position, Time.deltaTime * 0.8f);
		transform.LookAt (LookAt.transform);
		GDM.dateUpDate ();
		DL.transform.Rotate (0, GDM.speed, 0, Space.World);
	}
}
