using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
	public GameObject Relay;
	public GameObject relayPanel, worldPanel, callPanel, tutorialPanel, pausePanel;
	public GameObject noteTxt, creditTxt, thanksTxt, panel;
	public GameObject[] arrows;
	GameObject GDM, WC, LD;
	AudioSource AS;
	public AudioClip AC_Button, AC_Build, AC_Error;
	void Start(){
		GDM = GameObject.Find ("GameManager");
		WC = GameObject.Find ("WORLD");
		LD = GameObject.Find("DontDestroy");
		AS = GetComponent<AudioSource> ();
	}

	public void StartGame(){
		AS.PlayOneShot (AC_Button);
		GDM.SetActive (true);
		WC.SetActive (true);
		for (int i = 0; i < WC.transform.childCount; i++) {
			WC.transform.GetChild(i).gameObject.SetActive (true);
			for (int j = 1; j < WC.transform.GetChild(i).childCount; j++) {
				WC.transform.GetChild(i).GetChild(j).gameObject.SetActive (true);
			}
		}
		LD.SetActive (true);
		GDM.GetComponent<GameDataManager> ().UpdateCredit ();
		SceneManager.LoadScene ("WorldScene");
	}

	public void MyNote(){
		AS.PlayOneShot (AC_Button);
		if (noteTxt.activeInHierarchy)
			noteTxt.SetActive (false);
		else
			noteTxt.SetActive (true);

		creditTxt.SetActive (false);
		thanksTxt.SetActive (false);
	}

	public void TogglePanel(){
		AS.PlayOneShot (AC_Button);
		if (panel.activeInHierarchy)
			panel.SetActive (false);
		else
			panel.SetActive (true);

		creditTxt.SetActive (false);
		noteTxt.SetActive (false);
		thanksTxt.SetActive (false);
	}

	public void ToggleCredits(){
		AS.PlayOneShot (AC_Button);
		if (creditTxt.activeInHierarchy)
			creditTxt.SetActive (false);
		else
			creditTxt.SetActive (true);

		noteTxt.SetActive (false);
		thanksTxt.SetActive (false);
	}

	public void ToggleThanks(){
		AS.PlayOneShot (AC_Button);
		if (thanksTxt.activeInHierarchy)
			thanksTxt.SetActive (false);
		else
			thanksTxt.SetActive (true);

		noteTxt.SetActive (false);
		creditTxt.SetActive (false);
	}

	public void PauseGame(){
		AS.PlayOneShot (AC_Button);
		//find every active gameObject, set Time.timeScale = 0;
		GameObject[] activeObjects = GameObject.FindObjectsOfType<GameObject> ();
		foreach (GameObject ao in activeObjects) {
			if (ao.activeInHierarchy && ao.gameObject.name != "Main Camera" && ao.gameObject != gameObject && ao.gameObject.name != "Canvas")
				ao.SetActive (false);
		}
		pausePanel.SetActive (true);
	}

	public void ToggleTutorial(){
		AS.PlayOneShot (AC_Button);
		if (tutorialPanel.activeInHierarchy)
			tutorialPanel.SetActive (false);
		else
			tutorialPanel.SetActive (true);
	}

	public void QuitGame(){
		AS.PlayOneShot (AC_Button);
		Application.Quit ();
	}

	public void SpawnRelay(){
		AS.PlayOneShot (AC_Button);
		Instantiate (Relay, transform.position, Quaternion.Euler (0, 0, Random.Range (0, 360)));
		GDM.GetComponent<GameDataManager> ().UpdateCredit ();
	}

	public void AcceptMission(){
		AS.PlayOneShot (AC_Button);
		Relay.GetComponent<Relay> ().Alarm.SetActive (false);
		SceneManager.LoadScene ("SignalInterpret");
	}

	public void DisplayTheCall(){
		AS.PlayOneShot (AC_Button);
		callPanel.transform.GetChild (0).transform.GetChild (0).GetComponent<Text> ().text = GDM.GetComponent<GameDataManager> ().displayMessage;
		if (callPanel.activeInHierarchy)
			callPanel.SetActive (false);
		else
			callPanel.SetActive (true);
	}

	public void EndGame(){
		DisplayTheCall ();
	}

	public void FastForward(){
		
		if (GDM.GetComponent<GameDataManager> ().speed < 30) { //speed up max three times
			GDM.GetComponent<GameDataManager> ().speed += 10;
			AS.PlayOneShot (AC_Button);
		} else
			AS.PlayOneShot (AC_Error);
		if(!arrows [0].activeInHierarchy)
			arrows [0].SetActive (true);
		else if(!arrows [1].activeInHierarchy)
			arrows [1].SetActive (true);
		else if(!arrows [2].activeInHierarchy)
			arrows [2].SetActive (true);
	}

	public void DeclineMission(){
		AS.PlayOneShot (AC_Button);
		Relay.GetComponent<Relay> ().Alarm.SetActive (false);
		Camera.main.GetComponent<CameraCode> ().LerpPos = GameObject.Find ("MainCameraLerp");
		Camera.main.GetComponent<CameraCode> ().LookAt = gameObject;
		GameObject.Find("DontDestroy").GetComponent<LevelManager> ().input = "";
		worldPanel.SetActive (true);
		relayPanel.SetActive (false);
	}

	public void SetMission(){
		worldPanel.SetActive (false);
		relayPanel.SetActive (true);
	}
}
