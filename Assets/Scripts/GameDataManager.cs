using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDataManager : MonoBehaviour {
	public int Credit, totalGainedCredit, maxCredit;
	public int RelayCount;
	public int messageCount, firstContactYear, errorMargin, daysPassed;
	public List<GameObject> Relays = new List<GameObject> ();
	public GameObject MoneyText;
	public GameObject DateText;
	public Text EndGameText;
	public Dictionary<int, string> messageBits = new Dictionary<int, string> (); //this is where I divide total message into random pieces of strings
	public string totalMessage; //Source to create messageBits from
	public string displayMessage; //All chars are a non-used ASCII character at first
	public int Day, Month, Year;
	public float gameTime, speed;
	// Use this for initialization
	void Start () {
		speed = 1;
		MoneyText = GameObject.Find ("MoneyText");
		DateText = GameObject.Find ("DateText");
		totalMessage = "THIS_IS_AN_AUTOMATED_MESSAGE._PRIMITIVES_WHO_ARE_RECEIVING_THIS_MESSAGE,_TAKE_HEED._IF_YOUR_WORLD_TOO_IS_IN_SYNC_WITH_THIS_WAVELENGTH,_YOU_MUST_PREPARE_TO_LEAVE_YOUR_" +
			"PLANET._THIS_IS_NOT_AN_ULTIMATOM._OUR_PLANET_WAS_HIT_WITH_THE_SAME_WAVE_BUT_SLOWER,_THIS_IS_WHY_WE_CAN_MAKE_CONTACT_BEFORE_THE_CATASTROPHY._AN_ANCIENT_SYNTHETIC_RACE_HAS_ACTIVATED_A" +
			"_SUPER_WEAPON_$^&½13*/_YEARS_AGO._OUR_CALCULATIONS_SUGGEST_IN_FIFTY_(50)_YEARS_AFTER_YOU_RECEIVE_YOUR_FIRST_TRANSMISSION_YOUR_WORLD_WILL_BE_HIT_TOO." +
			"_THIS_IS_MESSAGE_HAS_STOPPED_UPDATING_CRITICAL_DATA._ANY_TIME_INFORMATION_MAY_BE_RELATIVE._END_OF_MES~~~~";
		//??? displayMessage same length with totalMessage
		for(int i = 0; i < totalMessage.Length; i++){
			displayMessage += "#";
		}
		//divide totalMessage into bits of 8
		Divisor();
		maxCredit = (totalMessage.Length + messageBits.Count) * 10;
		DontDestroyOnLoad (gameObject);
		Day = 11;
		Month = 02;
		Year = 2023;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find("EndGameText") != null)
			EndGameText = GameObject.Find ("EndGameText").GetComponent<Text> ();
		if(messageCount == totalMessage.Length / 8 - 1) //first contact!
			firstContactYear = Year;
		MoneyText = GameObject.Find ("MoneyText");
		DateText = GameObject.Find ("DateText");
		if (Year - firstContactYear >= 50 + errorMargin && firstContactYear != 0) {
			Destroy (GameObject.Find ("WORLD"));
			EndGameText.text = "GAME OVER \n YOU WERE TOO LATE \n FIRST MESSAGE: " + firstContactYear;
		}

		if (messageCount < 0) {
			EndGameText.text = "GAME OVER \n YOU GAVE THE ONLY CHANCE \n HUMANITY NEEDED TO SURVIVE \n DAYS TO SOLVE THE MESSAGE: " + daysPassed +
				"\n FIRST MESSAGE: " + firstContactYear + "\n MESSAGE COMPLETENESS: " + totalGainedCredit / maxCredit * 100 + "%";
		}
	}

	public void dateUpDate(){
		gameTime += Time.deltaTime * speed;
		if (gameTime > 4) {
			Day++;
			daysPassed++;
			gameTime = 0;
		}
		if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 9 || Month == 11) {
			if (Day == 31) {
				Day = 1;
				Month++;
			}
		} else if (Month == 2) {
			if (Year % 4 == 0 && Day == 29) {
				Day = 1;
				Month++;
			} else if (Day == 28) {
				Day = 1;
				Month++;
			}
		} else if (Month == 4 || Month == 6 || Month == 8 || Month == 10 || Month == 12) {
			if (Day == 30) {
				Day = 1;
				Month++;
			}
		} else if (Month >= 12) {
			Day = 1;
			Month = 1;
			Year++;
		}
		if(DateText != null)
			DateText.GetComponent<Text>().text = Day + " : " + Month + " : " + Year;
	}

	void Divisor(){
		int j = 0;
		messageBits.Add (j, "");
		for(int i = 0; i < totalMessage.Length; i++){
			if (i % 8 == 0 && i != 0) {
				j++;
				messageBits.Add(j, "" + totalMessage[i]);
			} else
				messageBits [j] += totalMessage [i];
		}
		messageCount = j;
	}

	public void UpdateCredit(){
		MoneyText.GetComponent<Text>().text = "CREDIT: " + Credit.ToString ();
	}
}
