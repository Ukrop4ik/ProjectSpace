using UnityEngine;
using System.Collections;

public class ShipComponent : MonoBehaviour {

    public enum component_type
    {
        Shield_Generator,
        Engine,
        Reactor,
        Weapon
    }
    public enum size
    {
        Small,
        Medium,
        Large
    }


    public component_type Type;
    public size Size;
    public SlotTypeEnum SlotType;

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

        if (SlotType != SlotTypeEnum.Weapon)
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
