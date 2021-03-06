﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class StationUI : MonoBehaviour {

    public GameObject escape;
    public Text day;
    public Text crd;
    public GameObject panelslot;
    public Image shipeqipimage;
    GameObject WeaponSlotPanel;
    GameObject EngineerSlotPanel;
    public GameObject PlayerActualShip;
    public ShipInventory shipinventory;
    public GameObject stationinventory;
    public Ship ship;
    bool CargoOpen;
    Profile profile;
    List<GameObject> listtodel = new List<GameObject>();
    void Start()
    {
        profile = ContextManagerGamePro.Instance().Profile;
        CargoOpen = false;
        stationinventory = gameObject.transform.Find("StationInventory").gameObject;
        WeaponSlotPanel = gameObject.transform.Find("ShipEqipPanel/WeaponSlotPanel").gameObject;
        EngineerSlotPanel = gameObject.transform.Find("ShipEqipPanel/EngineerSlotPanel").gameObject;
        PlayerActualShip = gameObject.transform.Find("PlayerActualShip").gameObject;
        AddShip();
    }
    void Update()
    {
        day.text = profile.Day.ToString();
        crd.text = profile.credits.ToString();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escape.SetActive(true);
        }
    }

    public void OpenCraftPanel()
    {
        GameObject obj = transform.Find("StationInventory").gameObject;
        GameObject objcraft = transform.Find("Craft").gameObject;

        if (obj.activeInHierarchy && objcraft.activeInHierarchy)
        {

            obj.SetActive(false);
            objcraft.SetActive(false);
            return;
        }
        else if (obj.activeInHierarchy)
        {
            objcraft.SetActive(true);
            obj.SetActive(true);
            return;
        }
        else
        {
            objcraft.SetActive(true);
            obj.SetActive(true);
            return;
        }
        
    }
    public void OpenShipPanel()
    {
        GameObject obj = transform.Find("StationInventory").gameObject;
        GameObject obj2 = transform.Find("ShipEqipPanel").gameObject;

        if (obj.activeInHierarchy && obj2.activeInHierarchy)
        {

            obj.SetActive(false);
            obj2.SetActive(false);
            return;
        }
        else if (obj.activeInHierarchy)
        {
            obj2.SetActive(true);
            obj.SetActive(true);
            return;
        }
        else
        {
            obj2.SetActive(true);
            obj.SetActive(true);
            return;
        }
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
                slotp.GetComponent<PanelEqipSlot>().SlotType = SlotTypeEnum.Engineer;

                if (slot.component != null)
                {
                    GameObject itemclone = Instantiate(Resources.Load("items/" + slot.component.transform.parent.gameObject.GetComponent<Item>().ItemId)) as GameObject;
                    itemclone.name = slot.component.gameObject.name;
                    itemclone.GetComponent<Item>().isClone = true;
                    itemclone.GetComponent<Item>().itemoriginal = slot.component.transform.parent.gameObject;
                    itemclone.transform.SetParent(slotp.transform);
                    itemclone.transform.localScale = Vector3.one;
                    itemclone.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    itemclone.GetComponent<Item>().space = ItemSpaceEnum.EquipSlot;
                }
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
                slotp.GetComponent<PanelEqipSlot>().SlotType = SlotTypeEnum.Weapon;

                if (slot.weapon != null)
                {
                    GameObject itemclone = Instantiate(Resources.Load("items/" + slot.weapon.transform.parent.gameObject.GetComponent<Item>().ItemId)) as GameObject;
                    itemclone.name = slot.weapon.gameObject.name;
                    itemclone.GetComponent<Item>().isClone = true;
                    itemclone.GetComponent<Item>().itemoriginal = slot.weapon.transform.parent.gameObject;
                    itemclone.transform.SetParent(slotp.transform);
                    itemclone.transform.localScale = Vector3.one;
                    itemclone.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    itemclone.GetComponent<Item>().space = ItemSpaceEnum.EquipSlot;
                }
            }
        }      
    }
    public void AddShip()
    {
        GameObject shipcontextobj = GameObject.Find("GameContext").transform.GetChild(0).gameObject;

        if (PlayerActualShip.transform.childCount > 0)
        {
            ship = PlayerActualShip.transform.GetChild(0).gameObject.GetComponent<Ship>();
            ContextManagerGamePro.Instance().playership = ship;
        }
        else if (shipcontextobj.transform.childCount > 0)
        {
            ship = shipcontextobj.transform.GetChild(0).gameObject.GetComponent<Ship>();
            ship.gameObject.transform.SetParent(PlayerActualShip.transform);
            ContextManagerGamePro.Instance().playership = ship;
        }

    }
    public void TestBattle()
    {
        GameObject shipcontextobj = GameObject.Find("GameContext").transform.GetChild(0).gameObject;
        Debug.Log(shipcontextobj.name);
        PlayerActualShip.transform.GetChild(0).transform.SetParent(shipcontextobj.transform);

        SceneManager.LoadScene("Tutorial");
    }

}
