using UnityEngine;
using System.Collections;
using UnityEngine.UI;


// attached to LOCAL CONTROL (feature screen)
public class FeatureControl : MonoBehaviour {

	public GameObject arch;
	public GameObject ech;
	public GameObject calc;
	public GameObject con;
	public GameObject tall;

	
	private int archi;
	private int echo;
	private int calci;
	private int cont;
	private int taller;

	//choice number of each feature
	private int archNumber;
	private int echoNumber;
	private int calciNumber;
	private int contNumber;
	private int tallerNumber;

	private Color original;


	bool archiChanged = false;
	bool echoChanged = false;
	bool calciChanged= false;
	bool contChanged= false;
	bool tallerChanged= false;


	void Start(){
		// record original color
		original = arch.transform.GetChild (1).gameObject.GetComponent<Image>().color;

		// if there's previous choices, display them
		// otherwise, the first buttons are first choice
		if (DataManager.Instance.screen3FirstTime) {
			// set the buttons of default choice (first ones) when measurement screen was loaded
			arch.transform.GetChild (0).gameObject.GetComponent<Image> ().color = Color.gray;
			ech.transform.GetChild (0).gameObject.GetComponent<Image> ().color = Color.gray;
			calc.transform.GetChild (0).gameObject.GetComponent<Image> ().color = Color.gray;
			con.transform.GetChild (0).gameObject.GetComponent<Image> ().color = Color.gray;
			tall.transform.GetChild (0).gameObject.GetComponent<Image> ().color = Color.gray;
			// retrieve previous choice 
		} else {
			arch.transform.GetChild (DataManager.Instance.architecture[DataManager.Instance.noduleIndex -1]).gameObject.GetComponent<Image> ().color = Color.gray;
			ech.transform.GetChild (DataManager.Instance.echogenicity[DataManager.Instance.noduleIndex -1]).gameObject.GetComponent<Image> ().color = Color.gray;
			calc.transform.GetChild (DataManager.Instance.calcification[DataManager.Instance.noduleIndex -1]).gameObject.GetComponent<Image> ().color = Color.gray;
			con.transform.GetChild (DataManager.Instance.contour[DataManager.Instance.noduleIndex -1]).gameObject.GetComponent<Image> ().color = Color.gray;
			tall.transform.GetChild (DataManager.Instance.tallerThanWider[DataManager.Instance.noduleIndex -1]).gameObject.GetComponent<Image> ().color = Color.gray;
			
		}

		// record choice number of each feature
		archNumber = arch.transform.childCount;
		echoNumber = ech.transform.childCount;
		calciNumber = calc.transform.childCount;
		contNumber = con.transform.childCount;
		tallerNumber = tall.transform.childCount;;

	}

	// called by buttons to send in feature choices, through script FeatureButton's OnPointerClick()
	public void SaveArchitecture(int archi){
		this.archi = archi;
		// pressed button will automatically change color
		// change other button to original color here
		for (int i = 0; i < archNumber; i++) {
			if (i != archi) {
				arch.transform.GetChild (i).gameObject.GetComponent<Image> ().color = original;
			}
		}
		archiChanged = true;
	}
	public void SaveEchogenicity(int echo){
		this.echo = echo;
		for (int i = 0; i < echoNumber; i++) {
			if (i != echo) {
				ech.transform.GetChild (i).gameObject.GetComponent<Image> ().color = original;
			}
		}
		echoChanged = true;
	}
	public void SaveCalcifications(int calci){
		this.calci = calci;
		for (int i = 0; i < calciNumber; i++) {
			if (i != calci) {
				calc.transform.GetChild (i).gameObject.GetComponent<Image> ().color = original;
			}
		}
		calciChanged = true;
	}
	public void SaveContour(int cont){
		this.cont = cont;
		for (int i = 0; i < contNumber; i++) {
			if (i != cont) {
				con.transform.GetChild (i).gameObject.GetComponent<Image> ().color = original;
			}
		}
		contChanged = true;
	}
	public void SaveIfTaller(int taller){
		this.taller = taller;
		Debug.Log ("the saved choice is: " + taller);
		for (int i = 0; i < tallerNumber; i++) {
			if (i != taller) {
				tall.transform.GetChild (i).gameObject.GetComponent<Image> ().color = original;
			}
		}
		tallerChanged = true;
	}

	// called by "next" button on feature screen, through OnClick() in inspector
	public void SaveNoduleFeature(){

		// if some choices are not changed, than read previous choice
		if (!archiChanged){
			archi = DataManager.Instance.architecture [DataManager.Instance.noduleIndex - 1];
		}
		if (!echoChanged) {
			echo = DataManager.Instance.echogenicity [DataManager.Instance.noduleIndex - 1];
		}
		if (!calciChanged) {
			calci = DataManager.Instance.calcification [DataManager.Instance.noduleIndex - 1];
		}
		if (!contChanged) {
			cont = DataManager.Instance.contour [DataManager.Instance.noduleIndex - 1];
		}
		if (!tallerChanged) {
			taller = DataManager.Instance.tallerThanWider [DataManager.Instance.noduleIndex - 1];
		}

		DataManager.Instance.SaveNoduleFeature (archi, echo, calci, cont, taller);
	}

}
