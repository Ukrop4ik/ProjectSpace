using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    public string ItemName;
    public string ItemInfo;
    public int ItemCost;
    public string DataPath;

    public GameObject itemoriginal;
    public string ItemId;
    public Sprite itemsprite;
    public bool isCreate = false;
    public bool isClone = false;
    Transform originparent;
    public GameObject createditem;
    public string Type = "";
    public ComponentSlot itemslot;

    public ItemSpaceEnum space;
    Text counttext;
    public enum ItemType
    {
        ShipComponent,
        Ammo
    }
    public ItemType type;
    public SlotTypeEnum SlotType;
    public string itemclass;

    public bool isStackable = false;
    public int stackmax = 0;
    public int stackvalue = 1;

    void Start()
    {
        counttext = gameObject.transform.GetChild(0).GetComponent<Text>();
        GetComponent<Image>().sprite = itemsprite;

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

    void Update()
    {
        if (!isCreate)
        counttext.text = stackvalue.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        originparent = transform.parent;
        if (isStackable && stackvalue > 1)
        {
            TakeFromStack(eventData.pointerDrag.GetComponent<Item>(), eventData.pointerEnter);
        }
        else
        {
            transform.SetParent(transform.parent.parent.parent);
        }
        DropItem();

    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        if (itemoriginal != null)
        {
            itemoriginal.transform.parent.GetComponent<ComponentSlot>().itemCreated = false;
            itemoriginal.transform.parent.GetComponent<ComponentSlot>().containitem = false;
            itemoriginal.transform.parent.GetComponent<ComponentSlot>().ship.GetComponent<ComponentController>().DeleteFromItem(itemoriginal.GetComponent<Item>());
            Destroy(itemoriginal);
            isClone = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (!eventData.pointerEnter)
        {
            BackItem();
            return;
        }

        if (eventData.pointerEnter.name != "DropZone" && eventData.pointerEnter.gameObject.tag != "Inventory")
        {
            BackItem();
            return;
        }


        if (eventData.pointerEnter.gameObject.tag == "Inventory")
        {
            Debug.Log("To inventory");
            if (eventData.pointerEnter.gameObject.GetComponent<InventoryPanel>().isCanDropped == false)
            {
                BackItem();
                return;
            }
            transform.SetParent(eventData.pointerEnter.gameObject.transform.GetChild(0).transform);
            transform.localScale = Vector3.one;
            if (eventData.pointerEnter.gameObject.GetComponent<InventoryPanel>().stack == true)
            {
                eventData.pointerEnter.gameObject.GetComponent<InventoryPanel>().CreateStack();
            }
                return;
        }

        if (isCreate) return;
        if (isClone) return;
        if (eventData.pointerEnter.gameObject.tag == "Inventory" && space != ItemSpaceEnum.Inventory) return;

    }

    public void TakeItem()
    {
        if (ContextManagerGamePro.Instance().playership.cargo.curVolume < ContextManagerGamePro.Instance().playership.cargo.Volume)
        {
            ContextManagerGamePro.Instance().playership.cargo.itemsincargo.Add(this);
            transform.SetParent(ContextManagerGamePro.Instance().playership.cargo.cargoobj.transform);
            transform.position = Vector3.zero;
        }
        else
        {
            transform.SetParent(originparent);
        }
    }

    public void EqipItem(ComponentSlot slot)
    {
        isCreate = true;
        this.transform.SetParent(slot.transform);
        slot.containitem = true;
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        slot.ship.ComponentController.CreateFromItem(this);
        GetComponent<Image>().enabled = false;

        itemslot = slot;

    }

    public void DropItem()
    {
        if (itemslot != null)
        {
            isCreate = false;
            itemslot.containitem = false;
            itemslot = null;
        }

    }

    bool CheckInShip()
    {
        if (transform.parent.parent)
        {
            if (transform.parent.parent.gameObject.tag == "Ship" && transform.parent.name != "Cargo")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public void BackItem()
    {
        transform.SetParent(originparent);
        transform.localScale = Vector3.one;
        return;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject tooltip = transform.root.transform.Find("ToolTip").gameObject;
        ToolTip tooltipclass = tooltip.GetComponent<ToolTip>();

        tooltip.transform.position = this.transform.position;
        if (tooltip.GetComponent<RectTransform>().offsetMax.y > 0)
        {
            tooltip.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
        }
        else if (tooltip.GetComponent<RectTransform>().offsetMin.y < 0)
        {
            tooltip.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
        }
        tooltip.transform.position = this.transform.position;

        tooltipclass.nametext.Id = ItemName;
        tooltipclass.infotext.Id = ItemInfo;
        tooltipclass.costtext.text = ItemCost.ToString();

        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject tooltip = transform.root.transform.Find("ToolTip").gameObject;
        tooltip.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
        tooltip.SetActive(false);
    }

    public void TakeFromStack(Item item, GameObject slot)
    {
        Debug.Log(item.ItemId + " " + slot);
        GameObject g = Instantiate(Resources.Load("items/" + item.ItemId) as GameObject);
        g.name = item.ItemId;
        Item i = g.GetComponent<Item>();
        i.stackvalue = item.stackvalue;
        item.stackvalue = 1;
        g.transform.SetParent(slot.transform.parent);
        g.transform.localScale = Vector3.one;
        i.stackvalue--;
        transform.SetParent(transform.parent.parent.parent);

    }
}
