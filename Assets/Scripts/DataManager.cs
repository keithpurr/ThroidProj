using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

// handles app logic  -- GLOBAL CONTROL
// unfortunately, as the only singleton game controller, handles both data and display 
public class DataManager : MonoBehaviour {

	// manages data through screen1 - 4
	//public GameObject locationTracker;

//	public GameObject input;

	GameObject markers;

	public bool screen1FirstTime = true;
	public bool screen2FirstTime = true;
	public bool screen3FirstTime = true;

	public Vector2[] noduleLocation;

	public string[] ap;
	public string[] transverse;
	public string[] longitudinal;

	public int[] architecture;
	public int[] echogenicity;
	public int[] calcification;
	public int[] contour;

	public int[] tallerThanWider;

	public int[] score;
	private GameObject location; 
	public Color[] colorCode;
	public float[] volume;
//	private string[] tiradsScore;

	public int noduleNumber = 1;
	// use for track screen measurement & feature for SceneLoader
	public int noduleIndex = 1;

	private Color color;


	private int arc, echo, calci, cont, taller;

	public int scene2LoadCount;
	public int scene3LoadCount;

	private GameObject instruction;



	// Static singleton property
	public static DataManager Instance { get; private set; }

	void Awake() {
		// First we check if there are any other instances conflicting
		if(Instance != null && Instance != this)
		{
			// If that is the case, we destroy other instances
			Destroy(gameObject);
			//Destroy(Instance);
		}

		// Here we save our singleton instance
		Instance = this;

		DontDestroyOnLoad(transform.gameObject);
	}
		

//	// When Screen4 is loaded, calculate and color code the Thyroid
//	void OnLevelWasLoaded(int level){
//		if (level == 3) {
//			Debug.Log ("Screen4 is reached");
//			ColorCodeThyroid ();
//			DisplayVolumeAndScore ();
//		} 
//	}

	// DISPLAY
	// for logic for a scene is loaded via "next"/ "back" button
	void OnLevelWasLoaded(int level){
		if (level == 1) {
			// place marker
			GameObject markerAdder = GameObject.Find ("Local Control");
			Debug.Log ("when level 0 was loaded the nodule number is :" + noduleNumber);
			markerAdder.GetComponent<MarkerAdder> ().PlaceMarker (noduleNumber);
			screen1FirstTime = false;
		}
		if (level == 2) {
			Debug.Log ("Screen2 loded: " + scene2LoadCount + "times");
			// get reference to instruction text and change it.
			instruction = GameObject.Find ("Instruction");
			//instruction.GetComponent<Text> ().text = "feature - nodule " + scene2LoadCount.ToString ();
			instruction.GetComponent<Text> ().text = "feature - nodule " + noduleIndex.ToString ();
			screen2FirstTime = false;

		} else if (level == 3) {
			// get reference to instruction text and change it.
			instruction = GameObject.Find ("Instruction");
			instruction.GetComponent<Text> ().text = "feature - nodule " + noduleIndex.ToString ();
			screen3FirstTime = false;
		}
	}
		



	// called on the change of value on the input field on first screen, instantiate all arrays
	public void SaveNoduleNumber(string number){

//		string number = input.transform.GetChild (1).GetComponent<Text> ().text;
//		Debug.Log (number);

		noduleNumber = int.Parse(number); 
		Debug.Log ("the noduleNumber is:" + noduleNumber);

		noduleLocation = new Vector2[noduleNumber];
		ap = new string[noduleNumber];
		transverse = new string[noduleNumber];
		longitudinal = new string[noduleNumber];

		architecture = new int[noduleNumber];
		echogenicity = new int[noduleNumber];
		calcification = new int[noduleNumber];
		contour = new int[noduleNumber];
		tallerThanWider = new int[noduleNumber];

		score = new int[noduleNumber];
		colorCode = new Color[noduleNumber];
		volume = new float[noduleNumber];

		Debug.Log ("all arrays are instantiated w/ moduleNumber: " + noduleNumber);
//		tiradsScore = new string[noduleNumber];
	}


