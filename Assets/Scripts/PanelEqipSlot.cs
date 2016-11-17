using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class PanelEqipSlot : MonoBehaviour, IDropHandler {
 
    StationUI stationUI;
    public ComponentSlot slot;
    public string Type = "";
    public SlotTypeEnum SlotType;

    // Use this for initialization
    void Start () {

        stationUI = transform.parent.parent.parent.gameObject.GetComponent<StationUI>();

        switch (SlotType)
        {
            case SlotTypeEnum.Engineer:
                Type = "Engineer";
                break;
            case SlotTypeEnum.Weapon:
                Type = "Weapon";
                break;
            default:
                Type = "None";
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag.tag == "item" && gameObject.name == "DropZone")
        {
            if (eventData.pointerDrag.gameObject.GetComponent<Item>().SlotType != SlotType)
            {
                Item _item = eventData.pointerDrag.gameObject.GetComponent<Item>();
                _item.BackItem();
                return;
            }
            eventData.pointerDrag.transform.SetParent(gameObject.transform);
            eventData.pointerDrag.transform.localScale = Vector3.one;

            Debug.Log(eventData.pointerDrag.name + " Drop  to " + gameObject.name);

            Item item = eventData.pointerDrag.GetComponent<Item>();


            if (item.Type != Type) return;
            if (slot.containitem) return;


            GameObject itemclone = Instantiate(item).gameObject;

            item.space = ItemSpaceEnum.ShipSlot;

            itemclone.name = item.gameObject.name;
            itemclone.GetComponent<Item>().isClone = true;
            itemclone.GetComponent<Item>().itemoriginal = item.gameObject;
            itemclone.transform.SetParent(gameObject.transform);
            itemclone.transform.localScale = Vector3.one;
            itemclone.GetComponent<CanvasGroup>().blocksRaycasts = true;
            itemclone.GetComponent<Item>().space = ItemSpaceEnum.EquipSlot;


            eventData.pointerDrag.GetComponent<Item>().EqipItem(slot);


        }
    }
}
