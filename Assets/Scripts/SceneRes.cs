using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneRes : MonoBehaviour {


	void Start () {
        LoadBattleUI();
    }


    void LoadBattleUI()
    {
        Instantiate(Resources.Load("SpaceUI") as GameObject);
        CreatePlayership();
    }
    void CreatePlayership()
    {
        GameObject shipcontextobj = GameObject.Find("GameContext").transform.GetChild(0).gameObject;
        shipcontextobj.transform.GetChild(0).transform.SetParent(null);
    }
}
