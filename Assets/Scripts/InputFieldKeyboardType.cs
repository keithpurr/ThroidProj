using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputFieldKeyboardType : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		if (gameObject.name != "First name" && gameObject.name != "Last name") {
			
			gameObject.GetComponent<InputField> ().keyboardType = TouchScreenKeyboardType.NumberPad;
//
//		} else {
//			gameObject.GetComponent<InputField> ().keyboardType = TouchScreenKeyboardType.NamePhonePad;
//		} 
	}

}
