using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffMasterController : MonoBehaviour {
	private GameMasterController gameMaster;
	private InputHandlerController inputHandler;
	private NoteMasterController noteMaster;
	private GameObject clefObject;
    public Sprite clefSpriteG;
    public Sprite clefSpriteF;
    public Sprite clefSpriteC;
    public Image clefChangeAnimation;
    public ProjectileController projectilePrefab;
    private SpriteRenderer clefSpriteRenderer;
    private AudioSource audioSource;
    private string currentClef;
    private string currentClefName;
    private int currentClefStaffLine;
    private Dictionary<string, Dictionary<float, string> > clefDictionary;

    private float probG2, probF4, probC3;

	private void Awake ()
	{
		gameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterController>();
		inputHandler = GameObject.Find("InputHandler").GetComponent<InputHandlerController>();
		noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
        audioSource = GetComponent<AudioSource>();
		clefObject = transform.Find("Clef").gameObject;
        clefSpriteRenderer = clefObject.GetComponent<SpriteRenderer>();
        InitDict();
        SetClef("g_2");
        AdjustDifficulty(0);
	}

	private void Update ()
	{
		NoteController leftmostNote = noteMaster.GetFrontNote();
        if (leftmostNote != null && leftmostNote.GetClefType() != currentClef)
        {
            clefChangeAnimation.gameObject.SetActive(true);
            gameMaster.EventWrongClef(gameObject);
        }
        else
        {
            clefChangeAnimation.gameObject.SetActive(false);
            gameMaster.EventRightClef(gameObject);
        }
	}

    private void AdjustDifficulty(int difficulty)
    {
        switch(difficulty)
        {
            case 0:
                probG2 = 1.0f;
                probF4 = 0.0f;
                probC3 = 0.0f;
                break;
            case 1:
                probG2 = 1.0f;
                probF4 = 0.0f;
                probC3 = 0.0f;
                break;
            case 2:
                probG2 = 1.0f;
                probF4 = 0.0f;
                probC3 = 0.0f;
                break;
            case 3:
                probG2 = 1.0f;
                probF4 = 0.0f;
                probC3 = 0.0f;
                break;
            case 4:
                probG2 = 1.0f;
                probF4 = 0.0f;
                probC3 = 0.0f;
                break;
            case 5:
                probG2 = 0.8f;
                probF4 = 1.0f;
                probC3 = 0.0f;
                break;
            case 6:
                probG2 = 0.7f;
                probF4 = 1.0f;
                probC3 = 0.0f;
                break;
            case 7:
                probG2 = 0.6f;
                probF4 = 1.0f;
                probC3 = 0.0f;
                break;
            case 8:
                probG2 = 0.5f;
                probF4 = 1.0f;
                probC3 = 0.0f;
                break;
            case 9:
                probG2 = 0.45f;
                probF4 = 0.9f;
                probC3 = 1.0f;
                break;
            case 10:
                probG2 = 0.4f;
                probF4 = 0.8f;
                probC3 = 1.0f;
                break;
            case 11:
                probG2 = 0.35f;
                probF4 = 0.7f;
                probC3 = 1.0f;
                break;
            case 12:
                probG2 = 0.33f;
                probF4 = 0.66f;
                probC3 = 1.0f;
                break;
        }
    }

	private void InitDict()
    {
        // I guess this was overkill but gg, all the valid clefs
        // plus O(1) access for a few redundant lines of copy-pasta
        clefDictionary = new Dictionary<string, Dictionary<float, string> >();
        clefDictionary["g_1"] = new Dictionary<float, string>()
        {
            {-4f, "d"}, {-3.5f, "e"}, {-3f, "f"}, {-2.5f, "g"}, {-2f, "a"},
            {-1.5f, "b"}, {-1f, "c"}, {-0.5f, "d"}, {0f, "e"}, {0.5f, "f"},
            {1f, "g"}, {1.5f, "a"}, {2f, "b"}, {2.5f, "c"}, {3f, "d"},
            {3.5f, "e"}, {4f, "f"}, {4.5f, "g"}, {5f, "a"}, {5.5f, "b"},
            {6f, "c"}, {6.5f, "d"}, {7f, "e"}, {7.5f, "f"}, {8f, "g"},
            {8.5f, "a"}, {9f, "b"}, {9.5f, "c"}, {10f, "d"}, {10.5f, "e"}
        };
        clefDictionary["g_2"] = new Dictionary<float, string>()
        {
            {-4f, "b"}, {-3.5f, "c"}, {-3f, "d"}, {-2.5f, "e"}, {-2f, "f"},
            {-1.5f, "g"}, {-1f, "a"}, {-0.5f, "b"}, {0f, "c"}, {0.5f, "d"},
            {1f, "e"}, {1.5f, "f"}, {2f, "g"}, {2.5f, "a"}, {3f, "b"},
            {3.5f, "c"}, {4f, "d"}, {4.5f, "e"}, {5f, "f"}, {5.5f, "g"},
            {6f, "a"}, {6.5f, "b"}, {7f, "c"}, {7.5f, "d"}, {8f, "e"},
            {8.5f, "f"}, {9f, "g"}, {9.5f, "a"}, {10f, "b"}, {10.5f, "c"}
        };
        clefDictionary["c_1"] = new Dictionary<float, string>()
        {
            {-4f, "g"}, {-3.5f, "a"}, {-3f, "b"}, {-2.5f, "c"}, {-2f, "d"},
            {-1.5f, "e"}, {-1f, "f"}, {-0.5f, "g"}, {0f, "a"}, {0.5f, "b"},
            {1f, "c"}, {1.5f, "d"}, {2f, "e"}, {2.5f, "f"}, {3f, "g"},
            {3.5f, "a"}, {4f, "b"}, {4.5f, "c"}, {5f, "d"}, {5.5f, "e"},
            {6f, "f"}, {6.5f, "g"}, {7f, "a"}, {7.5f, "b"}, {8f, "c"},
            {8.5f, "d"}, {9f, "e"}, {9.5f, "f"}, {10f, "g"}, {10.5f, "a"}
        };
        clefDictionary["c_2"] = new Dictionary<float, string>()
        {
            {-4f, "e"}, {-3.5f, "f"}, {-3f, "g"}, {-2.5f, "a"}, {-2f, "b"},
            {-1.5f, "c"}, {-1f, "d"}, {-0.5f, "e"}, {0f, "f"}, {0.5f, "g"},
            {1f, "a"}, {1.5f, "b"}, {2f, "c"}, {2.5f, "d"}, {3f, "e"},
            {3.5f, "f"}, {4f, "g"}, {4.5f, "a"}, {5f, "b"}, {5.5f, "c"},
            {6f, "d"}, {6.5f, "e"}, {7f, "f"}, {7.5f, "g"}, {8f, "a"},
            {8.5f, "b"}, {9f, "c"}, {9.5f, "d"}, {10f, "e"}, {10.5f, "f"}
        };
        clefDictionary["c_3"] = new Dictionary<float, string>()
        {
            {-4f, "c"}, {-3.5f, "d"}, {-3f, "e"}, {-2.5f, "f"}, {-2f, "g"},
            {-1.5f, "a"}, {-1f, "b"}, {-0.5f, "c"}, {0f, "d"}, {0.5f, "e"},
            {1f, "f"}, {1.5f, "g"}, {2f, "a"}, {2.5f, "b"}, {3f, "c"},
            {3.5f, "d"}, {4f, "e"}, {4.5f, "f"}, {5f, "g"}, {5.5f, "a"},
            {6f, "b"}, {6.5f, "c"}, {7f, "d"}, {7.5f, "e"}, {8f, "f"},
            {8.5f, "g"}, {9f, "a"}, {9.5f, "b"}, {10f, "c"}, {10.5f, "d"}
        };
        clefDictionary["c_4"] = new Dictionary<float, string>()
        {
            {-4f, "a"}, {-3.5f, "b"}, {-3f, "c"}, {-2.5f, "d"}, {-2f, "e"},
            {-1.5f, "f"}, {-1f, "g"}, {-0.5f, "a"}, {0f, "b"}, {0.5f, "c"},
            {1f, "d"}, {1.5f, "e"}, {2f, "f"}, {2.5f, "g"}, {3f, "a"},
            {3.5f, "b"}, {4f, "c"}, {4.5f, "d"}, {5f, "e"}, {5.5f, "f"},
            {6f, "g"}, {6.5f, "a"}, {7f, "b"}, {7.5f, "c"}, {8f, "d"},
            {8.5f, "e"}, {9f, "f"}, {9.5f, "g"}, {10f, "a"}, {10.5f, "b"}
        };
        clefDictionary["c_5"] = new Dictionary<float, string>()
        {
            {-4f, "f"}, {-3.5f, "g"}, {-3f, "a"}, {-2.5f, "b"}, {-2f, "c"},
            {-1.5f, "d"}, {-1f, "e"}, {-0.5f, "f"}, {0f, "g"}, {0.5f, "a"},
            {1f, "b"}, {1.5f, "c"}, {2f, "d"}, {2.5f, "e"}, {3f, "f"},
            {3.5f, "g"}, {4f, "a"}, {4.5f, "b"}, {5f, "c"}, {5.5f, "d"},
            {6f, "e"}, {6.5f, "f"}, {7f, "g"}, {7.5f, "a"}, {8f, "b"},
            {8.5f, "c"}, {9f, "d"}, {9.5f, "e"}, {10f, "f"}, {10.5f, "g"}
        };
        clefDictionary["f_3"] = new Dictionary<float, string>()
        {
            {-4f, "f"}, {-3.5f, "g"}, {-3f, "a"}, {-2.5f, "b"}, {-2f, "c"},
            {-1.5f, "d"}, {-1f, "e"}, {-0.5f, "f"}, {0f, "g"}, {0.5f, "a"},
            {1f, "b"}, {1.5f, "c"}, {2f, "d"}, {2.5f, "e"}, {3f, "f"},
            {3.5f, "g"}, {4f, "a"}, {4.5f, "b"}, {5f, "c"}, {5.5f, "d"},
            {6f, "e"}, {6.5f, "f"}, {7f, "g"}, {7.5f, "a"}, {8f, "b"},
            {8.5f, "c"}, {9f, "d"}, {9.5f, "e"}, {10f, "f"}, {10.5f, "g"}
        };
        clefDictionary["f_4"] = new Dictionary<float, string>()
        {
            {-4f, "d"}, {-3.5f, "e"}, {-3f, "f"}, {-2.5f, "g"}, {-2f, "a"},
            {-1.5f, "b"}, {-1f, "c"}, {-0.5f, "d"}, {0f, "e"}, {0.5f, "f"},
            {1f, "g"}, {1.5f, "a"}, {2f, "b"}, {2.5f, "c"}, {3f, "d"},
            {3.5f, "e"}, {4f, "f"}, {4.5f, "g"}, {5f, "a"}, {5.5f, "b"},
            {6f, "c"}, {6.5f, "d"}, {7f, "e"}, {7.5f, "f"}, {8f, "g"},
            {8.5f, "a"}, {9f, "b"}, {9.5f, "c"}, {10f, "d"}, {10.5f, "e"}
        };
        clefDictionary["f_5"] = new Dictionary<float, string>()
        {
            {-4f, "b"}, {-3.5f, "c"}, {-3f, "d"}, {-2.5f, "e"}, {-2f, "f"},
            {-1.5f, "g"}, {-1f, "a"}, {-0.5f, "b"}, {0f, "c"}, {0.5f, "d"},
            {1f, "e"}, {1.5f, "f"}, {2f, "g"}, {2.5f, "a"}, {3f, "b"},
            {3.5f, "c"}, {4f, "d"}, {4.5f, "e"}, {5f, "f"}, {5.5f, "g"},
            {6f, "a"}, {6.5f, "b"}, {7f, "c"}, {7.5f, "d"}, {8f, "e"},
            {8.5f, "f"}, {9f, "g"}, {9.5f, "a"}, {10f, "b"}, {10.5f, "c"}
        };
    }

    public void EventDifficultyChanged(int difficulty, GameObject caller)
    {
        if (caller == gameMaster.gameObject)
        {
            // Debug.Log("I heard that the difficulty has changed to " + difficulty, gameObject);
            AdjustDifficulty(difficulty);
        }
        else
        {
            Debug.LogError("I only listen to EventDifficultyChanged() calls from the GameMaster", gameObject);
        }
    }

    public void EventNoteSpelled(NoteController note, GameObject caller)
    {
        if (caller = noteMaster.gameObject)
        {
            ProjectileController projectile = Instantiate(projectilePrefab).GetComponent<ProjectileController>();
            projectile.transform.position = clefObject.transform.position;
            projectile.Initialize(note.gameObject, clefObject, -40f, 30f);
        }
        else
        {
            Debug.LogError("I only listen to EventNoteSpelled() calls from the NoteMaster", gameObject);
        }
    }

	public bool IsValidClef(string clef)
	{
		return clefDictionary.ContainsKey(clef);
	}

	public string GetNote(string clef, float staffPosition)
	{
		if (clefDictionary.ContainsKey(clef) &&
			clefDictionary[clef].ContainsKey(staffPosition))
		{
			return clefDictionary[clef][staffPosition];
		}
		Debug.LogError("Invalid staff position " + staffPosition);
        return "";
	}

    public void SetClef(string clefName, int staffLine)
    {
        audioSource.Play(0);
        if (staffLine < 1 || staffLine > 5)
        {
            Debug.LogError("Invalid range for clef positions", gameObject);
            return;
        }
        switch(clefName)
        {
            case "g":
                clefSpriteRenderer.sprite = clefSpriteG;
                clefSpriteRenderer.color = Color.cyan;
                break;
            case "f":
                clefSpriteRenderer.sprite = clefSpriteF;
                clefSpriteRenderer.color = Color.red;
                break;
            case "c":
                clefSpriteRenderer.sprite = clefSpriteC;
                clefSpriteRenderer.color = Color.green;
                break;
            default:
                Debug.LogError("Invalid clef name", gameObject);
                return;
        }
        currentClefName = clefName;
        currentClefStaffLine = staffLine;
        currentClef = clefName + "_" + staffLine;
        clefObject.transform.localPosition = new Vector3(clefObject.transform.localPosition.x, (float)staffLine - 3f, 0f);
    }

    public void SetClef(string clef)
    {
        if (!IsValidClef(clef))
        {
            Debug.LogError(clef + " is an invalid clef string");
            return;
        }
        string[] tokens = clef.Split('_');
        int line = 0;
        int.TryParse(tokens[1], out line);
        SetClef(tokens[0], line);
    }

    public string GetClef()
    {
        return currentClef;
    }

    public string GetClefName()
    {
        return currentClefName;
    }

    public int GetClefStaffLine()
    {
        return currentClefStaffLine;
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

    public string GenerateClefForNote()
    {
        string [] clefs = {"g_2", "f_4", "c_3"};
        float rand = Random.Range(0f, 1f);
        int clef = 0;
        if (rand <= probG2)
        {
            clef = 0;
        }
        else if (rand <= probF4)
        {
            clef = 1;
        }
        else
        {
            clef = 2;
        }
        return clefs[clef];
    }
}
