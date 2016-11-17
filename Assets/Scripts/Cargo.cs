using UnityEngine;
using System.Collections.Generic;

public class Cargo : MonoBehaviour {

    public int Volume = 1;
    public int curVolume;
    public List<Item> itemsincargo = new List<Item>();
    public GameObject cargoobj;

    void Start()
    {
        cargoobj = transform.FindChild("Cargo").gameObject;
        InvokeRepeating("UpdateCargo", 0, 0.2f);
    }
    void Update()
    {
        curVolume = cargoobj.transform.childCount;
    }

    void UpdateCargo()
    {

        for (int i = 0; i < itemsincargo.Count; i++)
        {
            if (itemsincargo[i] == null) itemsincargo.RemoveAt(i);
        }

        curVolume = itemsincargo.Count;
    }

}
