using UnityEngine;
using LitJson;
using System.IO;
using System.Collections.Generic;
using System;

public class SaveManager : MonoBehaviour {

    public static void SaveProfile(string name, Ship ship)
    {
        JsonData jsonData;
        // заполнение профиля данными
        bool isTutorial = ContextManagerGamePro.Instance().Profile.isTutorial;
        bool isCheat = ContextManagerGamePro.Instance().Profile.isCheat;
        List <int> stats = new List<int>();
        stats.Add(ContextManagerGamePro.Instance().Profile.credits);
        stats.Add(ContextManagerGamePro.Instance().Profile.fame);
        List<ItemObj> items = new List<ItemObj>();
        GameObject inventory = GameObject.Find("StationUI").GetComponent<StationUI>().stationinventory.transform.GetChild(0).transform.GetChild(0).gameObject;
        if (inventory.transform.childCount > 0)
        {
            for (int i = 0; i < inventory.transform.childCount; i++)
            {
                items.Add(new ItemObj(inventory.transform.GetChild(i).GetComponent<Item>().ItemId));
            }
        }
        Dictionary<string, int> ammos = new Dictionary<string, int>();
        List<string> PlayerShipsList = new List<string>();
        if (ContextManagerGamePro.Instance().Profile.playershipsIdlist.Count > 0)
        {
            PlayerShipsList.AddRange(ContextManagerGamePro.Instance().Profile.playershipsIdlist);
        }
        //заполнение данных корабля
        List<ItemInSlot> itemsinship = new List<ItemInSlot>();
        List<string> itemsincargo = new List<string>();
        foreach (ComponentSlot slot in ship.ComponentController.Slots)
        {
            if (!slot.containitem) continue;

            if (slot.SlotType == ComponentSlot.slotType.Weapon)
            {
                itemsinship.Add(new ItemInSlot(slot.weapon.Id, slot.slotnumber));
            }
            else
            {
                itemsinship.Add(new ItemInSlot(slot.component.component_name, slot.slotnumber));
            }
            
        }
        foreach (Item item in ship.cargo.itemsincargo)
        {
            if (!item) continue;
            itemsincargo.Add(item.ItemId);
        }
        PlayerShipObj shiptosave = new PlayerShipObj(ship.itemID, itemsinship, itemsincargo);

        ObjToSave obj = new ObjToSave(name, stats, items, ammos, PlayerShipsList, shiptosave, DateTime.Now, false, isTutorial, isCheat);

        jsonData = JsonMapper.ToJson(obj);

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
        List<ItemInSlot> itemsinship = new List<ItemInSlot>();
        List<string> cargoitems = new List<string>();
        PlayerShipObj shipempty = new PlayerShipObj("emptyID", itemsinship, cargoitems);
        ObjToSave obj = new ObjToSave(name, stats, items, ammos, PlayerShipsList, shipempty, DateTime.Now, true, true, false);
        JsonData jsonData = JsonMapper.ToJson(obj);
        File.WriteAllText(Application.persistentDataPath + "/" + name + ".json", jsonData.ToString());
        Debug.Log("Profile Save - " + "Name: " + obj.ProfileName + " path: " + Application.persistentDataPath + "/" + name + ".json");
    }

    //конструкторы данных профиля
    #region "конструкторы данных профиля"
    public class ObjToSave
    {

        public string ProfileName = "";
        public bool isNewProfile;
        public bool isTutorial;
        public bool isCheat;
        public List<int> ProfileStats;
        public List<ItemObj> Items;
        public Dictionary<string, int> Ammos;
        public List<string> PlayerShipsList;
        public PlayerShipObj PlayerShip;
        public DateTime SaveDate;
        public ObjToSave()
        {

        }

        public ObjToSave(string profilename, List<int> stats, List<ItemObj> items, Dictionary<string, int> ammos, List<string> PlayerShipsList, PlayerShipObj playership, DateTime savetime, bool isNew, bool isTutorial, bool isCheat)
        {
            this.ProfileName = profilename;
            this.isNewProfile = isNew;
            this.isTutorial = isTutorial;
            this.isCheat = isCheat; 
            this.ProfileStats = stats;
            this.Items = items;
            this.Ammos = ammos;
            this.PlayerShipsList = PlayerShipsList;
            this.PlayerShip = playership;
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
    #endregion

    //конструкторы данных корабля
    #region "конструкторы данных корабля"
    public class PlayerShipObj
    {
        public string ShipId;
        public List<ItemInSlot> Items;
        public List<string> ItemsInCargo;

        public PlayerShipObj()
        {

        }

        public PlayerShipObj(string Id, List<ItemInSlot> itemsID, List<string> itemsincargoID)
        {
            this.ShipId = Id;
            this.Items = itemsID;
            this.ItemsInCargo = itemsincargoID;
        }
    }

    public class ItemInSlot
    {
        public string Id;
        public int slotnumber;

        public ItemInSlot(string id, int number)
        {
            this.Id = id;
            this.slotnumber = number;
        }
    }
    #endregion

}

