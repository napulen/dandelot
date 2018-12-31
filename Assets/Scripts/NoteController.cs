using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour {
    public GameObject ledgerLinePrefab;
    private NoteMasterController noteMaster;
    private StaffMasterController staffMaster;
    private List<StaffController> lastStaffList;
    private StaffController currentStaff;
    private SpriteRenderer noteheadSprite;
    private GameObject stem;
    private float currentStaffPosition;
    private string currentNote;
    private float velocity;
	private void Awake()
    {
        noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        noteheadSprite = gameObject.GetComponent<SpriteRenderer>();
        stem = transform.Find("Stem").gameObject;
        lastStaffList = new List<StaffController>();
        currentStaff = null;
        currentStaffPosition = 3f;
        currentNote = "";
        velocity = 0f;
        StemUp();
	}

    public void Initialize(float vel)
    {
        velocity = vel;
    }

    private void StemUp()
    {
        stem.transform.localPosition = new Vector3(0.55f, 1.75f, 0f);
    }

    private void StemDown()
    {
        stem.transform.localPosition = new Vector3(-0.55f, -1.75f, 0f);
    }

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
        List<StaffController> newStaffList = staffMaster.GetStaffList();
        if (newStaffList.Count > 0 && newStaffList != lastStaffList)
        {
            // TODO: Do the proper thing
            currentStaff = newStaffList[0];
            ////////////////////////////
            lastStaffList = newStaffList;
        }
        if (currentStaff)
        {
            currentStaffPosition = currentStaff.PositionY2StaffPosition(transform.position.y);
            if (currentStaffPosition > 3f)
                StemDown();
            else
                StemUp();
            currentNote = currentStaff.StaffPosition2Note(currentStaffPosition);
            noteheadSprite.sprite = noteMaster.GetNoteheadSprite(currentNote);
            AddAdditionalLines();
            Debug.Log("I am a " + currentNote + " in staff position " + currentStaffPosition + " of the " + currentStaff.GetClefString() + " staff");
        }

        transform.position += Vector3.left * velocity * Time.deltaTime;
	}
}
