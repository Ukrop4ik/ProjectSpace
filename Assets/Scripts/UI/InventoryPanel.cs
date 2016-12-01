using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class InventoryPanel : MonoBehaviour, IDropHandler
{
    public GameObject contentpanel;
    public InventoryTypeEnum Type;
    public bool isCanDropped = true;
    void Start()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isCanDropped)
        {
            if (eventData.pointerDrag.tag == "item" && gameObject.name == "DropZone")
            {
                if (eventData.pointerDrag.GetComponent<Item>().isClone)
                {
                    eventData.pointerDrag.transform.SetParent(contentpanel.transform);
                    eventData.pointerDrag.GetComponent<Item>().space = ItemSpaceEnum.Inventory;
                    eventData.pointerDrag.GetComponent<Item>().isClone = false;

                }
            }
        }
        else
        {
            if (eventData.pointerDrag.tag == "item" && gameObject.name == "DropZone")
            {
                Debug.Log("Iventory Full!");
            }
        }
    }

    [ContextMenu("CreateStack")]
    public void CreateStack()
    {
        List<Item> items = new List<Item>();
        List<Item> result = new List<Item>();
        List<GameObject> todestroy = new List<GameObject>();
        Dictionary<string, Item> bucket = new Dictionary<string, Item>();

        for (int i = 0; i < contentpanel.transform.childCount; i++)
        {
            items.Add(contentpanel.transform.GetChild(i).gameObject.GetComponent<Item>());
        }

        foreach (Item i in items)
        {
            if (!i.isStackable || i.stackvalue == i.stackmax) result.Add(i);

            if (bucket.ContainsKey(i.ItemId) && !result.Contains(i))
            {
                foreach (KeyValuePair<string, Item> kvp in bucket)
                {
                    if (kvp.Key == i.ItemId)
                    {
                        kvp.Value.stackvalue += i.stackvalue;

                        int endvalue;

                        if (kvp.Value.stackvalue - kvp.Value.stackmax >= 0)
                        {
                            endvalue = kvp.Value.stackvalue - kvp.Value.stackmax;
                            i.stackvalue = i.stackmax;
                            result.Add(i);
                            kvp.Value.stackvalue = endvalue;
                        }
                        else
                        {
                            i.stackvalue = 0;
                            result.Add(i);
                        }

                    }
                }
            } 
            else
            {
                bucket.Add(i.ItemId, i);
            }
        }

        foreach (Item i in items)
        {
            if (i.stackvalue == 0) Destroy(i.gameObject);
        }

    }
}
