using UnityEngine;
using LitJson;
using System.IO;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour {


    public static void SaveProfile(string name)
    {
        List<int> stats = new List<int>();
        List<ItemObj> items = new List<ItemObj>();
        Dictionary<string, int> ammos = new Dictionary<string, int>();
        List<string> PlayerShipsList = new List<string>();

        ObjToSave obj = new ObjToSave(name, stats, items, ammos, PlayerShipsList);
        JsonData jsonData = JsonMapper.ToJson(obj);
        File.WriteAllText(Application.dataPath + "/" + name + ".json", jsonData.ToString());
        Debug.Log("Profile Save - " + "Name: " + obj.ProfileName);
    }

    public class ObjToSave
    {

        public string ProfileName = "";
        public List<int> ProfileStats;
        public List<ItemObj> Items;
        public Dictionary<string, int> Ammos;
        public List<string> PlayerShipsList;
        public ObjToSave()
        {

        }

        public ObjToSave(string profilename, List<int> stats, List<ItemObj> items, Dictionary<string, int> ammos, List<string> PlayerShipsList)
        {
            this.ProfileName = profilename;
            this.ProfileStats = stats;
            this.Items = items;
            this.Ammos = ammos;
            this.PlayerShipsList = PlayerShipsList;
 
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

