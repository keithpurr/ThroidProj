using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// for drag&drop of markers
//
public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler{

	private Vector3 startPosition;

	private int index;

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		// disable dragging on report screen.
		if (SceneManager.GetActiveScene ().buildIndex != 4) {
			transform.position = eventData.position;
		}

	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		// disable dragging on report screen.
		if (SceneManager.GetActiveScene ().buildIndex != 4) {
			transform.position = eventData.position;
		}

	}

	#endregion

}
