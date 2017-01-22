using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManShooter : MonoBehaviour {
	GameObject TrailMan;
	public GameObject Node;
	float time;
	// Use this for initialization
	void Start () {
		TrailMan = Resources.Load ("TrailDrawer") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 0.8f) {
			Instantiate (TrailMan, transform.position, Quaternion.Euler (0, 90, 0));
			time = 0;
		}
	}
}
