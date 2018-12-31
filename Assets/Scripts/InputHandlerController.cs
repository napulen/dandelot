using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerController : MonoBehaviour {
    private GameMasterController gameMaster;
    private NoteMasterController noteMaster;
    private StaffMasterController staffMaster;

    private void Awake ()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterController>();
        noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
    }

	private void Update ()
    {
        if (Input.GetKeyDown("a"))
        {
            noteMaster.EventNoteSpelled("a", gameObject);
        }
        if (Input.GetKeyDown("b"))
        {
            noteMaster.EventNoteSpelled("b", gameObject);
        }
        if (Input.GetKeyDown("c"))
        {
            noteMaster.EventNoteSpelled("c", gameObject);
        }
        if (Input.GetKeyDown("d"))
        {
            noteMaster.EventNoteSpelled("d", gameObject);
        }
        if (Input.GetKeyDown("e"))
        {
            noteMaster.EventNoteSpelled("e", gameObject);
        }
        if (Input.GetKeyDown("f"))
        {
            noteMaster.EventNoteSpelled("f", gameObject);
        }
        if (Input.GetKeyDown("g"))
        {
            noteMaster.EventNoteSpelled("g", gameObject);
        }
    }
}
