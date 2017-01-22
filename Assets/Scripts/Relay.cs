using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relay : MonoBehaviour {
	GameObject EventSystem;
	GameObject World;
	Ray ray;
	RaycastHit hit;
	public bool placable, placed;
	public LayerMask mask;
	GameDataManager GDM;
	public int creditValue;
	GameObject errorText;
	public GameObject Alarm;
	GameObject LerpTo;
	LevelManager LM;
	float time;
	public float speed;
	string inc_msg;
	AudioSource AS;
	public AudioClip AC_Place, AC_Transmission;
	// Use this for initialization
	void Start () {
		AS = GetComponent<AudioSource> ();
		speed = 1;
		placable = true;
		EventSystem = GameObject.Find ("EventSystem");
		World = GameObject.Find ("WORLD");
		transform.parent = World.transform;
		transform.LookAt (World.transform);
		GDM = GameObject.Find ("GameManager").GetComponent<GameDataManager>();
		Alarm = transform.FindChild ("Alarm").transform.gameObject;
		LerpTo = transform.FindChild ("LerpCameraTo").transform.gameObject;
		Alarm.SetActive(false);
		errorText = GameObject.Find ("ErrorText");
		errorText.GetComponent<Text>().text = "";
		transform.FindChild("anten").gameObject.GetComponent<Renderer> ().material.color = Color.green;
		LM = GameObject.Find ("DontDestroy").GetComponent<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!placed) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			transform.LookAt (World.transform);
			if (Physics.Raycast (ray, out hit, 100, mask.value)) {
				transform.position = hit.point;
				if (placable && Input.GetButtonDown ("Fire1") && GDM.Credit >= creditValue) {
					placed = true;
					GDM.Credit -= creditValue;
					GDM.RelayCount++;
					GDM.Relays.Add (gameObject);
					GDM.UpdateCredit ();
					AS.PlayOneShot (AC_Place);
					gameObject.layer = LayerMask.NameToLayer ("RELAY");
					transform.FindChild ("anten").gameObject.GetComponent<Renderer> ().material.color = Color.white;
				} else if (Input.GetButtonDown ("Fire1")) {
					if (!placable)
						errorText.GetComponent<Text> ().text = "CANNOT PLACE HERE!";
					else
						errorText.GetComponent<Text> ().text = "NOT ENOUGH CREDITS!";
					Destroy (gameObject);
				}
			}
		} else {
			speed = GDM.speed;
			time += Time.deltaTime * speed;
			if (time > 60) { //TODO: reduce wait time with time upgrades
				int rand = Random.Range (0, 100);
				Debug.Log (rand);
				if (rand < 30 + GDM.RelayCount && !Alarm.activeInHierarchy) //TODO: Increase multiplier by Relay Efficiency upgrades
					IncomingMessage ();
				time = 0;
			}
		}
	}

	void OnMouseDown(){
		AS.Stop ();
		EventSystem = GameObject.Find ("EventSystem");
		//Enable relay mission menu!
		GDM.messageBits.TryGetValue (LM.input_int, out inc_msg);
		if (Alarm.activeInHierarchy && inc_msg != null) {
			EventSystem.GetComponent<MenuButtons> ().SetMission ();
			EventSystem.GetComponent<MenuButtons> ().Relay = gameObject;
			Camera.main.GetComponent<CameraCode> ().LerpPos = LerpTo;
			Camera.main.GetComponent<CameraCode> ().LookAt = gameObject;
		} else
			transform.GetChild (0).gameObject.SetActive (false);	
	}

	public void IncomingMessage(){
		
		AS.clip = AC_Transmission;
		AS.Play ();
		//select a random piece from the complete string.
		int rand = Random.Range (0, GDM.messageBits.Count + 1);
		GDM.messageBits.TryGetValue (rand, out inc_msg);
		if (inc_msg != null) {
			Alarm.SetActive (true);
			GDM.speed = 1;
			LM.input_int = rand;
			LM.input = inc_msg;
		}
		//never get same value ever again!
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.name != "WORLD") {
			placable = false;
			if(!placed)
				transform.FindChild("anten").gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.name != "WORLD") {
			placable = true;
			if(!placed)
				transform.FindChild("anten").gameObject.GetComponent<Renderer> ().material.color = Color.green;
		}
	}
}
