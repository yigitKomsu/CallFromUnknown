using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDataManager : MonoBehaviour {
	GameObject[] nodes;
	public GameObject node;
	public GameObject Button;
	public int[] values;
	int i;
	public float time;
	// Use this for initialization
	void Start () {
		nodes = new GameObject[node.transform.childCount];
		values = new int[nodes.Length - 2];
		for (i = 0; i < node.transform.childCount; i++)
			nodes [i] = node.transform.GetChild (i).transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (i < nodes.Length - 2) {
			values [i] = (int)nodes [i].GetComponent<Node> ().keyValue;
			i++;
		} else {
			i = 0;
		}
		if (time > 1 && time < 2) {
			Button.SetActive (false);
			GameObject.Find ("EventSystem").GetComponent<Encoder_Decoder> ().PrintMessage (false);
		} else {
			Button.SetActive (true);
		}
	}
}
