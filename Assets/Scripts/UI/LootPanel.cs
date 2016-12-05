using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LootPanel : MonoBehaviour
{


    public GameObject lootlistpanel;
    public CargoContainer conteiner;
    public SceneRes sceneres;


    void OnEnable()
    {



        sceneres = GameObject.Find("Scene").GetComponent<SceneRes>();

        foreach (Item item in sceneres.dropitems)
        {
            GameObject i = Instantiate(Resources.Load(item.DataPath + item.ItemId)) as GameObject;
            i.transform.SetParent(lootlistpanel.transform);
            i.transform.localScale = Vector3.one;
        }

        Time.timeScale = 0;
    }

    public void OnDisable()
    {

    }
    public void OnFinal()
    {
        Time.timeScale = 1;

        GameObject obj = GameObject.Find("GameContext");

        SceneBuilder scene = GameObject.Find("Scene").GetComponent<SceneBuilder>();

        ContextManagerGamePro.Instance().playership.transform.SetParent(obj.transform.GetChild(0).transform);
        ContextManagerGamePro.Instance().navpoint = Vector3.zero;
        ContextManagerGamePro.Instance().PreviousScene = scene.SceneName;
        ContextManagerGamePro.Instance().Profile.fame += scene.SceneFame;
        SceneManager.LoadScene("Station");

        Debug.Log("MISSION END ...... LOAD STATION!");
    }
}
