using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMission : MonoBehaviour {

    public Button missionbutton;
    public GameObject minimission_panel;

    public string ID;
    public string mission_name;
    public int danger;
    public int fame;
    public int FC;
    public int days;

    public int fameconditions;
    public int dayconditions;

    public List<string> dropitems = new List<string>();
}
