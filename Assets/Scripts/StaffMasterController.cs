using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMasterController : MonoBehaviour {
	public StaffController staffPrefab;
	private GameMasterController gameMaster;
	private InputHandlerController inputHandler;
	private NoteMasterController noteMaster;
	private List<StaffController> staffList;
	private Dictionary<string, Dictionary<float, string> > clefDictionary;
	private int difficulty;
    private float clefRotationTimeout;
    private float staffRotationTimeout;
	private float timeElapsed;
	private bool initialStaffSet;

	private void Awake ()
	{
		gameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterController>();
		inputHandler = GameObject.Find("InputHandler").GetComponent<InputHandlerController>();
		noteMaster = GameObject.Find("NoteMaster").GetComponent<NoteMasterController>();
		staffList = new List<StaffController>();
		InitDict();
		initialStaffSet = false;
		difficulty = 0;
        clefRotationTimeout = 2f;
		timeElapsed = 0f;
	}

	private void Update ()
	{
		// If the GameMaster has not set an initial staff, just wait
		if (!initialStaffSet) return;
		timeElapsed += Time.deltaTime;
        if (timeElapsed > clefRotationTimeout)
        {
            timeElapsed = 0f;
            string[] clefs = new string[] {"g_2", "f_4", "c_3"};
            int clefId = Random.Range(0, 3);
            int staffId = Random.Range(0, staffList.Count);
            staffList[staffId].SetClef(clefs[clefId]);
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

	private void OrderStaffs()
	{
		int numberOfStaffs = staffList.Count;
		if (numberOfStaffs == 1)
		{
			staffList[0].transform.localPosition = Vector3.zero;
		}
		if (numberOfStaffs == 2)
		{
			staffList[0].transform.localPosition = new Vector3(0f, -4f, 0f);
			staffList[1].transform.localPosition = new Vector3(0f, 4f, 0f);
		}
	}

	public List<StaffController> GetStaffList()
	{
		return staffList;
	}

    public List<string> GetStaffListString()
    {
        List<string> staffListString = new List<string>();
        foreach (StaffController staff in staffList)
        {
            staffListString.Add(staff.GetClefString());
        }
        return staffListString;
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

	public void EventSetInitialStaff(List<string> clefs, GameObject caller)
    {
        if (caller == gameMaster.gameObject)
        {
            Debug.Log("Setting the inital staff", gameObject);
			foreach (string clef in clefs)
			{
				StaffController staff = Instantiate(staffPrefab, transform).GetComponent<StaffController>();
				staff.SetClef(clef);
				staffList.Add(staff);
			}
			OrderStaffs();
			initialStaffSet = true;
			gameMaster.EventStaffReady(gameObject);
        }
        else
        {
            Debug.LogError("I only listen to EventSetInitialStaff() calls from the GameMaster", gameObject);
        }
    }

    public bool IsLatestStaffList(List<string> staffListString)
    {
        if (staffList.Count != staffListString.Count) return false;

        bool areEqual = true;
        for (int i = 0; i < staffList.Count; i++)
        {
            // Debug.Log("staffList[" + i + "]: " + staffList[i].GetClefString() + ", other[" + i + "]: " + otherStaffList[i].GetClefString());
            if (!(staffList[i].GetClefString() == staffListString[i]))
            {
                areEqual = false;
                break;
            }
        }
        return areEqual;

    }
}
