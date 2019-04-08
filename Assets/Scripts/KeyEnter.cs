 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 using UnityEngine.EventSystems;

 public class KeyEnter : MonoBehaviour, ISelectHandler, IDeselectHandler {
     Button buttonMe;
	 bool isSelected;

	 void Start ()
	 {
         buttonMe = GetComponent<Button>();
		 isSelected = false;
     }

	 public void OnSelect(BaseEventData eventData)
	 {
		 isSelected = true;
	 }

	 public void OnDeselect(BaseEventData eventData)
	 {
		 isSelected = false;
	 }

     void Update()
	 {
         if(isSelected)
         {
			 if (Input.GetButtonDown("C") ||
			 	 Input.GetButtonDown("D") ||
				 Input.GetButtonDown("E") ||
				 Input.GetButtonDown("F") ||
				 Input.GetButtonDown("G") ||
				 Input.GetButtonDown("A") ||
				 Input.GetButtonDown("B"))
			{
				buttonMe.onClick.Invoke();
			}
		}
     }
 }
