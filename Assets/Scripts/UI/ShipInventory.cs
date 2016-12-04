using UnityEngine;
using System.Collections;

public class ShipInventory : MonoBehaviour
{

    public GameObject content;
    public StationUI stationUI;
    public int maxitem = 0;
    public int itemcount = 0;
    InventoryPanel inventory;

    void Start()
    {
        stationUI = transform.parent.gameObject.GetComponent<StationUI>();
        inventory = transform.GetChild(0).gameObject.GetComponent<InventoryPanel>();
    }

    void Update()
    {

        itemcount = content.transform.childCount;

        if (itemcount < maxitem)
        {
            inventory.isCanDropped = true;
        }
        else
        {
            inventory.isCanDropped = false;
        }

    }

    public void Open()
    {
        maxitem = stationUI.ship.cargo.Volume;
        int count = stationUI.ship.cargo.cargoobj.transform.childCount;
        itemcount = count;
        for (int i = 0; i < count; i++)
        {
            GameObject item = stationUI.ship.cargo.cargoobj.transform.GetChild(0).gameObject;
            item.transform.SetParent(content.transform);
            stationUI.ship.cargo.itemsincargo.Remove(item.GetComponent<Item>());
            item.transform.localScale = Vector3.one;
            item.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
    public void Close()
    {
        int count = content.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject item = content.transform.GetChild(0).gameObject;
            item.transform.SetParent(stationUI.ship.cargo.cargoobj.transform);
            stationUI.ship.cargo.itemsincargo.Add(item.GetComponent<Item>());
            item.transform.localScale = Vector3.zero;
            item.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void TakeAll()
    {
        int count = content.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject item = content.transform.GetChild(0).gameObject;
            item.transform.SetParent(stationUI.stationinventory.transform.GetChild(0).GetComponent<InventoryPanel>().contentpanel.transform);
            stationUI.ship.cargo.itemsincargo.Remove(item.GetComponent<Item>());
            item.transform.localScale = Vector3.one;
            item.transform.rotation = Quaternion.Euler(0, 0, 0);
            stationUI.stationinventory.transform.GetChild(0).GetComponent<InventoryPanel>().CreateStack();
        }

    }

}
