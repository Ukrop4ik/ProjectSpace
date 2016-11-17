using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

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
                    Item item = eventData.pointerDrag.GetComponent<Item>();
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
}
