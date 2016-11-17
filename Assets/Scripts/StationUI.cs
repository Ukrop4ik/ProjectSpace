using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StationUI : MonoBehaviour {

    public GameObject panelslot;
    public Image shipeqipimage;
    GameObject WeaponSlotPanel;
    GameObject EngineerSlotPanel;
    GameObject PlayerShipStorage;
    GameObject PlayerActualShip;
    ShipInventory shipinventory;
    public GameObject stationinventory;
    public Ship ship;
    bool CargoOpen;

    List<GameObject> listtodel = new List<GameObject>();

    void Start()
    {
        CargoOpen = false;
        stationinventory = gameObject.transform.Find("StationInventory").gameObject;
        shipinventory = gameObject.transform.Find("ShipInventory").gameObject.GetComponent<ShipInventory>();
        WeaponSlotPanel = gameObject.transform.Find("ShipEqipPanel/WeaponSlotPanel").gameObject;
        EngineerSlotPanel = gameObject.transform.Find("ShipEqipPanel/EngineerSlotPanel").gameObject;
        PlayerShipStorage = gameObject.transform.Find("PlayerShipStorage").gameObject;
        PlayerActualShip = gameObject.transform.Find("PlayerActualShip").gameObject;

        CreateSlotFromShip();
        AddShip();
    }

    [ContextMenu("CreateSlot")]
    public void CreateSlotFromShip()
    {
        foreach (GameObject obj in listtodel)
        {
            Destroy(obj);
        }

        listtodel.Clear();

        foreach (ComponentSlot slot in PlayerActualShip.transform.GetChild(0).GetComponent<ComponentController>().Slots)
        {
            if (slot.SlotType == ComponentSlot.slotType.Engineer)
            {
                GameObject slotp = Instantiate(panelslot);
                listtodel.Add(slotp);
                slotp.name = "DropZone";
                slotp.transform.SetParent(EngineerSlotPanel.transform);
                slotp.transform.position = Vector2.zero;
                slotp.transform.localScale = Vector2.one;
                slotp.GetComponent<PanelEqipSlot>().slot = slot;
                slotp.GetComponent<PanelEqipSlot>().SlotType = PanelEqipSlot.slotType.Engineer;
            }
            if (slot.SlotType == ComponentSlot.slotType.Weapon)
            {          
                GameObject slotp = Instantiate(panelslot);
                listtodel.Add(slotp);
                slotp.name = "DropZone";
                slotp.transform.SetParent(WeaponSlotPanel.transform);
                slotp.transform.position = Vector2.zero;
                slotp.transform.localScale = Vector2.one;
                slotp.GetComponent<PanelEqipSlot>().slot = slot;
                slotp.GetComponent<PanelEqipSlot>().SlotType = PanelEqipSlot.slotType.Weapon;
            }
        }      
    }

    public void PressCargoButton()
    {       
        if (!CargoOpen)
        {
            shipinventory.gameObject.SetActive(true);
            stationinventory.gameObject.SetActive(true);
            shipinventory.Open();
        }
        else
        {
            shipinventory.gameObject.SetActive(false);
            shipinventory.Close();
        }
        CargoOpen = !CargoOpen;
    }

    public void AddShip()
    {
        ship = PlayerActualShip.transform.GetChild(0).gameObject.GetComponent<Ship>();
    }

}
