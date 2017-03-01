using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// for feature screen
// attached to feature bunttons  -- send choice to LOCAL CONTROL (feature screen)
// save features architecture, echogenicity, calcification and contour to DataManager
public class FeatureButton : MonoBehaviour, IPointerClickHandler {

	public GameObject LocalControl;

//	private int archi;
//	private int echo;
//	private int calci;
//	private int cont;
//	private int taller;
	int choice = -1;

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		// get text on button
		Transform child = transform.GetChild(0);
		string text = child.gameObject.GetComponent<Text> ().text;

		// see whitch group the button belongs to, and send choice to local control 
		if (transform.parent.name == "Architecture") {
			if (text == "Solid*")
				choice = 0;
			else if (text == "Cystic")
				choice = 1;
			else if (text == "Mixed")
				choice = 2;

			LocalControl.GetComponent<FeatureControl> ().SaveArchitecture (choice);
		
		} else if (transform.parent.name == "Echogenicity") {
			if (text == "Hypoechoic*")
				choice = 0;
			else if (text == "Isoechoic")
				choice = 1;
			else if (text == "Hyperechoic")
				choice = 2;
			
			LocalControl.GetComponent<FeatureControl> ().SaveEchogenicity (choice);
		
		} else if (transform.parent.name == "Calcifications") {
			if (text == "Microcalcification*")
				choice = 0;
			else if (text == "Macrocalcification")
				choice = 1;
			LocalControl.GetComponent<FeatureControl> ().SaveCalcifications (choice);
		
		} else if (transform.parent.name == "Contour") {
			if (text == "Microlobulated or irregular margins*")
				choice = 0;
			else if (text == "Completely smoothed and well-defined")
				choice = 1;
			else if (text == "Indistinct")
				choice = 2;
			else if (text == "Infiltrative")
				choice = 3;

			LocalControl.GetComponent<FeatureControl> ().SaveContour (choice);

		} else if (transform.parent.name == "Taller than wide") {
			if (text == "Yes*")
				choice = 0;
			else if (text == "No")
				choice = 1;
			
			LocalControl.GetComponent<FeatureControl> ().SaveIfTaller (choice);

		} else {
			choice = -1;
		}

		gameObject.GetComponent<Image> ().color = Color.gray;

	}
	#endregion


}
