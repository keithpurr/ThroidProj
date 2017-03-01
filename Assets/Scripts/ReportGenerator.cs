using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

// for report/summary screen
// *can be put into DataManager OnLevelLoaded too.
public class ReportGenerator : MonoBehaviour {

	public int number;

	public GameObject marker;

	public GameObject markers;

	private Vector2[] position;

	private GameObject[] insMarker;


	public GameObject canvas;

	private Color color;

	public GameObject text;

	public GameObject[] insText;

	public GameObject report;

	public Transform markerText;


	public string[] translateFeatureArc;
	public string[] translateFeatureEcho;
	public string[] translateFeatureCalci;
	public string[] translateFeatureCont;
	public string[] translateFeatureTall;
	// Use this for initialization
	void Start () {
		
		number = DataManager.Instance.noduleNumber;

		Debug.Log ("number retrieved:" + number);

		// for debug only
		//number = 2;

		//text = new GameObject[4];

		position = new Vector2[number];
		Array.Copy (DataManager.Instance.noduleLocation, position, number);

		insMarker = new GameObject[number];

		insText = new GameObject[number];

		translateFeatureArc = new string[number];
		translateFeatureEcho= new string[number];
		translateFeatureCalci= new string[number];
		translateFeatureCont= new string[number];
		translateFeatureTall= new string[number];


		//position = DataManager.Instance.noduleLocation;
		// calculate score and color
		DataManager.Instance.CalculateScore ();
		// calculate volumn
		DataManager.Instance.CalculateVolume();

		// convert feature from int to string
		ConvertIntToString();

		// instantiate marker
		for (int i = 0; i < number; i++) {

			insMarker[i] = Instantiate (marker, new Vector3 (position[i].x, position[i].y, 0), Quaternion.identity) as GameObject;

			insMarker[i].transform.SetParent (canvas.transform, false);

			Debug.Log ("the position of marker is: " + position[i]);

			GameObject text = insMarker[i].transform.FindChild("Text").gameObject;

			text.GetComponent<Text> ().text = (i + 1).ToString();

			insMarker [i].GetComponent<Image> ().color = DataManager.Instance.colorCode[i];

			if (insMarker [i].GetComponent<Image> ().color == Color.white) {
				markerText = insMarker [i].transform.GetChild (0);
				markerText.gameObject.GetComponent<Text> ().color = Color.black;
			}

			// display "report"
			if(i < 3) 
				insText[i] = Instantiate (text, new Vector3 (-450, 250 - i * 200, 0), Quaternion.identity)as GameObject;
			else
				insText[i] = Instantiate (text, new Vector3 (140, 250 - (i - 3) * 200, 0), Quaternion.identity)as GameObject;

			insText [i].transform.SetParent (report.transform, false);

			insText [i].GetComponent<Text> ().fontStyle = FontStyle.Normal;

			insText [i].GetComponent<Text> ().fontSize = 20;

			insText [i].GetComponent<Text> ().color = Color.black;

			insText [i].GetComponent<Text> ().alignment = TextAnchor.UpperLeft;

			insText [i].GetComponent<Text> ().text = "nodule " + (i + 1) + ":\n" + "volume: " + DataManager.Instance.volume [i] + "ml\n" +
//			"architecture: " + DataManager.Instance.architecture [i] + "\n" +
//			"echogenicity: " + DataManager.Instance.echogenicity [i] + "\n" +
//			"calcification: " + DataManager.Instance.calcification [i] + "\n" +
//			"contour: " + DataManager.Instance.contour [i] + "\n" +
//			"Taller than wide: " + DataManager.Instance.tallerThanWider [i];
				"architecture: " + translateFeatureArc [i] + "\n" +
				"echogenicity: " + translateFeatureEcho [i] + "\n" +
				"calcification: " + translateFeatureCalci [i] + "\n" +
				"contour: " + translateFeatureCont [i] + "\n" +
				"Taller than wide: " + translateFeatureTall [i];


		}
	
	
	}

	void ConvertIntToString(){
		int index = 0;
		foreach (int element in DataManager.Instance.architecture) {
			if (element == 0)
				translateFeatureArc [index] = "Solid*";
			else if (element == 1)
				translateFeatureArc [index] = "Cystic";
			else if (element == 2)
				translateFeatureArc [index] = "Mixed";
			index++;
			
		}

		index = 0;
		foreach (int element in DataManager.Instance.echogenicity) {
			if (element == 0)
				translateFeatureEcho[index]= "Hypoechoic*";
			else if (element == 1)
				translateFeatureEcho[index]= "Isoechoic";
			else if (element == 2)
				translateFeatureEcho[index]="Hyperechoic";
			index++;
		}

		index = 0;
		foreach (int element in DataManager.Instance.calcification) {
			if (element == 0)
				translateFeatureCalci[index]= "Microcalcification*";
			else if (element == 1)
				translateFeatureCalci[index]= "Macrocalcification";
			index++;
		}

		index = 0;
		foreach (int element in DataManager.Instance.contour) {
			if (element == 0)
				translateFeatureCont[index]= "Microlobulated or irregular margins*";
			else if (element == 1)
				translateFeatureCont[index]= "Completely smoothed and well-defined";
			else if (element == 2)
				translateFeatureCont[index]="Indistinct";
			else if (element == 3)
				translateFeatureCont[index]="Infiltrative";
			index++;
		}

		index = 0;
		foreach (int element in DataManager.Instance.tallerThanWider) {
			if (element == 0)
				translateFeatureTall[index]= "Yes*";
			else if (element == 1)
				translateFeatureTall[index]= "No";
			index++;
		}
	}

	// called by "Save Report" button on summary screen
	public void CallSave(){
		DataManager.Instance.Save();
	}
		

}