	// called by "next" buntton on drag/drop location screen to save locations of nodule
	public void SaveNoduleLocation(){
		markers = GameObject.Find ("Markers");
		//markers.transform.Find ("Marker").gameObject;

		Debug.Log ("noduleNumber = " + noduleNumber);
//		Debug.Log (noduleLocation.GetLength);
		for (int i = 0; i < noduleNumber; i++) {
			Transform marker = markers.transform.GetChild (i);
 			noduleLocation [i] = marker.localPosition;
			Debug.Log ("The position is" + noduleLocation [i]);
		}
	}

	// called by "next" button on measurement screen, through InputMenu's SaveNoduleMeasurement() function
	public void SaveNoduleMeasurement(string ap, string transverse, string longitudinal){
		
		this.ap[noduleIndex - 1] = ap;
		this.transverse[noduleIndex - 1] = transverse;
		this.longitudinal[noduleIndex - 1] = longitudinal;

		Debug.Log ("index is:" + (noduleIndex - 1) + "the measures are" + ap + transverse + longitudinal);
		
	}

	// called by "next" button on feature screen, through FeatureButton's SaveNoduleFeature() function 
	public void SaveNoduleFeature(int archi, int echo, int calci, int cont, int taller){
		architecture [noduleIndex - 1] = archi;
		echogenicity [noduleIndex - 1] = echo;
		calcification [noduleIndex - 1] = calci;
		contour [noduleIndex - 1] = cont;
		tallerThanWider [noduleIndex - 1] = taller;

		Debug.Log ("index is:" + (noduleIndex - 1) + "features are" +archi + echo+calci+cont+taller);
	}


	public void CalculateScore(){
		for (int i = 0; i < noduleNumber; i++) {
			arc = Convert.ToInt32 (architecture [i] == 0);
			echo = Convert.ToInt32 (echogenicity [i] == 0);
			calci = Convert.ToInt32 (calcification [i] == 0);
			cont = Convert.ToInt32 (contour [i] == 0);
			taller = Convert.ToInt32 (tallerThanWider [i] == 0);

			score [i] = arc + echo + calci + cont + taller;
			colorCode [i] = CalculateTRIADSScoreAndColorCode (score [i]);
				
		}
	}

//	void ColorCodeThyroid(){
//
//		CalculateScore ();
//		CalculateTRIADSScoreAndColorCode ();
//
//		location = GameObject.Find (noduleLocation);
//
//		location.GetComponent<SpriteRenderer> ().material.color = colorCode;
//
//		Debug.Log ("the " + location + " is paited " + colorCode);
//	}

	public Color CalculateTRIADSScoreAndColorCode(int score){
		switch (score) 
		{
		case 0:
			return Color.white;
		case 1:
			return Color.green;
		case 2:
			return Color.blue;
		case 3:
			return Color.yellow;
		case 4:
			return Color.yellow;
		case 5:
			return Color.red;
		}
		return Color.gray;
	}
		

	public void CalculateVolume(){
		for (int i = 0; i < noduleNumber; i++) {
			volume[i] = float.Parse (ap[i]) * float.Parse (transverse[i]) * float.Parse (longitudinal[i]) * (float)Math.PI / 6;
			volume[i] = Mathf.Round (volume[i] * 100f) / 100f;

		}
	}

	// called by sceneLoader, when about to load the first screen
	public void DestroyOldManager(){
		Destroy (gameObject);
	}


