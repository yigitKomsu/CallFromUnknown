using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
	float interval = 1.25f;
	public char Value; //pulled from encoder_decoder
	public float x_val, y_val;
	public float keyValue;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		x_val = (transform.localPosition.x + 5) / interval;
		y_val = -(transform.localPosition.z - 5) / interval;
		x_val = Mathf.FloorToInt (x_val);
		y_val = Mathf.FloorToInt (y_val);
		//gotta do some math
		keyValue = 8 * y_val + x_val;
	}
}
