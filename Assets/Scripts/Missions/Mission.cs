using UnityEngine;
using System.Collections.Generic;

public class Mission : MonoBehaviour {

    static RadioMessage RadioMessageObj;
    public static SpaceUI UI;
    public static SceneBuilder MissionBuilder;
    public static SceneRes MissionRes;
    public static Ship playership;
    static List<RadioData> radiolist = new List<RadioData>();
    static Queue<RadioData> radioQueue = new Queue<RadioData>();

    void Start()
    {
        Invoke("Init", 1f);
    }

    void Update()
    {
        
    }

    void Step()
    {
        Debug.Log("Mission step");

        if (radioQueue.Count > 0)
        {
            var radio = radioQueue.Peek();
            if (radio.isCreate == false)
            {
                RadioMessageObj.Create(radio.headerID, radio.radioID, radio.sprite);
                radio.isCreate = true;
            }

            if (radio.timetoshow > 0)
            {
                radio.timetoshow--;
            }
            else
            {
                RadioMessageObj.gameObject.SetActive(false);
                radioQueue.Dequeue();
            }
        }
    }


    public void Init()
    {
        MissionBuilder = GameObject.Find("Scene").GetComponent<SceneBuilder>();
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<SpaceUI>();
        MissionRes = GameObject.Find("Scene").GetComponent<SceneRes>();
        playership = MissionRes.playershiptransform.gameObject.GetComponent<Ship>();
        RadioMessageObj = UI.RadioMessage;

        InvokeRepeating("Step",0, 1f);
    }

    public static void RadioMessage(string headerID, string radioID, float timetoshow, Sprite sprite)
    {
        radioQueue.Enqueue(new RadioData(headerID, radioID, timetoshow, sprite));
    }
    public static void SetCondition(WinConditionEnum condition)
    {
        MissionBuilder.condition = condition;
    }
    public  bool IsPlayerInArea(string areaname)
    {
        if (!playership) return false; 
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

    public static void CreateNavigationArrow(string actiongroup, ArrowTypeEnum type)
    {
        foreach (GameObject ship in Mission.MissionRes.enemis)
        {
            if (!ship) continue;
            if (ship.GetComponent<Ship>().actiongroup == actiongroup)
            {
                ship.gameObject.GetComponent<AIship>().CreateArrow(type);
            }
        }
    }

    public static void CreateNavigationArrowToObj(string objectname, ArrowTypeEnum type)
    {
        UI.CreateArrow(playership.gameObject, GameObject.Find(objectname), type);
    }

    public static int GetPlayerShild()
    {
        return playership.shield;
    }
    public static int GetPlayerHP()
    {
        return playership.HP;
    }
    public static Ship GetPlayerShip()
    {
        return playership;
    }
    public static void DamagePlayerHP(float relativevalue)
    {
        int playerHP = (int)(GetPlayerHP() * relativevalue);
        playership.HP = playerHP;
    }

    public static List<Ship> GetShips(string actiongroup)
    {
        List<Ship> ships = new List<Ship>();
        foreach (GameObject ship in Mission.MissionRes.enemis)
        {
            if (!ship) continue;
            if (ship.GetComponent<Ship>().actiongroup == actiongroup)
            {
                ships.Add(ship.GetComponent<Ship>());
            }
        }

        return ships;
    }

    public static void ShowShip(string actiongroup)
    {
        foreach (GameObject ship in Mission.MissionRes.enemis)
        {
            if (!ship) continue;
            if (ship.GetComponent<Ship>().actiongroup == actiongroup)
            {
                ship.gameObject.SetActive(true);
            }
        }
    }
    public static void HideShip(string actiongroup)
    {
        foreach (GameObject ship in Mission.MissionRes.enemis)
        {
            if (!ship) continue;
            if (ship.GetComponent<Ship>().actiongroup == actiongroup)
            {
                ship.gameObject.SetActive(false);
            }
        }
    }
    public static void DestroyArea(string area)
    {
        Destroy(GameObject.Find(area));
    }

    public static void CreateUiMarker(string objectname, MarkerTypeEnum type)
    {
        GameObject marker = Instantiate(UI.UImarker);
        marker.GetComponent<UiMarker>().target = GameObject.Find(objectname);
        marker.transform.SetParent(UI.transform);
        marker.transform.localScale = Vector3.one;
    }

    public static void ShipCommand(string actiongroup, ShipLogicEnum command)
    {
        foreach (GameObject ship in Mission.MissionRes.enemis)
        {
            if (!ship) continue;
            if (ship.GetComponent<Ship>().actiongroup == actiongroup)
            {
                ship.gameObject.GetComponent<AIship>().Logic = command;
            }
        }
    }

    class RadioData
    {
        public string headerID;
        public string radioID;
        public float timetoshow;
        public Sprite sprite;
        public bool isCreate;
        public bool isEnd;

        public RadioData(string headerID, string radioID, float timetoshow, Sprite sprite)
        {
            isCreate = false;
            isEnd = false;
            this.headerID = headerID;
            this.radioID = radioID;
            this.timetoshow = timetoshow;
            this.sprite = sprite;
        }
    }
}
