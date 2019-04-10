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
    public int performanceCheckThreshold;

    private int level;
    private int streak;
    private int score;
    private bool updatePerformance;
    private bool isSearchingClef;
    private int bestStreak;
    private int correctNotes;
    private int misspelledNotes;
    private int notesSpawned;
    private float previousPerformance;
    private int previousLevel;
    private int previousActiveNotes;


	private void Start ()
    {
        inputHandler = GameObject.Find("InputHandler").GetComponent<InputHandlerController>();
        noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        audioSource = GetComponent<AudioSource>();
        noteMaster.EventStartSpawningNotes(gameObject);
        isSearchingClef = false;
        bestStreak = 0;
        score = 0;
        level = 0;
        previousPerformance = -1;
        updatePerformance = true;
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
        if (updatePerformance)
        {
            updatePerformance = false;
            int activeNotes = notesSpawned - correctNotes;
            float positivePerformance = correctNotes / (float)notesSpawned;
            if (activeNotes > previousActiveNotes)
            {
                positivePerformance = correctNotes / (float)(notesSpawned + activeNotes);
            }
            float negativePerformance = misspelledNotes / (float)notesSpawned;
            float performance = positivePerformance - negativePerformance;
            Debug.Log("Performance: " + performance);
            if (performance > previousPerformance)
            {
                level = level < 12 ? level + 1: 12;
                Debug.Log("Increasing difficulty to " + level.ToString());
            }
            else if (performance < previousPerformance)
            {
                level = level > 0 ? level - 1: 0;
                Debug.Log("Decreasing difficulty to " + level.ToString());
            }
            if (level != previousLevel)
            {
                noteMaster.EventDifficultyChanged(level, gameObject);
                staffMaster.EventDifficultyChanged(level, gameObject);
            }
            previousPerformance = performance;
            previousLevel = level;
        }
        scoreText.text = "Score: " + score;
	}

    public void EventNoteSpawned(GameObject caller)
    {
        if (caller == noteMaster.gameObject)
        {
            // Debug.Log("I have heard that a note has been spawned", gameObject);
            notesSpawned++;
            if (notesSpawned % performanceCheckThreshold == 0)
            {
                updatePerformance = true;
            }
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
            // Debug.Log("I have heard that a note has been destroyed", gameObject);
            streak++;
            correctNotes++;
            score += streak;
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
            StaticClass.Score = score;
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
            // Debug.Log("I have heard that a clef has changed", gameObject);
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
