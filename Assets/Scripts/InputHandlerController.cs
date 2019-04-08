using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandlerController : MonoBehaviour {
    private GameMasterController gameMaster;
    private NoteMasterController noteMaster;
    private StaffMasterController staffMaster;

    public Button buttonC;
    public Button buttonD;
    public Button buttonE;
    public Button buttonF;
    public Button buttonG;
    public Button buttonA;
    public Button buttonB;
    public Button buttonPowerUp;

    private bool axisInUse;
    private int clefIndex;

    private void Awake ()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterController>();
        noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();

        buttonC.onClick.AddListener(NoteCSpelled);
        buttonD.onClick.AddListener(NoteDSpelled);
        buttonE.onClick.AddListener(NoteESpelled);
        buttonF.onClick.AddListener(NoteFSpelled);
        buttonG.onClick.AddListener(NoteGSpelled);
        buttonA.onClick.AddListener(NoteASpelled);
        buttonB.onClick.AddListener(NoteBSpelled);

        axisInUse = false;
        clefIndex = 0;
    }

    private void NoteCSpelled()
    {
        noteMaster.EventNoteSpelled("c", gameObject);

    }

    private void NoteDSpelled()
    {
        noteMaster.EventNoteSpelled("d", gameObject);
    }

    private void NoteESpelled()
    {
        noteMaster.EventNoteSpelled("e", gameObject);
    }

    private void NoteFSpelled()
    {
        noteMaster.EventNoteSpelled("f", gameObject);
    }

    private void NoteGSpelled()
    {
        noteMaster.EventNoteSpelled("g", gameObject);
    }

    private void NoteASpelled()
    {
        noteMaster.EventNoteSpelled("a", gameObject);
    }

    private void NoteBSpelled()
    {
        noteMaster.EventNoteSpelled("b", gameObject);
    }

	private void Update ()
    {
        if (Input.GetButtonDown("A"))
        {
            NoteASpelled();
        }
        if (Input.GetButtonDown("B"))
        {
            NoteBSpelled();
        }
        if (Input.GetButtonDown("C"))
        {
            NoteCSpelled();
        }
        if (Input.GetButtonDown("D"))
        {
            NoteDSpelled();
        }
        if (Input.GetButtonDown("E"))
        {
            NoteESpelled();
        }
        if (Input.GetButtonDown("F"))
        {
            NoteFSpelled();
        }
        if (Input.GetButtonDown("G"))
        {
            NoteGSpelled();
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0f)
        {
            if (!axisInUse)
            {
                axisInUse = true;
                float sign = Mathf.Sign(horizontal);
                if (sign > 0)
                {
                    clefIndex = (clefIndex + 1) % 3;
                    Debug.Log("Rotating right!");
                }
                else
                {
                    clefIndex = (clefIndex + 2) % 3;
                    Debug.Log("Rotating left!");
                }
                switch(clefIndex)
                {
                    case 0:
                        staffMaster.SetClef("g_2");
                        break;
                    case 1:
                        staffMaster.SetClef("f_4");
                        break;
                    case 2:
                        staffMaster.SetClef("c_3");
                        break;
                }
            }
        }
        if (horizontal == 0f)
        {
            axisInUse = false;
        }
    }
}
