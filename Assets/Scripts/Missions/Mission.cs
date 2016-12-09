using UnityEngine;
using System.Collections.Generic;

public class Mission : MonoBehaviour {

    static RadioMessage RadioMessageObj;
    public static SpaceUI UI;
    public static SceneBuilder MissionBuilder;
    public static SceneRes MissionRes;
    public static Ship playership;
    void Start()
    {
        Invoke("Init", 1f);
    }

    public void Init()
    {
        MissionBuilder = GameObject.Find("Scene").GetComponent<SceneBuilder>();
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<SpaceUI>();
        MissionRes = GameObject.Find("Scene").GetComponent<SceneRes>();
        playership = MissionRes.playershiptransform.gameObject.GetComponent<Ship>();
        RadioMessageObj = UI.RadioMessage;
        
        
    }

    public static void RadioMessage(string headerID, string radioID, float timetoshow, Sprite sprite)
    {
        RadioMessageObj.Create(headerID, radioID, timetoshow, sprite);
    }
    public static void SetCondition(WinConditionEnum condition)
    {
        MissionBuilder.condition = condition;
    }
    public  bool IsPlayerInArea(string areaname)
    {
        GameObject areaobj = GameObject.Find(areaname);
        if (areaobj == null)
        {
            Debug.LogError("Cant find area " + areaname + " in scene");
            return false;
        }
        TriggerArea area = areaobj.GetComponent<TriggerArea>();
        float Range;
        Range = area.Radius;

        return Vector3.Distance(playership.transform.position, areaobj.transform.position) < Range;
    }
}
