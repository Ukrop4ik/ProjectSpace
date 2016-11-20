using UnityEngine;
using LitJson;
using System.IO;
using System.Collections.Generic;
using System;

public class SaveManager : MonoBehaviour {

    public static void SaveProfile(string name)
    {

        // заполнение профиля данными
        List<int> stats = new List<int>();
        stats.Add(ContextManagerGamePro.Instance().Profile.credits);
        stats.Add(ContextManagerGamePro.Instance().Profile.fame);
        List<ItemObj> items = new List<ItemObj>();
        Dictionary<string, int> ammos = new Dictionary<string, int>();
        List<string> PlayerShipsList = new List<string>();
        ObjToSave obj = new ObjToSave(name, stats, items, ammos, PlayerShipsList, DateTime.Now);
        JsonData jsonData = JsonMapper.ToJson(obj);
        File.WriteAllText(Application.persistentDataPath + "/" + name + ".json", jsonData.ToString());
        Debug.Log("Profile Save - " + "Name: " + obj.ProfileName + " path: " + Application.persistentDataPath + "/" + name + ".json");
    }

    public static void LoadProfile()
    {
        Profile profile = ContextManagerGamePro.Instance().Profile;
        string jsonstring = File.ReadAllText(Application.persistentDataPath + "/" + profile.profilename + ".json");
        JsonData data = JsonMapper.ToObject(jsonstring);
        profile.credits = (int)data["ProfileStats"][0];
        profile.fame = (int)data["ProfileStats"][1];
    }

    public static void CreateEmptyProfile(string name)
    {
        List<int> stats = new List<int>();
        List<ItemObj> items = new List<ItemObj>();
        Dictionary<string, int> ammos = new Dictionary<string, int>();
        List<string> PlayerShipsList = new List<string>();
        ObjToSave obj = new ObjToSave(name, stats, items, ammos, PlayerShipsList, DateTime.Now);
        JsonData jsonData = JsonMapper.ToJson(obj);
        File.WriteAllText(Application.persistentDataPath + "/" + name + ".json", jsonData.ToString());
        Debug.Log("Profile Save - " + "Name: " + obj.ProfileName + " path: " + Application.persistentDataPath + "/" + name + ".json");
    }

    public class ObjToSave
    {

        public string ProfileName = "";
        public List<int> ProfileStats;
        public List<ItemObj> Items;
        public Dictionary<string, int> Ammos;
        public List<string> PlayerShipsList;
        public DateTime SaveDate;
        public ObjToSave()
        {

        }

        public ObjToSave(string profilename, List<int> stats, List<ItemObj> items, Dictionary<string, int> ammos, List<string> PlayerShipsList, DateTime savetime)
        {
            this.ProfileName = profilename;
            this.ProfileStats = stats;
            this.Items = items;
            this.Ammos = ammos;
            this.PlayerShipsList = PlayerShipsList;
            this.SaveDate = savetime;
 
        }       
    }

    public class ItemObj
    {
        public string itemID;

        public ItemObj()
        {
        }
        public ItemObj(string id)
        {
            this.itemID = id;
        }
    }

    public class PlayerShipObj
    {

    }


}

