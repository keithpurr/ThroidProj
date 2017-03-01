using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


// attached to "previous" buttons
// do scene loads & calculat nodule indexes
public class PreviousButton : MonoBehaviour, IPointerClickHandler {

	int presentScene;

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		presentScene =  SceneManager.GetActiveScene ().buildIndex;

		if (presentScene == 2 && DataManager.Instance.noduleIndex == 1) {
			SceneManager.LoadScene (1);
		} else if (presentScene == 2) {
			DataManager.Instance.noduleIndex--;
			SceneManager.LoadScene (3);
		} else if (presentScene == 3) {
			SceneManager.LoadScene (2);
		}
//		SceneManager.LoadScene (presentScene - 1);
//
//		if (presentScene == 2 || (presentScene == 3 && DataManager.Instance.screen3FirstTime)){
//			DataManager.Instance.scene2LoadCount--;
//		}
	}
	#endregion


	


}
