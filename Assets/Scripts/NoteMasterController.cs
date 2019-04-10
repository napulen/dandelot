using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMasterController : MonoBehaviour {
    public NoteController noteprefab;
    private GameMasterController gameMaster;
    private InputHandlerController inputHandler;
    private StaffMasterController staffMaster;
    private Queue<NoteController> noteQueue;
    private float noteVelocity;
    private bool shouldSpawnNotes;
    private float notePositionX0;
    private float elapsedTime;
    private float noteSpawnInterval;
    private int lastStaffPosition;
    private int noteRangeMin;
    private int noteRangeMax;

    private void Awake ()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterController>();
        inputHandler = GameObject.Find("InputHandler").GetComponent<InputHandlerController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        noteQueue = new Queue<NoteController>();
        noteVelocity = 2f;
        shouldSpawnNotes = false;
        GameObject pageArea = GameObject.Find("PageArea");
        notePositionX0 = pageArea.transform.localScale.x / 2f;
        elapsedTime = 0f;
        AdjustDifficulty(0);
    }

    void Update ()
    {
        elapsedTime += Time.deltaTime;
        if (shouldSpawnNotes && elapsedTime > noteSpawnInterval)
        {
            elapsedTime = 0f;
            Vector3 position = getNotePosition();
            NoteController newNote = Instantiate(noteprefab, transform).GetComponent<NoteController>();
            newNote.transform.position = position;
            float staffPosition = staffMaster.PositionY2StaffPosition(position.y);
            string clef = staffMaster.GenerateClefForNote();
            newNote.Initialize(noteVelocity, clef, staffPosition);
            noteQueue.Enqueue(newNote);
            gameMaster.EventNoteSpawned(gameObject);
        }
	}

    private Vector3 getNotePosition ()
    {
        int staffPosition;
        do {
            staffPosition = Random.Range(noteRangeMin, noteRangeMax);
        } while (staffPosition == lastStaffPosition);
        lastStaffPosition = staffPosition;
        float originY = -3f + staffPosition * 0.5f;
        return new Vector3(notePositionX0, originY, 0f);
    }

    private void AdjustDifficulty(int difficulty)
    {
        switch(difficulty)
        {
            case 0:
                noteSpawnInterval = 6.0f;
                noteRangeMin = 5;
                noteRangeMax = 7;
                break;
            case 1:
                noteSpawnInterval = 6.0f;
                noteRangeMin = 5;
                noteRangeMax = 7;
                break;
            case 2:
                noteSpawnInterval = 5.5f;
                noteRangeMin = 4;
                noteRangeMax = 8;
                break;
            case 3:
                noteSpawnInterval = 5.0f;
                noteRangeMin = 3;
                noteRangeMax = 9;
                break;
            case 4:
                noteSpawnInterval = 4.5f;
                noteRangeMin = 2;
                noteRangeMax = 10;
                break;
            case 5:
                noteSpawnInterval = 4.0f;
                noteRangeMin = 1;
                noteRangeMax = 11;
                break;
            case 6:
                noteSpawnInterval = 3.5f;
                noteRangeMin = 0;
                noteRangeMax = 12;
                break;
            case 7:
                noteSpawnInterval = 3.0f;
                noteRangeMin = -1;
                noteRangeMax = 13;
                break;
            case 8:
                noteSpawnInterval = 2.5f;
                noteRangeMin = -2;
                noteRangeMax = 14;
                break;
            case 9:
                noteSpawnInterval = 2.0f;
                noteRangeMin = -3;
                noteRangeMax = 15;
                break;
            case 10:
                noteSpawnInterval = 1.5f;
                noteRangeMin = -4;
                noteRangeMax = 16;
                break;
            case 11:
                noteSpawnInterval = 1.0f;
                noteRangeMin = -5;
                noteRangeMax = 17;
                break;
            case 12:
                noteSpawnInterval = 0.75f;
                noteRangeMin = -6;
                noteRangeMax = 18;
                break;
        }
    }

    public void EventNoteSpelled(string note, GameObject caller)
    {
        if (caller == inputHandler.gameObject)
        {
            Debug.Log("I heard that note " + note + " was spelled by the user", gameObject);
            string currentClef = staffMaster.GetClef();
            if (noteQueue.Count > 0 &&
                noteQueue.Peek().GetClefType() == currentClef &&
                noteQueue.Peek().GetNote() == note)
            {
                gameMaster.EventNoteSpelled(gameObject);
                NoteController noteController = noteQueue.Dequeue();
                noteController.Spelled();
                staffMaster.EventNoteSpelled(noteController, gameObject);
            }
            else
            {
                gameMaster.EventNoteMispelled(gameObject);
            }
        }
        else
        {
            Debug.LogError("I only listen to EventNoteSpelled() calls from the InputHandler", gameObject);
        }
    }

    public void EventNoteHit(GameObject obj, GameObject caller)
    {
        if (caller.tag == "Projectile")
        {
            gameMaster.EventNoteDestroyed(gameObject);
            Destroy(obj);
        }
        else
        {
            Debug.LogError("I only listen to EventNoteHit() calls from a projectile");
        }
    }

    public NoteController GetFrontNote()
    {
        if (noteQueue.Count > 0)
        {
            return noteQueue.Peek();
        }
        return null;
    }

    public bool IsFrontNote(NoteController note)
    {
        return (noteQueue.Count > 0 && noteQueue.Peek() == note);
    }

    public void EventNoteReachedEnd(GameObject caller)
    {
        if (caller.tag == "Note")
        {
            Debug.Log("I heard that a note has made it to the other side", gameObject);
            gameMaster.EventNoteMissed(gameObject);
            NoteController noteController = caller.GetComponent<NoteController>();
            if (noteQueue.Count > 0 && noteQueue.Peek() == noteController)
            {
                noteQueue.Dequeue();
            }
            else
            {
                Debug.LogError("A note that reached the other side is not the first element in the note queue");
            }
            Destroy(caller);
        }
        else
        {
            Debug.LogError("I only listen to EventNoteReachedEnd() calls from NoteControllers", gameObject);
        }
    }

    public void EventDifficultyChanged(int difficulty, GameObject caller)
    {
        if (caller == gameMaster.gameObject)
        {
            AdjustDifficulty(difficulty);
            Debug.Log("I heard that the difficulty has changed to " + difficulty, gameObject);
        }
        else
        {
            Debug.LogError("I only listen to EventDifficultyChanged() calls from the GameMaster", gameObject);
        }
    }

    public void EventStopSpawningNotes(GameObject caller)
    {
        if (caller == gameMaster.gameObject)
        {
            Debug.Log("Stopping the stream of notes", gameObject);
        }
        else
        {
            Debug.LogError("I only listen to EventStopSpawning() calls from the GameMaster", gameObject);
        }
    }

    public void EventStartSpawningNotes(GameObject caller)
    {
        if (caller == gameMaster.gameObject)
        {
            Debug.Log("Starting (or restarting) the stream of notes", gameObject);
            shouldSpawnNotes = true;
        }
        else
        {
            Debug.LogError("I only listen to EventStartSpawningNotes() calls from the GameMaster", gameObject);
        }
    }
}
