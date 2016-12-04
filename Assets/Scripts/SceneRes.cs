using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneRes : MonoBehaviour {

    public List<GameObject> enemis = new List<GameObject>();
    SceneBuilder scene;
    public List<Item> dropitems = new List<Item>();
    public SpaceUI spaceUI;

    void Start ()
    {
        scene = gameObject.GetComponent<SceneBuilder>();
        LoadBattleUI();
        InvokeRepeating("WinCondition", 0, 1f);
        InvokeRepeating("Defeat", 0, 1f);
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
        Transform shipT = shipcontextobj.transform.GetChild(0);
        shipcontextobj.transform.GetChild(0).transform.SetParent(null);    
        shipT.position = Vector3.zero;
    }
    public void WinCondition()
    {

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
    public void Win()
    {
        spaceUI.WinPanel.SetActive(true);
    }
    void KillAllCondition()
    {
        if (enemis.Count == 0)
        {
            Win();
        }
    }
    void CustomCondition()
    {

    }
}
