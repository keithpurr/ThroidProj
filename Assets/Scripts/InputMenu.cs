using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


// for measurement screen
// Attached to Local Control (measure screen)
// record 3 dimensions of measurement ap, trans and longi of nodule, and save to DataManager
public class InputMenu : MonoBehaviour {

	string ap;
	string transverse;
	string longitudinal;

	public GameObject title;
	private int index;

	public GameObject ap_;
	public GameObject trans_;
	public GameObject longi_;

	Transform holder_ap;
	Transform holder_trans;
	Transform holder_longi;

	GameObject dataManager;

	bool apChanged = false;
	bool transChanged = false;
	bool longiChanged = false;


	void Start(){
		holder_ap = ap_.transform.GetChild (0);
		holder_trans = trans_.transform.GetChild (0);
		holder_longi = longi_.transform.GetChild (0);

		// if there is already previous choices, then display them in as place holder
		if (!DataManager.Instance.screen2FirstTime){
			holder_ap.GetComponent<Text> ().text = DataManager.Instance.ap [DataManager.Instance.noduleIndex - 1];
			holder_trans.GetComponent<Text> ().text = DataManager.Instance.transverse [DataManager.Instance.noduleIndex - 1];
			holder_longi.GetComponent<Text> ().text = DataManager.Instance.longitudinal [DataManager.Instance.noduleIndex - 1];
		}
	}

	// called by inputFields on Value Changed()
	public void InputAP(string ap){
		this.ap = ap;
		apChanged = true;
	}

	public void InputTrans(string trans){
		transverse = trans;
		transChanged = true;
	}

	public void InputLongit(string longitud){
		longitudinal = longitud;
		longiChanged = true;
	}

	// called by "next" button on measurement screen, through OnClick() in inspector
	public void SaveNoduleMeasurement(){

		// if one of the measurements is not changed, send value of placeholder
		if (!apChanged) {
			this.ap = holder_ap.GetComponent<Text> ().text;
		}
		if (!transChanged) {
			transverse = holder_trans.GetComponent<Text>().text;
		}
		if (!longiChanged) {
			longitudinal = holder_longi.GetComponent<Text> ().text;
		}


		DataManager.Instance.SaveNoduleMeasurement (ap, transverse, longitudinal);

	}
		
		
}
