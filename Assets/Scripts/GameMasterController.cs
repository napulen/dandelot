using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMasterController : MonoBehaviour {
    private InputHandlerController inputHandler;
    private NoteMasterController noteMaster;
    private StaffMasterController staffMaster;
    private AudioSource audioSource;

    public Text streakText;
    public Text scoreText;
    public AudioClip wrongClef;
    public AudioClip wrongNote;

    private int streak;
    private bool isSearchingClef;
    private int bestStreak;
    private int correctNotes;
    private int misspelledNotes;

	private void Start ()
    {
        inputHandler = GameObject.Find("InputHandler").GetComponent<InputHandlerController>();
        noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        audioSource = GetComponent<AudioSource>();
        noteMaster.EventStartSpawningNotes(gameObject);
        isSearchingClef = false;
        bestStreak = 0;
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
        scoreText.text = "Score: " + correctNotes;
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
            correctNotes++;
            streak++;
            if (streak > bestStreak)
            {
                bestStreak = streak;
            }
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
            // Debug.Log("I have heard that a note has been mispelled", gameObject);
            streak = 0;
            misspelledNotes++;
            if (!isSearchingClef)
            {
                audioSource.clip = wrongNote;
                audioSource.Play();
            }
        }
        else
        {
            // Debug.LogError("I only listen to EventNoteMispelled() calls from the NoteMaster", gameObject);
        }
    }

    public void EventNoteSpelled(GameObject caller)
    {
        if (caller == noteMaster.gameObject)
        {
            // Debug.Log("I have heard that a note has been spelled", gameObject);
        }
        else
        {
            // Debug.LogError("I only listen to EventNoteSpelled() calls from the NoteMaster", gameObject);
        }
    }

    public void EventNoteMissed(GameObject caller)
    {
        if (caller == noteMaster.gameObject)
        {
            Debug.Log("It seems the user has missed a note", gameObject);
            StaticClass.BestStreak = bestStreak;
            StaticClass.CorrectNotes = correctNotes;
            StaticClass.MisspelledNotes = misspelledNotes;
            SceneManager.LoadScene("GameOver");
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

    public void EventWrongClef(GameObject caller)
    {
        if (caller == staffMaster.gameObject)
        {
            if (!isSearchingClef)
            {
                isSearchingClef = true;
                audioSource.clip = wrongClef;
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogError("I only listen to EventWrongClef() calls from the StaffMaster", gameObject);
        }
    }

    public void EventRightClef(GameObject caller)
    {
        if (caller == staffMaster.gameObject)
        {
            if (isSearchingClef)
            {
                isSearchingClef = false;
                audioSource.Stop();
            }
        }
        else
        {
            Debug.LogError("I only listen to EventWrongClef() calls from the StaffMaster", gameObject);
        }
    }
}
