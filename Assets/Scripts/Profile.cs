using UnityEngine;
using System.Collections.Generic;

public class Profile : MonoBehaviour {

    public bool created = false;
    public bool isNew = true;
    public bool isTutorial = true;
    public bool isCheat = false;
    public string profilename;
    public int credits = 0;
    public int fame = 0;
    public GameObject defaultship { get; private set; }
    public List<string> playershipsIdlist { get; private set; }
	// Use this for initialization
	void Start () {

        playershipsIdlist = new List<string>();
        defaultship = Resources.Load("starttership") as GameObject;
        const bool test = false;
        if (test)
        {
            created = true;
            profilename = "test";
        }
        DontDestroyOnLoad(this.gameObject);

	}

    public void AddNewShipToPlayer(string shipId)
    {
        if (playershipsIdlist.Contains(shipId)) return;
        playershipsIdlist.Add(shipId);
    }
}
