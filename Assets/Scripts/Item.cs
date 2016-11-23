using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject itemoriginal;
    public string ItemId;
    public Sprite itemsprite;
    [SerializeField]
    bool inShip = false;
    public bool isCreate = false;
    public bool isClone = false;
    Transform originparent;
    Item item;
    public GameObject createditem;
    public string Type = "";
    public ComponentSlot itemslot;

    public ItemSpaceEnum space;

    public enum ItemType
    {
        ShipComponent,
        Ammo
    }
    public ItemType type;
    public SlotTypeEnum SlotType;
    public string itemclass;

    void Start()
    {
        item = GetComponent<Item>();
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
        inShip = CheckInShip();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        item.GetComponent<CanvasGroup>().blocksRaycasts = false;
        originparent = transform.parent;
        transform.SetParent(transform.parent.parent.parent);
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
        item.GetComponent<CanvasGroup>().blocksRaycasts = true;

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
            return;
        }

        if (isCreate) return;
        if (isClone) return;
        if (eventData.pointerEnter.gameObject.tag == "Inventory" && item.space != ItemSpaceEnum.Inventory) return;

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
        item.GetComponent<Image>().enabled = false;

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
        Debug.Log("Enter");
        GameObject tooltip = transform.root.transform.Find("ToolTip").gameObject;
        tooltip.transform.position = this.transform.position;
        Debug.Log(tooltip.GetComponent<RectTransform>().offsetMin.y);
        Debug.Log(tooltip.GetComponent<RectTransform>().offsetMax.y);
        if (tooltip.GetComponent<RectTransform>().offsetMax.y > 0)
        {
            tooltip.GetComponent<RectTransform>().pivot = new Vector2(0, 1);

        }
        else if (tooltip.GetComponent<RectTransform>().offsetMin.y < 0)
        {
            tooltip.GetComponent<RectTransform>().pivot = new Vector2(0, 0);

        }
        tooltip.transform.position = this.transform.position;
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject tooltip = transform.root.transform.Find("ToolTip").gameObject;
        tooltip.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        tooltip.SetActive(false);
    }
}
