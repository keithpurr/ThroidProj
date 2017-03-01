using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// for instantating marker, called upon the change of inputField "Enter nodule number"
// use this script to get to SavaLocation Meathod in DataManager
public class MarkerAdder : MonoBehaviour{

	public  GameObject marker;

	private GameObject newMarker;

	private GameObject text;

	public GameObject markers;

	// called by the DataManager, when screen drag/ drop is loaded
	public void PlaceMarker(int number){
		
		for (int i = 0; i < number; i++) {

			// place marker
			//newMarker = Instantiate (marker, new Vector3 (Random.Range(-70.0F, 70.0F), Random.Range(-50.0F, 50.0F) + 115, 0), Quaternion.identity) as GameObject;

			// if the screen1 is loaded for the first time, instantiate marker along a vertical line,
			// otherwise load from noduleLocation
			if (DataManager.Instance.screen1FirstTime == true) {
				// put marker along a vertical line
				newMarker = Instantiate (marker, new Vector3 (0, 200 - i * 50, 0), Quaternion.identity) as GameObject;
			} else {
				newMarker = Instantiate (marker, new Vector3 (DataManager.Instance.noduleLocation[i].x, DataManager.Instance.noduleLocation[i].y, 0), Quaternion.identity) as GameObject;
			}

			newMarker.transform.SetParent (markers.transform, false);

			text = newMarker.transform.FindChild("Text").gameObject;

			text.GetComponent<Text> ().text = (i + 1).ToString();

			Debug.Log ("Marker placed: " + text.GetComponent<Text> ().text);
			
		}
		
	}

	// called by "next" button on drag/drop screen
	public void CallSaveLocation(){
		DataManager.Instance.SaveNoduleLocation ();
	}

}
