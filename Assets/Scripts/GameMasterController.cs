using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterController : MonoBehaviour {
    private InputHandlerController inputHandler;
    private NoteMasterController noteMaster;
    private StaffMasterController staffMaster;

    public Text streakText;
    public Text scoreText;

    private int streak;
    private int score;

	private void Start ()
    {
        inputHandler = GameObject.Find("InputHandler").GetComponent<InputHandlerController>();
        noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        noteMaster.EventStartSpawningNotes(gameObject);
	}

	private void Update ()
    {
        if (streak > 1)
        {
            streakText.text = streak + "x streak!";
        }
        else
        {
            streakText.text = "";
        }
        scoreText.text = "Score: " + score;
	}

    public void EventNoteSpawned(GameObject caller)
    {
        if (caller == noteMaster.gameObject)
        {
            Debug.Log("I have heard that a note has been spawned", gameObject);
        }
        else
        {
            Debug.LogError("I only listen to EventNoteSpawned() calls from the NoteMaster", gameObject);
        }
    }

    public void EventNoteDestroyed(GameObject caller)
    {
        if (caller == noteMaster.gameObject)
        {
            Debug.Log("I have heard that a note has been destroyed", gameObject);
            score++;
            streak++;
        }
        else
        {
            Debug.LogError("I only listen to EventNoteDestroyed() calls from the NoteMaster", gameObject);
        }
    }

    public void EventNoteMispelled(GameObject caller)
    {
        if (caller == noteMaster.gameObject)
        {
            Debug.Log("I have heard that a note has been mispelled", gameObject);
            streak = 0;
        }
        else
        {
            Debug.LogError("I only listen to EventNoteMispelled() calls from the NoteMaster", gameObject);
        }
    }

    public void EventNoteSpelled(GameObject caller)
    {
        if (caller == noteMaster.gameObject)
        {
            Debug.Log("I have heard that a note has been spelled", gameObject);
        }
        else
        {
            Debug.LogError("I only listen to EventNoteSpelled() calls from the NoteMaster", gameObject);
        }
    }

    public void EventNoteMissed(GameObject caller)
    {
        if (caller == noteMaster.gameObject)
        {
            Debug.Log("It seems the user has missed a note", gameObject);

        }
        else
        {
            Debug.LogError("I only listen to EventNoteMissed() calls from the NoteMaster", gameObject);
        }
    }

    public void EventClefChanged(GameObject caller)
    {
        if (caller == staffMaster.gameObject)
        {
            Debug.Log("I have heard that a clef has changed", gameObject);
        }
        else
        {
            Debug.LogError("I only listen to EventClefChanged() calls from the StaffMaster", gameObject);
        }
    }
}
