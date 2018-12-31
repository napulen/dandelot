using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffController : MonoBehaviour {
    public Sprite clefSpriteG;
    public Sprite clefSpriteF;
    public Sprite clefSpriteC;
    private GameObject clefObject;
    private SpriteRenderer clefSprite;
    private StaffMasterController staffMaster;
    private string currentClefName;
    private int currentClefStaffLine;

    private void Awake ()
    {
        clefObject = transform.Find("Clef").gameObject;
        clefSprite = clefObject.GetComponent<SpriteRenderer>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        // By default, have a treble clef
        SetClef("g_2");
	}

	private void Update ()
    {

	}

    public void SetClef(string clefName, int staffLine)
    {
        if (staffLine < 1 || staffLine > 5)
        {
            Debug.LogError("Invalid range for clef positions", gameObject);
            return;
        }
        switch(clefName)
        {
            case "g":
                clefSprite.sprite = clefSpriteG;
                break;
            case "f":
                clefSprite.sprite = clefSpriteF;
                break;
            case "c":
                clefSprite.sprite = clefSpriteC;
                break;
            default:
                Debug.LogError("Invalid clef name", gameObject);
                return;
        }
        currentClefName = clefName;
        currentClefStaffLine = staffLine;
        clefObject.transform.localPosition = new Vector3(clefObject.transform.localPosition.x, (float)staffLine - 3f, 0f);
    }

    public void SetClef(string clef)
    {
        if (!staffMaster.IsValidClef(clef))
        {
            Debug.LogError(clef + " is an invalid clef string");
            return;
        }
        string[] tokens = clef.Split('_');
        int line = 0;
        int.TryParse(tokens[1], out line);
        SetClef(tokens[0], line);
    }

    public string GetClefName()
    {
        return currentClefName;
    }

    public int GetClefStaffLine()
    {
        return currentClefStaffLine;
    }

    public string GetClefString()
    {
        return currentClefName + "_" + currentClefStaffLine;
    }

    public float StaffPosition2PositionY(float staffPosition)
    {
        float offset = staffPosition - 3f;
        float positionY = transform.position.y;
		return positionY + offset;
    }

    public float PositionY2StaffPosition(float positionY)
    {
        float offset = positionY - transform.position.y;
        float staffPosition = offset + 3f;
        float fraction = staffPosition - (int)staffPosition;
        if (Mathf.Approximately(fraction, 0.5f)) fraction = 0.5f;
        else if (Mathf.Approximately(fraction, -0.5f)) fraction = -0.5f;
        else fraction = 0f;
        return (int)staffPosition + fraction;
    }

    public string StaffPosition2Note(float staffPosition)
    {
        return staffMaster.GetNote(GetClefString(), staffPosition);
    }
}
