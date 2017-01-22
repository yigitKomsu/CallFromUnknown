using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldControls : MonoBehaviour {
	Rigidbody rb;
	float sensitivity;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = new Vector3 (0, 0.3f, 0);
		rb.angularDrag = 0;
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetAxis ("Fire1") > 0) {
			rb.angularDrag = 1;
			rb.angularVelocity = Vector3.zero;
			rb.AddTorque (new Vector3 (Input.GetAxis ("MY") * 20, Input.GetAxis ("MX") * 20, 0));
		}

	}
}