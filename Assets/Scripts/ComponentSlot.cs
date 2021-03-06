﻿using UnityEngine;
using System.Collections;

public class ComponentSlot : MonoBehaviour {

    public Ship ship;
    public ShipComponent component;
    public Weapon weapon;
    public Transform LookPoint;
    public Item item;
    public bool itemCreated = false;
    public bool containitem;
    public int slotnumber;
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
    public size Size;
    public slotType SlotType;
    void Start ()
    {

        ship = transform.GetComponentInParent<Ship>();

    }
    [ContextMenu("RemoveSlot")]
    public void RemoveSlot()
    {
        if (ship.ComponentController.Slots.Contains(this))
        {
            ship.ComponentController.Slots.Remove(this);
            ship.ComponentController.UpdateComponents();
        }
    }
    [ContextMenu("AddSlot")]
    public void AddSlot()
    {
        if (!ship.ComponentController.Slots.Contains(this))
        {
            ship.ComponentController.Slots.Add(this);
            ship.ComponentController.UpdateComponents();
        }
    }

}
