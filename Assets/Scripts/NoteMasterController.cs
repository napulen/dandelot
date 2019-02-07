using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMasterController : MonoBehaviour {
    public NoteController noteprefab;
    public List<Sprite> noteheadsDoReMi;
    public List<Sprite> noteheadsCDE;
    private GameMasterController gameMaster;
    private InputHandlerController inputHandler;
    private StaffMasterController staffMaster;
    private Queue<NoteController> noteQueue;
    private float avgNoteSpawnInterval;
    private float noteVelocity;
    private bool shouldSpawnNotes;
    private float notePositionX0;
    private float elapsedTime;
    private float currentNoteSpawnInterval;
    private int difficulty;

    // temporary
    private float posbottom;
    private float postop;
    private float currentpos;
    private float direction;

    private void Awake ()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterController>();
        inputHandler = GameObject.Find("InputHandler").GetComponent<InputHandlerController>();
        staffMaster = GameObject.Find("StaffMaster").GetComponent<StaffMasterController>();
        noteQueue = new Queue<NoteController>();
        difficulty = 0;
        avgNoteSpawnInterval = 1f;
        noteVelocity = 1f;
        shouldSpawnNotes = false;
        GameObject pageArea = GameObject.Find("PageArea");
        notePositionX0 = pageArea.transform.localScale.x / 2f;
        elapsedTime = 0f;
        currentNoteSpawnInterval = avgNoteSpawnInterval;

        // temporary
        posbottom = -5f;
        postop = 5f;
        currentpos = posbottom;
        direction = 1f;
    }

    void Update ()
    {
        elapsedTime += Time.deltaTime;
        if (shouldSpawnNotes && elapsedTime > currentNoteSpawnInterval)
        {
            // temporary
            if (currentpos > postop) direction = -1f;
            if (currentpos < posbottom) direction = 1f;
            currentpos += 0.5f * direction;

            elapsedTime = 0f;
            Vector3 position = new Vector3(notePositionX0, currentpos, 0f);
            NoteController newNote = Instantiate(noteprefab, transform).GetComponent<NoteController>();
            newNote.transform.position = position;
            float staffPosition = staffMaster.PositionY2StaffPosition(currentpos);
            string clef = staffMaster.GenerateClefForNote();
            newNote.Initialize(noteVelocity, clef, staffPosition);
            noteQueue.Enqueue(newNote);
        }
	}

    private Vector3 getNotePosition ()
    {
        int staffPosition = Random.Range(-6, 18);
        float originY = -3f + staffPosition * 0.5f;
        return new Vector3(notePositionX0, originY, 0f);
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
                gameMaster.EventNoteDestroyed(gameObject);
                NoteController noteController = noteQueue.Dequeue();
                Destroy(noteController.gameObject);
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

    public void EventDifficultyChanged(int d, GameObject caller)
    {
        if (caller == gameMaster.gameObject)
        {
            difficulty = d;
            Debug.Log("I heard that the difficulty has changed to " + d, gameObject);
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
