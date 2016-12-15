﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LootPanel : MonoBehaviour
{


    public GameObject lootlistpanel;
    public CargoContainer conteiner;
    public SceneRes sceneres;

    void Start()
    {
        
    }


    void OnEnable()
    {
       
    }

    public void OnDisable()
    {

    }
    public void OnFinal()
    {

        GameObject obj = GameObject.Find("GameContext");

        SceneBuilder scene = GameObject.Find("Scene").GetComponent<SceneBuilder>();

        ContextManagerGamePro.Instance().playership.transform.SetParent(obj.transform.GetChild(0).transform);
        ContextManagerGamePro.Instance().navpoint = Vector3.zero;
        ContextManagerGamePro.Instance().PreviousScene = scene.SceneName;
        ContextManagerGamePro.Instance().Profile.fame += scene.SceneFame;
        SceneManager.LoadScene("Station");

        Debug.Log("MISSION END ...... LOAD STATION!");
    }

    public void Drop()
    {
        sceneres = GameObject.Find("Scene").GetComponent<SceneRes>();
        if (sceneres.dropitems.Count > 0)
            foreach (Item item in sceneres.dropitems)
            {
                GameObject i = Instantiate(Resources.Load("items/" + item.ItemId)) as GameObject;
                i.transform.SetParent(lootlistpanel.transform);
                i.transform.localScale = Vector3.one;
            }
    }
}
