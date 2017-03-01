using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// attached to the "next" or "start new" button
public class SceneLoader : MonoBehaviour, IPointerClickHandler {

	int presentIndex;

	private int loadCount;

	GameObject numberInput;

	Transform numberText;

	//	private GameObject dataManager;
	//	private Component dataManage;
	//	private int noduleNumber;
	//	private int noduleIndex = 1;
	//	private GameObject instruction;
	//	private string insContent;


	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		presentIndex = SceneManager.GetActiveScene ().buildIndex;

		Debug.Log ("present index is: " + presentIndex);

//		// when press "next" in screen2/3(measurement/feature screen)
//		if (presentIndex == 2 || presentIndex == 3) {
//			if (presentIndex == 2) {
//				loadCount = DataManager.Instance.scene2LoadCount;
//			} else {
//				loadCount = DataManager.Instance.scene3LoadCount;
//			}
//
//			// if screen for all nodules are not yet loaded, load current screen again
//			if(loadCount < DataManager.Instance.noduleNumber){
//				SceneManager.LoadScene (presentIndex);
//			}
//			// if all nodule measurement/feature is filled out, load next screen
//			else 
//				SceneManager.LoadScene (presentIndex + 1);
//		}

		// change to for each nodule, load measurement and featrue one after another:
		if (presentIndex == 2) {
			SceneManager.LoadScene (3);
		} else if (presentIndex == 3) {
			DataManager.Instance.noduleIndex++;
			if (DataManager.Instance.noduleIndex <= DataManager.Instance.noduleNumber) {
				SceneManager.LoadScene (2);
			} else {
				SceneManager.LoadScene (4);
			}

		} else if (presentIndex == 4) {
			if (gameObject.name == "Start New") {
				SceneManager.LoadScene (0);
			} 
//			else if (gameObject.name == "Email Report") {
//				SceneManager.LoadScene (5);
//			}
			DataManager.Instance.DestroyOldManager ();
//			if (gameObject.name == "Move to 3D")
//				SceneManager.LoadScene (4);
//		} else if (presentIndex == 4) {
//			if (gameObject.name == "Return")
//				SceneManager.LoadScene (3);
////			if (gameObject.name == "Quit") {
////				Debug.Log ("Quits the application");
////				Application.Quit ();
		} else if (presentIndex == 0) {
			numberInput = GameObject.Find ("Nodule number");
			numberText = numberInput.transform.GetChild (2);
			// if nodule number has not been input, warn
//			Debug.Log(numberText.gameObject.GetComponent<Text> ().text);
//			Debug.Log(numberText.gameObject.name);
			if (numberText.gameObject.GetComponent<Text> ().text == "") {
				Debug.Log ("number is empty");
				numberInput.GetComponent<Image> ().color = Color.yellow;
			} else {
				SceneManager.LoadScene (1);
			}
		// the start screen
		} else if (presentIndex == 6) {
			if (gameObject.name == "Start")
				SceneManager.LoadScene (0);
		}
		else {
			Debug.Log ("load the +1 screen");
			SceneManager.LoadScene (presentIndex + 1);
		}
	}

	#endregion


	public void FillInInstrution(string title){
		
	}
}

