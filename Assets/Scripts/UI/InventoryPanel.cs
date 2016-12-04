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
    public bool stack = false;
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
                    CreateStack();

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
            Item value;
            if (bucket.TryGetValue(i.ItemId, out value))
            {
                value.stackvalue += i.stackvalue;

                int endvalue;

                if (value.stackvalue - value.stackmax >= 0)
                {
                    endvalue = value.stackvalue - value.stackmax;
                    i.stackvalue = i.stackmax;
                    result.Add(i);
                    value.stackvalue = endvalue;
                }
                else
                {
                    i.stackvalue = 0;
                    result.Add(i);
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

    public void OnEnable()
    {
        CreateStack();
    }
}
