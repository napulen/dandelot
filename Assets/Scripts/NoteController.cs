using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour {
    public GameObject ledgerLinePrefab;
    private NoteMasterController noteMaster;
    private StaffMasterController staffMaster;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float notePositionXf;
    private float staffPosition;
    private string noteString;
    private string clefType;
    private float velocity;
	private void Awake()
    {
        noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        GameObject pageArea = GameObject.Find("PageArea");
        notePositionXf = -(pageArea.transform.localScale.x / 2f);
        staffPosition = 3f;
        noteString = "";
        velocity = 0f;
	}

    public void Initialize(float vel, string clef, float staffPos)
    {
        velocity = vel;
        clefType = clef;
        staffPosition = staffPos;
        noteString = staffMaster.GetNote(clef, staffPosition);
        string[] clefStaffLine = clef.Split('_');
        switch(clefStaffLine[0])
        {
            case "g":
                animator.SetBool("ClefG", true);
                spriteRenderer.color = Color.cyan;
                break;
            case "f":
                animator.SetBool("ClefF", true);
                spriteRenderer.color = Color.red;
                break;
            case "c":
                animator.SetBool("ClefC", true);
                spriteRenderer.color = Color.green;
                break;
        }
    }

    public void Spelled()
    {
        Color tmp = spriteRenderer.color;
        tmp.a = 0.4f;
        spriteRenderer.color = tmp;
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
        //
        //     Debug.Log("I am a " + currentNote + " in staff position " + currentStaffPosition + " of the " + currentStaff.GetClefString() + " staff");
        // }
        AddAdditionalLines();
        transform.position += Vector3.left * velocity * Time.deltaTime;
        if (transform.position.x <= notePositionXf)
        {
            noteMaster.EventNoteReachedEnd(gameObject);
        }
	}
}
