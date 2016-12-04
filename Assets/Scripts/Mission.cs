using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour {

    static RadioMessage RadioMessageObj;
    SpaceUI UI;
    public static SceneBuilder MissionBuilder;
    public static SceneRes MissionRes;
    void Start()
    {
        Invoke("Init", 1f);
    }

    public void Init()
    {
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<SpaceUI>();
        RadioMessageObj = UI.RadioMessage;
        MissionBuilder = GameObject.Find("Scene").GetComponent<SceneBuilder>();
        MissionRes = GameObject.Find("Scene").GetComponent<SceneRes>();
    }

    public static void RadioMessage(string headerID, string radioID, float timetoshow, Sprite sprite)
    {
        RadioMessageObj.Create(headerID, radioID, timetoshow, sprite);
    }
}
