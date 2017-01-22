using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataNodeManager : MonoBehaviour {
	public int[] keyValues;
	public GameObject answerNodes;
	public Dictionary<char, int> inverted = new Dictionary<char, int> ();
	int x_val, y_val;
	float x_err, y_err;
	// Use this for initialization
	Encoder_Decoder ed;
	void Start () {
		keyValues = new int[8];
		int temp = 0;
		ed = GetComponent<Encoder_Decoder> ();
		for(int i = 0; i < 8; i++){
			char c = ed.input[i];
			inverted.TryGetValue (c, out temp);
			keyValues [i] = temp;
			x_val = keyValues [i] % 8;
			y_val = (keyValues [i] - x_val) / 8;
			x_err = Random.Range (-0.15f, 0.15f);
			y_err = Random.Range (-0.15f, 0.15f);
			answerNodes.transform.GetChild (i).transform.GetChild(0).transform.localPosition = new Vector3 (x_val * 1.25f - 5 + x_err, 81.9f, -(y_val * 1.25f + 5) + y_err + 9);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
