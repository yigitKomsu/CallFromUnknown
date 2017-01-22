using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGridScript : MonoBehaviour {
	GameObject myNode;
	Ray ray;
	RaycastHit hit;
	public AudioClip AC_Freq;
	AudioSource AS;
	// Use this for initialization
	void Start () {
		myNode = transform.GetChild (0).gameObject;
		AS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if(Physics.Raycast(ray, out hit)){
			myNode.transform.position = new Vector3(hit.point.x, hit.point.y, transform.position.y);
			AS.PlayOneShot (AC_Freq);
		}
	}
}
