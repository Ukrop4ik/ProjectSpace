using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneRes : MonoBehaviour {

    public List<GameObject> enemis = new List<GameObject>();
    SceneBuilder scene;
    WinConditionEnum condition;

    void Start ()
    {
        scene = gameObject.GetComponent<SceneBuilder>();
        condition = scene.condition;
        LoadBattleUI();
        InvokeRepeating("WinCondition", 0, 1f);
        InvokeRepeating("Defeat", 0, 1f);
    }
    void LoadBattleUI()
    {
        Instantiate(Resources.Load("SpaceUI") as GameObject);
        CreatePlayership();
    }
    void CreatePlayership()
    {
        GameObject shipcontextobj = GameObject.Find("GameContext").transform.GetChild(0).gameObject;
        Transform shipT = shipcontextobj.transform.GetChild(0);
        shipcontextobj.transform.GetChild(0).transform.SetParent(null);    
        shipT.position = Vector3.zero;
    }
    public void WinCondition()
    {
        switch (condition)
        {
            case WinConditionEnum.KillAll:
                KillAllCondition();
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
    void Win()
    {
        GameObject obj = GameObject.Find("GameContext");
        ContextManagerGamePro.Instance().playership.transform.SetParent(obj.transform.GetChild(0).transform);
        ContextManagerGamePro.Instance().navpoint = Vector3.zero;
        ContextManagerGamePro.Instance().PreviousScene = scene.SceneName;
        ContextManagerGamePro.Instance().Profile.fame += scene.SceneFame;
        SceneManager.LoadScene("Station");
    }
    void KillAllCondition()
    {
        if (enemis.Count == 0)
        {
            Win();
        }
    }
}
