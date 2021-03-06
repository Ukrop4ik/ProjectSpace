﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneRes : MonoBehaviour {

    public bool generete = false;

    public List<GameObject> enemis = new List<GameObject>();
    SceneBuilder scene;
    public List<GameObject> dropitems = new List<GameObject>();
    public SpaceUI spaceUI;
    public Transform playershiptransform;
    public bool win = false;
    public List<GameObject> pirateships = new List<GameObject>();
    public List<GameObject> prisonerships = new List<GameObject>();
    public List<GameObject> guardships = new List<GameObject>();

    void Start ()
    {


        scene = gameObject.GetComponent<SceneBuilder>();
        LoadBattleUI();
        InvokeRepeating("WinCondition", 0, 1f);
        InvokeRepeating("Defeat", 0, 1f);


        if (generete)
        {
            foreach (string id in ContextManagerGamePro.Instance().loadedmission.dropitems)
            {
                Debug.Log(id);
                dropitems.Add(Instantiate((Resources.Load("items/" + id)) as GameObject));
            }
        }
    }
    void LoadBattleUI()
    {
        GameObject o = Instantiate(Resources.Load("SpaceUI") as GameObject);
        spaceUI = o.GetComponent<SpaceUI>();
        CreatePlayership();
    }
    void CreatePlayership()
    {
        GameObject shipcontextobj = GameObject.Find("GameContext").transform.GetChild(0).gameObject;
        playershiptransform = shipcontextobj.transform.GetChild(0);
        shipcontextobj.transform.GetChild(0).transform.SetParent(null);
        playershiptransform.position = Vector3.zero;
        enemis.Add(playershiptransform.gameObject);
    }
    public void WinCondition()
    {
        if (win) return;
        switch (scene.condition)
        {
            case WinConditionEnum.KillAll:
                KillAllCondition();
                break;

            case WinConditionEnum.Custom:
                break;
            default:
                break;
        }
    }
    public void Defeat()
    {
        if (ContextManagerGamePro.Instance().playership != null) return;
        ContextManagerGamePro.Instance().PreviousScene = scene.SceneName;
        SceneManager.LoadScene("Station");
    }

    void KillAllCondition()
    {
        if (enemis.Count == 0)
        {
 
        }
    }
    void CustomCondition()
    {

    }

    public void CreateUiArrow(GameObject target, ArrowTypeEnum type)
    {
        spaceUI.CreateArrow(playershiptransform.gameObject, target, type);
    }
}
