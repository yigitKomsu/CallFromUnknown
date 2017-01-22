using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Encoder_Decoder : MonoBehaviour {
	char[] Character_Set = new char[64]; //this is th set of all characters in the game
	List<char> Character_List = new List<char>();
	public Dictionary<int, char> Character_Map = new Dictionary<int, char>(); //this is the map that always changes with each new message
	DataNodeManager DNM;
	GameDataManager GDM;
	WorldControls WC;
	public int[] message; //comes as numbers and dividers 61 5 21 ....
	public string input;
	public int input_int;
	public string output;
	public Text outputMessage;
	public Text TimeRemains;
	int randIndex, i, j;
	bool called_this;
	public float time;
	LevelManager LM;
	AudioSource AS;
	// Use this for initialization
	void Start () {
		#region I HATE THIS
		Character_Set [0] = 'A';	Character_Set [1] = 'B';	Character_Set [2] = 'C';	Character_Set [3] = 'D';	Character_Set [4] = 'E';
		Character_Set [5] = 'F';	Character_Set [6] = 'G';	Character_Set [7] = 'H';	Character_Set [8] = 'I';	Character_Set [9] = 'J';
		Character_Set [10] = 'K';	Character_Set [11] = 'L';	Character_Set [12] = 'M';	Character_Set [13] = 'N';	Character_Set [14] = 'O';
		Character_Set [15] = 'P';	Character_Set [16] = 'Q';	Character_Set [17] = 'R';	Character_Set [18] = 'S';	Character_Set [19] = 'T';
		Character_Set [20] = 'U';	Character_Set [21] = 'V';	Character_Set [22] = 'W';	Character_Set [23] = 'X';	Character_Set [24] = 'Y';
		Character_Set [25] = 'Z';	//END OF ALPHABET!!

		Character_Set [26] = '[';	Character_Set [27] = '/';	Character_Set [28] = ']';	Character_Set [29] = '^';	Character_Set [30] = '_';
		Character_Set [31] = '!';	Character_Set [32] = '#';	Character_Set [33] = '$';	Character_Set [34] = '%';	Character_Set [35] = '&';
		Character_Set [36] = '½';	Character_Set [37] = '(';	Character_Set [38] = ')';	Character_Set [39] = '*';	Character_Set [40] = '+';
		Character_Set [41] = ',';	Character_Set [42] = ':';	Character_Set [43] = ';';	Character_Set [44] = '<';	Character_Set [45] = '>';
		Character_Set [46] = '=';	Character_Set [47] = '?';	Character_Set [48] = '@';	Character_Set [49] = '{';	Character_Set [50] = '}';
		Character_Set [51] = '|';	Character_Set [52] = '-';	Character_Set [53] = '~'; //END OF CHARACTERS!!

		Character_Set[54] = '0';	Character_Set[55] = '1';	Character_Set[56] = '2';	Character_Set[57] = '3';	Character_Set[58] = '4';
		Character_Set[59] = '5';	Character_Set[60] = '6';	Character_Set[61] = '7';	Character_Set[62] = '8';	Character_Set[63] = '9'; //END OF NUMBERS!!
		#endregion
		DNM = GetComponent<DataNodeManager> ();
		GDM = GameObject.FindObjectOfType<GameDataManager> ().GetComponent<GameDataManager> ();;
		WC = GameObject.FindObjectOfType<WorldControls> ().GetComponent<WorldControls> ();;
		WC.gameObject.SetActive (false);
		AS = GetComponent<AudioSource> ();
		//change input from gamedatamanager
		for (int i = 0; i < 64; i++) {
			Character_List.Add (Character_Set [i]);
		}
		for (int j = 0; j < 64; j++) {//do this 64 times
			randIndex = Random.Range(0, Character_List.Count);
			Encode (randIndex, j);
		}

		LM = GameObject.Find ("DontDestroy").GetComponent<LevelManager> ();
		input = LM.input;
		input_int = LM.input_int;
		outputMessage.text = input;
		time = 60; //TODO: pull time from LevelManager, increase time with time upgrades, decrease time with count of remaining string bits
	}

	void Update(){
		time -= Time.deltaTime;
		TimeRemains.text = "TIME: " + time.ToString("####");
		if (time <= 0)
			PushOutput(); //send whatever the output is to LevelManager
	}

	void Encode(int index, int mapIndex){
		Character_Map.Add (mapIndex, Character_List [index]);
		DNM.inverted.Add (Character_List [index], mapIndex);
		Character_List.RemoveAt (index);
	}

	public void PrintMessage(bool called){
		if (!called_this) {
			called_this = called = true;
			output = "";
		}
		if (i < 8) {
			message [i] = GetComponent<NodeDataManager> ().values [i];
			i++;
		} else {
			i = 0;
			GetComponent<NodeDataManager> ().time = 1;
		}
		Decode ();
		outputMessage.text = output;
		if(output.Length == 8)
			GetComponent<NodeDataManager> ().time = 0;
	}

	public void PushOutput(){
		LM.decoded = output;
		char[] arr = GDM.displayMessage.ToCharArray ();
		for(int i = 0; i < GDM.messageBits[input_int].Length; i++){
			arr [input_int * 8 + i] = output[i];
		}
		GDM.displayMessage = new string (arr);
		for(int k = 0; k < output.Length; k++){
			if (LM.decoded [k] == LM.input [k]) {
				GDM.Credit += 10;
				GDM.totalGainedCredit += 10;
			}
		}
		GDM.Credit += 10;
		GDM.totalGainedCredit += 10;
		WC.gameObject.SetActive (true);
		GDM.gameObject.SetActive (true);
		for (int i = 0; i < WC.transform.childCount; i++) {
			WC.transform.GetChild(i).gameObject.SetActive (true);
			for (int j = 1; j < WC.transform.GetChild(i).childCount; j++) {
				WC.transform.GetChild(i).GetChild(j).gameObject.SetActive (true);
			}
		}
		LM.gameObject.SetActive (true);
		GDM.GetComponent<GameDataManager> ().messageCount--;
		GDM.messageBits.Remove (input_int);
		GDM.GetComponent<GameDataManager> ().UpdateCredit ();
		AS.Play ();
		SceneManager.LoadScene ("WorldScene");
	}

	void Decode(){
		char temp;
		if (j < message.Length) {
			Character_Map.TryGetValue (message [j], out temp);
			output += temp;
			j++;
		} else {
			j = 0;
			GetComponent<NodeDataManager> ().time = 0;
			called_this = false;
		}
	}
}
