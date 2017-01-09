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
    public GameObject ObjectivePrefab;
    public static GameObject OPrefab;
    static Dictionary<string, ObjectiveData> ObjectiveDataList = new Dictionary<string, ObjectiveData>();

    void Start()
    {
        Invoke("Init", 1f);
        OPrefab = ObjectivePrefab;
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
    public static void Win()
    {
        ObjectiveDataList.Clear();
        MissionRes.win = true;
        UI.WinPanel.SetActive(true);
        UI.WinPanel.GetComponent<LootPanel>().Drop();
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

    #region "Objectives"
    public static void CreateObjective(string id, int value = 0)
    {
        GameObject obj = Instantiate(OPrefab);
        obj.transform.SetParent(GameObject.Find("ObjectivesPanel").transform);
        obj.transform.localScale = Vector3.one;

        Objectiv script = obj.GetComponent<Objectiv>();
        script.CreateText(id);
        obj.GetComponent<Localization>().Id = id;
        obj.GetComponent<Localization>().CreateText(id);
        script.CreateCounter(value);

        ObjectiveData data = new ObjectiveData(id, value, obj, script);
        ObjectiveDataList.Add(id, data);

    }
    public static void SetObjectiveCounter(string id, int value)
    {
        ObjectiveData data;
        ObjectiveDataList.TryGetValue(id, out data);
        data.objectiv.UpdateCounter(data.countermax - value);
    }
    public static void ObjectiveComplite(string id)
    {
        ObjectiveData data;
        ObjectiveDataList.TryGetValue(id, out data);
        data.objectiv.End();
    }
    public static void ObjectiveFail(string id)
    {
        ObjectiveData data;
        ObjectiveDataList.TryGetValue(id, out data);
        data.objectiv.Cancel();
    }
    #endregion

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

    public static void FactionVsFaction(FactionEnum factionA, FactionEnum factionB)
    {
        List<GameObject> factionAships = new List<GameObject>();
        List<GameObject> factionBships = new List<GameObject>();

        switch (factionA)
        {
            case FactionEnum.Pirate:
                factionAships.AddRange(MissionRes.pirateships);
                break;
            case FactionEnum.Guard:
                factionAships.AddRange(MissionRes.guardships);
                break;
            case FactionEnum.Prisoner:
                factionAships.AddRange(MissionRes.prisonerships);
                break;
            default:
                break;
        }

        switch (factionB)
        {
            case FactionEnum.Pirate:
                factionBships.AddRange(MissionRes.pirateships);
                break;
            case FactionEnum.Guard:
                factionBships.AddRange(MissionRes.guardships);
                break;
            case FactionEnum.Prisoner:
                factionBships.AddRange(MissionRes.prisonerships);
                break;
            default:
                break;
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

    class ObjectiveData
    {
        public string ID;
        public int countermax;
        public GameObject obj;
        public Objectiv objectiv;

        public ObjectiveData(string id, int maxcounter, GameObject objobjective, Objectiv script)
        {
            ID = id;
            countermax = maxcounter;
            obj = objobjective;
            objectiv = script;
        }
    }


}
