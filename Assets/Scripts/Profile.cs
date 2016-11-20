using UnityEngine;
using System.Collections.Generic;

public class Profile : MonoBehaviour {

    public bool created = false;
    public string profilename;
    public int credits = 0;
    public int fame = 0;
    public GameObject defaultship { get; private set; }

	// Use this for initialization
	void Start () {

        defaultship = Resources.Load("starttership") as GameObject;
        const bool test = false;
        if (test)
        {
            created = true;
            profilename = "test";
        }
        DontDestroyOnLoad(this.gameObject);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
