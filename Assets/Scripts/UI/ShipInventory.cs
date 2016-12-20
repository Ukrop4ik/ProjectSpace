using UnityEngine;
using System.Collections;

public class ShipInventory : MonoBehaviour
{
    public Transform stationinventory;
    public Transform content;
    public StationUI stationUI;
    public int maxitem = 0;
    public int itemcount = 0;
    public InventoryPanel inventory;

    void Start()
    {
        stationUI = GameObject.Find("StationUI").GetComponent<StationUI>();
       // inventory = transform.GetChild(0).gameObject.GetComponent<InventoryPanel>();
    }

    public void TakeAll()
    {
        content = ContextManagerGamePro.Instance().cargo;
        int count = content.childCount;
        if (content.childCount < 1) return;


        for (int i = 0; i < count; i++)
        {
            GameObject item = content.GetChild(0).gameObject;
            item.transform.SetParent(stationinventory);
            item.transform.localScale = Vector3.one;
            item.transform.rotation = Quaternion.Euler(0, 0, 0);
            inventory.CreateStack();
        }

    }

}