	public void Save(){
		// in real case 1. load existing data and merge with new data 2. save
		// or save to a list of files reportInfo1.dat, reportInfo2.dat...
		// create binary formatter, a file
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/reportInfo.dat");
		// better to use constructor
		// create a data container 
		// fill in data
		ReportInfo data = new ReportInfo (); 
		data.number = noduleNumber;

		// intialization
		data.noduleLocation = new Vector2[noduleNumber];
		data.ap = new string[noduleNumber];
		data.transverse = new string[noduleNumber];
		data.longitudinal = new string[noduleNumber];

		data.architecture = new int[noduleNumber];
		data.echogenicity = new int[noduleNumber];
		data.calcification = new int[noduleNumber];
		data.contour = new int[noduleNumber];
		data.tallerThanWider = new int[noduleNumber];

		data.score = new int[noduleNumber];
		data.colorCode = new Color[noduleNumber];
		data.volume = new float[noduleNumber];

		// copy arrays to container
		Array.Copy(data.noduleLocation,this.noduleLocation, noduleNumber);
		Array.Copy(data.ap,this.ap, noduleNumber);
		Array.Copy(data.transverse,this.transverse, noduleNumber);
		Array.Copy(data.longitudinal,this.longitudinal, noduleNumber);

		Array.Copy(data.architecture,this.architecture, noduleNumber);
		Array.Copy(data.echogenicity,this.echogenicity, noduleNumber);
		Array.Copy(data.calcification,this.calcification, noduleNumber);
		Array.Copy(data.contour,this.contour, noduleNumber);
		Array.Copy(data.tallerThanWider,this.tallerThanWider, noduleNumber);

		Array.Copy(data.score,this.score, noduleNumber);
		Array.Copy(data.colorCode,this.colorCode, noduleNumber);
		Array.Copy(data.volume,this.volume, noduleNumber);

		// write to file and close
		bf.Serialize (file, data);
		file.Close();

		Debug.Log ("data saved to: " + Application.persistentDataPath + "/reportInfo.dat");
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/reportInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/reportInfo.dat",FileMode.Open);

			// cast the deserialized generic object to reportInfo data
			ReportInfo data = (ReportInfo)bf.Deserialize(file);
			file.Close ();

			this.noduleNumber = data.number;

			// initialization
			this.noduleLocation = new Vector2[noduleNumber];
			this.ap = new string[noduleNumber];
			this.transverse = new string[noduleNumber];
			this.longitudinal = new string[noduleNumber];

			this.architecture = new int[noduleNumber];
			this.echogenicity = new int[noduleNumber];
			this.calcification = new int[noduleNumber];
			this.contour = new int[noduleNumber];
			this.tallerThanWider = new int[noduleNumber];

			this.score = new int[noduleNumber];
			this.colorCode = new Color[noduleNumber];
			this.volume = new float[noduleNumber];

			Array.Copy(this.noduleLocation,data.noduleLocation,data.number);
			Array.Copy(this.ap,data.ap,data.number);
			Array.Copy(this.transverse,data.transverse,data.number);
			Array.Copy(this.longitudinal,data.longitudinal,data.number);

			Array.Copy(this.architecture,data.architecture,data.number);
			Array.Copy(this.echogenicity,data.echogenicity,data.number);
			Array.Copy(this.calcification,data.calcification,data.number);
			Array.Copy(this.contour,data.contour,data.number);
			Array.Copy(this.tallerThanWider,data.tallerThanWider,data.number);

			Array.Copy(this.score,data.score,data.number);
			Array.Copy(this.colorCode,data.colorCode,data.number);
			Array.Copy(this.volume,data.volume,data.number);

			SceneManager.LoadScene (3);
			 
		}
	}
}


// a data container that allows to write to a file
// when this class is instantiated and put data to, it's going to be serialized
[Serializable]
class ReportInfo
{
	public int number;
	public Vector2[] noduleLocation;

	public string[] ap;
	public string[] transverse;
	public string[] longitudinal;

	public int[] architecture;
	public int[] echogenicity;
	public int[] calcification;
	public int[] contour;
	public int[] tallerThanWider;

	public int[] score;
	public Color[] colorCode;
	public float[] volume;



}
