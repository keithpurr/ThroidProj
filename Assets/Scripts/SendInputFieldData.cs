using UnityEngine;
using System.Collections;

public class SendInputFieldData : MonoBehaviour {

	// called by on end edit of input field
	public void SendNumberToManager(string number){
		// otherwise the instance will always be the first one, any new ones would be destroyed
		DataManager.Instance.SaveNoduleNumber (number);
	}
}
