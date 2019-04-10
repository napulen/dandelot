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

    private Animator buttonCAnimator;
    private Animator buttonDAnimator;
    private Animator buttonEAnimator;
    private Animator buttonFAnimator;
    private Animator buttonGAnimator;
    private Animator buttonAAnimator;
    private Animator buttonBAnimator;
    private Animator buttonPowerUpAnimator;

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

        buttonCAnimator = buttonC.GetComponent<Animator>();
        buttonDAnimator = buttonD.GetComponent<Animator>();
        buttonEAnimator = buttonE.GetComponent<Animator>();
        buttonFAnimator = buttonF.GetComponent<Animator>();
        buttonGAnimator = buttonG.GetComponent<Animator>();
        buttonAAnimator = buttonA.GetComponent<Animator>();
        buttonBAnimator = buttonB.GetComponent<Animator>();

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
            buttonAAnimator.SetTrigger("Pressed");
            NoteASpelled();
        }
        if (Input.GetButtonUp("A"))
        {
            buttonAAnimator.SetTrigger("Normal");
        }
        if (Input.GetButtonDown("B"))
        {
            buttonBAnimator.SetTrigger("Pressed");
            NoteBSpelled();
        }
        if (Input.GetButtonUp("B"))
        {
            buttonBAnimator.SetTrigger("Normal");
        }
        if (Input.GetButtonDown("C"))
        {
            buttonCAnimator.SetTrigger("Pressed");
            NoteCSpelled();
        }
        if (Input.GetButtonUp("C"))
        {
            buttonCAnimator.SetTrigger("Normal");
        }
        if (Input.GetButtonDown("D"))
        {
            buttonDAnimator.SetTrigger("Pressed");
            NoteDSpelled();
        }
        if (Input.GetButtonUp("D"))
        {
            buttonDAnimator.SetTrigger("Normal");
        }
        if (Input.GetButtonDown("E"))
        {
            buttonEAnimator.SetTrigger("Pressed");
            NoteESpelled();
        }
        if (Input.GetButtonUp("E"))
        {
            buttonEAnimator.SetTrigger("Normal");
        }
        if (Input.GetButtonDown("F"))
        {
            buttonFAnimator.SetTrigger("Pressed");
            NoteFSpelled();
        }
        if (Input.GetButtonUp("F"))
        {
            buttonFAnimator.SetTrigger("Normal");
        }
        if (Input.GetButtonDown("G"))
        {
            buttonGAnimator.SetTrigger("Pressed");
            NoteGSpelled();
        }
        if (Input.GetButtonUp("G"))
        {
            buttonGAnimator.SetTrigger("Normal");
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
                    // Debug.Log("Rotating right!");
                }
                else
                {
                    clefIndex = (clefIndex + 2) % 3;
                    // Debug.Log("Rotating left!");
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
