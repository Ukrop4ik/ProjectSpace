using UnityEngine;
using LitJson;
using System.IO;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour {

    public static void SaveProfile(string name)
    {
        if (!File.Exists(Application.persistentDataPath + "/profilelist.json"))
        {
            ProfileList profiles = new ProfileList(new List<string>() { name });
            JsonData jsonDataProfiles = JsonMapper.ToJson(profiles);
            File.WriteAllText(Application.persistentDataPath + "/profilelist.json", jsonDataProfiles.ToString());
        }
        else
        {
            string jsonstring = File.ReadAllText(Application.persistentDataPath + "/profilelist.json");
            JsonData jsonDataProfiles = JsonMapper.ToObject(jsonstring);

            List<string> newlist = new List<string>();

            for (int i = 0; i < jsonDataProfiles["profilesnames"].Count; i++)
            {
                newlist.Add(jsonDataProfiles["profilesnames"][i].ToString());
            }

            if (newlist.Contains(name))
            {
                Debug.Log("Not Unical Name!");
                return;
            }
            newlist.Add(name);

            ProfileList profiles = new ProfileList(newlist);
            jsonDataProfiles = JsonMapper.ToJson(profiles);
            File.WriteAllText(Application.persistentDataPath + "/profilelist.json", jsonDataProfiles.ToString());
        }

        List<int> stats = new List<int>();
        List<ItemObj> items = new List<ItemObj>();
        Dictionary<string, int> ammos = new Dictionary<string, int>();
        List<string> PlayerShipsList = new List<string>();

        ObjToSave obj = new ObjToSave(name, stats, items, ammos, PlayerShipsList);
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

    public class ProfileList
    {
       public List<string> profilesnames;

        public ProfileList()
        {
        }

        public ProfileList(List<string> names)
        {
            this.profilesnames = names;
        }
    }

}

