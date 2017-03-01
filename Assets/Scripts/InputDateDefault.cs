using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputDateDefault : MonoBehaviour {

	Transform placeHolder;

	// Use this for initialization
	void Start () {
		
		placeHolder = gameObject.transform.GetChild (0);
		placeHolder.gameObject.GetComponent<Text> ().text = System.DateTime.Today.ToString ("d");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
