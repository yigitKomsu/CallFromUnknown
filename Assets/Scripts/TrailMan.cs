using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMan : MonoBehaviour {
	GameObject[] nodes;
	GameObject spawner;
	int index;
	int childCount;
	float lerpSpd;
	// Use this for initialization
	void Start () {
		spawner = GameObject.Find ("TrailSpawner");
		childCount = spawner.GetComponent<TrailManShooter> ().Node.transform.childCount;
		nodes = new GameObject[childCount];
		for (int i = 0; i < childCount; i++)
			nodes [i] = spawner.GetComponent<TrailManShooter> ().Node.transform.GetChild(i).transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward / 10);

		Quaternion toRot = Quaternion.FromToRotation (transform.forward, nodes [index].transform.position - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, toRot, 0.3f); //TODO: rotate speed relative to 1/x_distance
		
		if (transform.position.x > nodes [index].transform.position.x) {
			index++;
			if (index == childCount - 1)
				Destroy (gameObject);
		}
		
	}
}
