using UnityEngine;
using System.Collections.Generic;

public class ComponentController : MonoBehaviour
{


    public List<ComponentSlot> Slots = new List<ComponentSlot>();
    public List<ShipComponent> ShipComponents = new List<ShipComponent>();
    public List<Weapon> ShipWeapons = new List<Weapon>();
    Ship ship;

    void Start()
    {
        Slots.AddRange(gameObject.GetComponentsInChildren<ComponentSlot>());

        if (gameObject.GetComponent<Ship>())
        {
            ship = gameObject.GetComponent<Ship>();
        }

        InvokeRepeating("UpdateComponents", 0.2f, 0.1f);

    }

    [ContextMenu("UpdateComponents")]
    public void UpdateComponents()
    {
        ShipComponents.Clear();
        ShipWeapons.Clear();
        if (Slots.Count > 0)
        {
            foreach (var slot in Slots)
            {
                if (slot.SlotType != ComponentSlot.slotType.Weapon)
                {
                    ShipComponents.Add(slot.component);
                }
                else
                {
                    ShipWeapons.Add(slot.weapon);
                }

            }
        }
    }

    //[ContextMenu("CreateFromItem")]
    //public void CreateFromItem()
    //{
    //    List<Item> itemsinslot = new List<Item>();
    //    foreach (ComponentSlot slot in Slots)
    //    {
    //        if (slot.transform.childCount > 0)
    //        {
    //            if (slot.transform.GetChild(0).gameObject.tag != "item") continue;
    //            itemsinslot.Add(slot.transform.GetChild(0).GetComponent<Item>());              
    //        }
    //    }

    //    foreach (Item item in itemsinslot)
    //    {
    //        if (item.transform.childCount > 0) continue;

    //        string itemID = item.ItemId;
    //        GameObject createditem = Instantiate<GameObject>(Resources.Load(itemID) as GameObject);
    //        item.createditem = createditem;
    //        createditem.transform.SetParent(item.transform);
    //        ShipComponent component = item.createditem.GetComponent<ShipComponent>();
    //        switch (item.itemclass)
    //        {
    //            case "ShieldGenerator":
    //                ship.maxshield += component.value;
    //                ship.shieldregen += component.supportvalue;
    //                ship.shieldregencost += component.energyCost;
    //                break;
    //            case "Engine":
    //                ship.maxspeed += component.value;
    //                ship.acceleration += component.floatValue;
    //                break;
    //            case "Reactor":
    //                ship.maxenergy += component.value;
    //                ship.enegyregen += component.supportvalue;
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    [ContextMenu("DeleteFromItem")]
    public void DeleteFromItem(Item item)
    {      

            ShipComponent component = item.transform.GetChild(0).GetComponent<ShipComponent>();

            switch (item.itemclass)
            {
                case "ShieldGenerator":
                    ship.maxshield -= component.value;
                    ship.shieldregen -= component.supportvalue;
                    ship.shieldregencost -= component.energyCost;
                    break;
                case "Engine":
                    ship.maxspeed -= component.value;
                    ship.acceleration -= component.floatValue;
                    break;
                case "Reactor":
                    ship.maxenergy -= component.value;
                    ship.enegyregen -= component.supportvalue;
                    break;
                default:
                    break;
            }

            Destroy(component.gameObject);
        }


    [ContextMenu("CreateFromItem")]
    public void CreateFromItem(Item item)
    {

            string itemID = item.ItemId;
            GameObject createditem = Instantiate<GameObject>(Resources.Load(itemID) as GameObject);
            item.createditem = createditem;
            createditem.transform.SetParent(item.transform);
            ShipComponent component = item.createditem.GetComponent<ShipComponent>();
            switch (item.itemclass)
            {
                case "ShieldGenerator":
                    ship.maxshield += component.value;
                    ship.shieldregen += component.supportvalue;
                    ship.shieldregencost += component.energyCost;
                    break;
                case "Engine":
                    ship.maxspeed += component.value;
                    ship.acceleration += component.floatValue;
                    break;
                case "Reactor":
                    ship.maxenergy += component.value;
                    ship.enegyregen += component.supportvalue;
                    break;
                default:
                    break;
            }
        }
    }
    




