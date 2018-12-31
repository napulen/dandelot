using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterController : MonoBehaviour {
    private InputHandlerController inputHandler;
    private NoteMasterController noteMaster;
    private StaffMasterController staffMaster;

	private void Start ()
    {
        inputHandler = GameObject.Find("InputHandler").GetComponent<InputHandlerController>();
        noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        // Setting the inital staff arrangement
        List<string> initialStaffList = new List<string>();
        initialStaffList.Add("g_2");
        staffMaster.EventSetInitialStaff(initialStaffList, gameObject);
        noteMaster.EventStartSpawningNotes("doremifasollasi", gameObject);
	}

	private void Update ()
    {

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
        }
        else
        {
            Debug.LogError("I only listen to EventNoteMispelled() calls from the NoteMaster", gameObject);
        }
    }

    public void EventStaffReady(GameObject caller)
    {
        if (caller == staffMaster.gameObject)
        {
            Debug.Log("I have heard that a staff has been finally set", gameObject);
        }
        else
        {
            Debug.LogError("I only listen to EventStaffReady() calls from the StaffMaster", gameObject);
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

    public void EventStaffChanged(GameObject caller)
    {
        if (caller == staffMaster.gameObject)
        {
            Debug.Log("I have heard that a staff has changed", gameObject);
        }
        else
        {
            Debug.LogError("I only listen to EventStaffChanged() calls from the StaffMaster", gameObject);
        }
    }
}
