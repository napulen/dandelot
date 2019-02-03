using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour {
    public GameObject ledgerLinePrefab;
    private NoteMasterController noteMaster;
    private StaffMasterController staffMaster;
    private List<string> currentStaffListString;
    private StaffController currentStaff;
    private SpriteRenderer noteheadSprite;
    // private GameObject stem;
    private float notePositionXf;
    private float currentStaffPosition;
    private string currentNote;
    private float velocity;
	private void Awake()
    {
        noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        noteheadSprite = gameObject.GetComponent<SpriteRenderer>();
        GameObject pageArea = GameObject.Find("PageArea");
        notePositionXf = -(pageArea.transform.localScale.x / 2f);
        // stem = transform.Find("Stem").gameObject;
        currentStaffListString = new List<string>();
        currentStaff = null;
        currentStaffPosition = 3f;
        currentNote = "";
        velocity = 0f;
        // StemUp();
	}

    public void Initialize(float vel)
    {
        velocity = vel;
    }

    public string GetCurrentNote()
    {
        return currentNote;
    }

    /*
    private void StemUp()
    {
        stem.transform.localPosition = new Vector3(0.55f, 1.75f, 0f);
    }

    private void StemDown()
    {
        stem.transform.localPosition = new Vector3(-0.55f, -1.75f, 0f);
    }
    */

    private void AddAdditionalLines()
    {
        if (currentStaffPosition >= 6f)
        {
            float offset = currentStaffPosition - (int)currentStaffPosition;
            int additionalLines = (int)currentStaffPosition - 5;
            for (int i = 0; i < additionalLines; i++)
            {
                GameObject ledgerLine = Instantiate(ledgerLinePrefab, transform, false);
                ledgerLine.transform.localPosition = new Vector3(0f, -((float)i + offset), 0f);
            }
        }
        else if (currentStaffPosition <= 0)
        {
            float offset = (int)currentStaffPosition - currentStaffPosition;
            int additionalLines = 1 - (int)currentStaffPosition;
            for(int i = 0; i < additionalLines; i++)
            {
                GameObject ledgerLine = Instantiate(ledgerLinePrefab, transform, false);
                ledgerLine.transform.localPosition = new Vector3(0f, (float)i + offset, 0f);
            }
        }
    }

	private void Update()
    {
        if (!staffMaster.IsLatestStaffList(currentStaffListString))
        {
            SetCurrentStaff();
            currentStaffListString = staffMaster.GetStaffListString();
            currentStaffPosition = currentStaff.PositionY2StaffPosition(transform.position.y);
            // if (currentStaffPosition > 3f)
            //    StemDown();
            // else
            //     StemUp();
            currentNote = currentStaff.StaffPosition2Note(currentStaffPosition);
            noteheadSprite.sprite = noteMaster.GetNoteheadSprite(currentNote);
            AddAdditionalLines();
            Debug.Log("I am a " + currentNote + " in staff position " + currentStaffPosition + " of the " + currentStaff.GetClefString() + " staff");
        }
        transform.position += Vector3.left * velocity * Time.deltaTime;
        if (transform.position.x <= notePositionXf)
        {
            noteMaster.EventNoteReachedEnd(gameObject);
        }
	}

    private void SetCurrentStaff()
    {
        float shortestDistance = float.MaxValue;
        foreach (StaffController staff in staffMaster.GetStaffList())
        {
            float distance = Mathf.Abs(staff.transform.position.y - transform.position.y);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                currentStaff = staff;
            }
        }
        return;
    }
}
