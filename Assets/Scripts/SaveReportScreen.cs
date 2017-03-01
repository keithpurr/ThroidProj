using UnityEngine;
using System.Collections;

public class SaveReportScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CaptureScreenshot(){
		// prototype on Mac
		Application.CaptureScreenshot("/Users/Keith/Library/Application Support/Qi Liu/Thyroid/report.png");
		// mobile version
		// can't do save to camera roll withour plug-in
//		Application.CaptureScreenshot("report.png");
		Debug.Log ("report saved to path: " + Application.persistentDataPath + "/report.png");
	}
}
