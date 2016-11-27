using UnityEngine;
using System.Collections.Generic;

public class ComponentController : MonoBehaviour
{


    public List<ComponentSlot> Slots = new List<ComponentSlot>();
    public List<ShipComponent> ShipComponents = new List<ShipComponent>();
    public List<Weapon> ShipWeapons = new List<Weapon>();
    public Ship ship;

    void Start()
    {
        Slots.AddRange(gameObject.GetComponentsInChildren<ComponentSlot>());

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
            createditem.transform.SetParent(item.transform);
            item.transform.SetSiblingIndex(0);
            createditem.transform.SetSiblingIndex(0);
            createditem.transform.localScale = Vector3.one;
            createditem.transform.position = Vector3.zero;
            createditem.transform.rotation = Quaternion.Euler(0, 0, 0);
            item.createditem = createditem;

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
    




