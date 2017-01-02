using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniMission : MonoBehaviour {

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

    public bool randomitem = false;
    public int randomitemcountMax = 1;
    public int itemrar = 10;

    public List<string> dropitems = new List<string>();

    public void LoadMission()
    {
        GameObject PlayerActualShip = GameObject.Find("PlayerActualShip").gameObject;
        ContextManagerGamePro.Instance().loadedmission = this;
        
        GameObject shipcontextobj = GameObject.Find("GameContext").transform.GetChild(0).gameObject;
        Debug.Log(shipcontextobj.name);
        PlayerActualShip.transform.GetChild(0).transform.SetParent(shipcontextobj.transform);

        if (randomitem)
        {
            dropitems.AddRange(ContextManagerGamePro.Instance().StaticMetods.GetItemFromList(itemrar, randomitemcountMax));
        }

        SceneManager.LoadScene(ID);
    }

    public void RandomItem()
    {

    }
}
