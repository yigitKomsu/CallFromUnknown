using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePopUpHandler : MonoBehaviour {
	public GameObject BuildPop, DisplayPopUp, FFPopUp;

	public void TogglePopUp(GameObject pop){
		if (pop.activeInHierarchy)
			pop.SetActive (false);
		else
			pop.SetActive (true);
	}

}
