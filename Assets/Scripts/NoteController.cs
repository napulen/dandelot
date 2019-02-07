using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour {
    public GameObject ledgerLinePrefab;
    private NoteMasterController noteMaster;
    private StaffMasterController staffMaster;
    private float notePositionXf;
    private float staffPosition;
    private string noteString;
    private string clefType;
    private float velocity;
	private void Awake()
    {
        noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        GameObject pageArea = GameObject.Find("PageArea");
        notePositionXf = -(pageArea.transform.localScale.x / 2f);
        staffPosition = 3f;
        noteString = "";
        velocity = 0f;
	}

    public void Initialize(float vel, string clef, string note)
    {
        velocity = vel;
        clefType = clef;
        noteString = note;
    }

    public string GetNote()
    {
        return noteString;
    }

    public string GetClefType()
    {
        return clefType;
    }

    private void AddAdditionalLines()
    {
        if (staffPosition >= 6f)
        {
            float offset = staffPosition - (int)staffPosition;
            int additionalLines = (int)staffPosition - 5;
            for (int i = 0; i < additionalLines; i++)
            {
                GameObject ledgerLine = Instantiate(ledgerLinePrefab, transform, false);
                ledgerLine.transform.localPosition = new Vector3(0f, -((float)i + offset), 0f);
            }
        }
        else if (staffPosition <= 0)
        {
            float offset = (int)staffPosition - staffPosition;
            int additionalLines = 1 - (int)staffPosition;
            for(int i = 0; i < additionalLines; i++)
            {
                GameObject ledgerLine = Instantiate(ledgerLinePrefab, transform, false);
                ledgerLine.transform.localPosition = new Vector3(0f, (float)i + offset, 0f);
            }
        }
    }

	private void Update()
    {
        // if (!staffMaster.IsLatestStaffList(currentStaffListString))
        // {
        //     SetCurrentStaff();
        //     currentStaffListString = staffMaster.GetStaffListString();
        //     currentStaffPosition = currentStaff.PositionY2StaffPosition(transform.position.y);
        //     currentNote = currentStaff.StaffPosition2Note(currentStaffPosition);
        //     AddAdditionalLines();
        //     Debug.Log("I am a " + currentNote + " in staff position " + currentStaffPosition + " of the " + currentStaff.GetClefString() + " staff");
        // }
        transform.position += Vector3.left * velocity * Time.deltaTime;
        if (transform.position.x <= notePositionXf)
        {
            noteMaster.EventNoteReachedEnd(gameObject);
        }
	}
}
