using UnityEngine;
using LitJson;
using System.IO;
using System.Collections.Generic;
using System;

public class SaveManager : MonoBehaviour {

    public static void SaveProfile(string name, Ship ship)
    {
        bool needcreatedefaulship = false;
        JsonData jsonData;
        PlayerShipObj shiptosave;
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
        int _day = ContextManagerGamePro.Instance().Profile.Day;

        //заполнение данных корабля
        List<ItemInSlot> itemsinship = new List<ItemInSlot>();
        List<string> itemsincargo = new List<string>();

        if (ContextManagerGamePro.Instance().playership != null)
        {
            foreach (ComponentSlot slot in ship.ComponentController.Slots)
            {
                if (!slot.containitem) continue;

                if (slot.SlotType == ComponentSlot.slotType.Weapon)
                {
                    itemsinship.Add(new ItemInSlot(slot.weapon.Id, slot.slotnumber, slot.weapon.gameObject.GetComponent<ShipComponent>().DataPath));
                }
                else
                {
                    itemsinship.Add(new ItemInSlot(slot.component.component_name, slot.slotnumber, slot.component.DataPath));
                }

            }
            foreach (Item item in ship.cargo.itemsincargo)
            {
                if (!item) continue;
                itemsincargo.Add(item.ItemId);
            }
            shiptosave = new PlayerShipObj(ship.itemID, itemsinship, itemsincargo);
        }
        else
        {
            needcreatedefaulship = true;
            shiptosave = DefaultShip();
        }

        ObjToSave obj = new ObjToSave(name, _day, stats, items, ammos, PlayerShipsList, shiptosave, DateTime.Now, false, isTutorial, isCheat);

        jsonData = JsonMapper.ToJson(obj);

        File.WriteAllText(Application.persistentDataPath + "/" + name + ".json", jsonData.ToString());

        if (needcreatedefaulship)
        {
            Profile profile = ContextManagerGamePro.Instance().Profile;
            JsonData data = DataLoad(profile);

            CreateShipFromFile(data);
        }

        Debug.Log("Profile Save - " + "Name: " + obj.ProfileName + " path: " + Application.persistentDataPath + "/" + name + ".json");
    }

    public static void LoadProfile()
    {

        //загрузка данных профиля
        Profile profile = ContextManagerGamePro.Instance().Profile;
        JsonData data = DataLoad(profile);
        profile.Day = (int)data["Day"];
        profile.credits = (int)data["ProfileStats"][0];
        profile.fame = (int)data["ProfileStats"][1];
        CreateShipFromFile(data);
    }

    private static JsonData DataLoad(Profile profile)
    {
        string jsonstring = File.ReadAllText(Application.persistentDataPath + "/" + profile.profilename + ".json");
        return JsonMapper.ToObject(jsonstring);
    }

    private static void CreateShipFromFile(JsonData data)
    {
        //загрузка текущего корабля
        if (data["PlayerShip"]["ShipId"].ToString() != "emptyID")
        {
            GameObject ship = Instantiate(Resources.Load(data["PlayerShip"]["ShipId"].ToString()) as GameObject);
            ship.name = data["PlayerShip"]["ShipId"].ToString();
            Ship shipscript = ship.GetComponent<Ship>();
            ContextManagerGamePro.Instance().playership = shipscript;

            List<ComponentSlot> Slots = new List<ComponentSlot>();
            List<ItemInSlot> itemsIds = new List<ItemInSlot>();
            Slots.AddRange(ship.GetComponentsInChildren<ComponentSlot>());

            for (int i = 0; i < data["PlayerShip"]["Items"].Count; i++)
            {
                itemsIds.Add(new ItemInSlot(data["PlayerShip"]["Items"][i]["Id"].ToString(), (int)data["PlayerShip"]["Items"][i]["slotnumber"], data["PlayerShip"]["Items"][i]["DataPath"].ToString()));
                Debug.Log(data["PlayerShip"]["Items"][i]["Id"].ToString());
            }

            foreach (ComponentSlot slot in Slots)
            {
                foreach (ItemInSlot item in itemsIds)
                {
                    if (slot.slotnumber == item.slotnumber)
                    {
                        slot.ship = shipscript;
                        GameObject _item = Instantiate(Resources.Load(item.DataPath + item.Id) as GameObject);
                        _item.transform.SetParent(slot.transform);
                        _item.transform.SetSiblingIndex(0);
                        _item.GetComponent<Item>().EqipItem(slot);
                    }
                }
            }

            StationUI UI = GameObject.Find("StationUI").GetComponent<StationUI>();
            UI.ship = shipscript;
            ship.transform.SetParent(UI.PlayerActualShip.transform);
        }
    }

    public static void CreateEmptyProfile(string name)
    {
        List<int> stats = new List<int>();
        stats.Add(1000); //credits
        stats.Add(0); // fame
        List<ItemObj> items = new List<ItemObj>();
        Dictionary<string, int> ammos = new Dictionary<string, int>();
        List<string> PlayerShipsList = new List<string>();
        ObjToSave obj = new ObjToSave(name, 0, stats, items, ammos, PlayerShipsList, DefaultShip(), DateTime.Now, true, true, false);
        JsonData jsonData = JsonMapper.ToJson(obj);
        File.WriteAllText(Application.persistentDataPath + "/" + name + ".json", jsonData.ToString());
        Debug.Log("Profile Save - " + "Name: " + obj.ProfileName + " path: " + Application.persistentDataPath + "/" + name + ".json");
    }

    private static PlayerShipObj DefaultShip()
    {
        List<ItemInSlot> itemsinship = new List<ItemInSlot>();
        List<string> cargoitems = new List<string>();
        itemsinship.Add(new ItemInSlot("SmallKinetikTurretMK1", 3, "ShipComponent/"));
        itemsinship.Add(new ItemInSlot("SmallKinetikTurretMK1", 4, "ShipComponent/"));
        return new PlayerShipObj("starttership", itemsinship, cargoitems);
    }

    //конструкторы данных профиля
    #region "конструкторы данных профиля"
    private class ObjToSave
    {

        public string ProfileName = "";
        public bool isNewProfile;
        public bool isTutorial;
        public bool isCheat;
        public int Day;
        public List<int> ProfileStats;
        public List<ItemObj> Items;
        public Dictionary<string, int> Ammos;
        public List<string> PlayerShipsList;
        public PlayerShipObj PlayerShip;
        public DateTime SaveDate;
        public ObjToSave()
        {

        }

        public ObjToSave(string profilename, int Day, List<int> stats, List<ItemObj> items, Dictionary<string, int> ammos, List<string> PlayerShipsList, PlayerShipObj playership, DateTime savetime, bool isNew, bool isTutorial, bool isCheat)
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
            this.Day = Day;
 
        }       
    }

    private class ItemObj
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
    private class PlayerShipObj
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

    private class ItemInSlot
    {
        public string Id;
        public int slotnumber;
        public string DataPath;

        public ItemInSlot(string id, int number, string path)
        {
            this.Id = id;
            this.slotnumber = number;
            this.DataPath = path;
        }
    }
    #endregion

}

