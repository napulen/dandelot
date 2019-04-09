using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMapper : MonoBehaviour {
	Button thisButton;
	Animator buttonAnimator;
	public string associatedButtonName;

	void Start ()
	{
		thisButton = GetComponent<Button>();
		buttonAnimator = GetComponent<Animator>();
	}

	void Update ()
	{
		if (Input.GetButtonDown(associatedButtonName))
		{
			thisButton.onClick.Invoke();
			buttonAnimator.SetTrigger("Pressed");
		}

		if (Input.GetButtonUp(associatedButtonName))
		{
			Debug.Log("Released");
			buttonAnimator.SetTrigger("Normal");
		}
	}
}