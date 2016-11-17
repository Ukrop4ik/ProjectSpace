using UnityEngine;
using System.Collections;

public class ShipComponent : MonoBehaviour {

    public enum component_type
    {
        Shield_Generator,
        Engine,
        Reactor
    }
    public enum size
    {
        Small,
        Medium,
        Large
    }
    public enum slotType
    {
        Engineer,
        Scince,
        Crew,
        Weapon
    }


    public component_type Type;
    public size Size;
    public slotType SlotType;

    public string component_name;
    public bool isInstall = false;
    public bool isActive = false;

    public int value;
    public int supportvalue;
    public int energyCost;
    public float floatValue;

    ComponentSlot slot;

	// Use this for initialization
	void Start ()
    {
       
        slot = transform.GetComponentInParent<ComponentSlot>();

        if (SlotType != slotType.Weapon)
        {
            slot.component = transform.gameObject.GetComponent<ShipComponent>();
        }
        else
        {
            slot.weapon = transform.gameObject.GetComponent<Weapon>();
            slot.weapon.LookPoint = slot.LookPoint;
        }
        

    }
	
    [ContextMenu("Activate")]
    public void Activate()
    {
        isActive = !isActive;
    }

}
