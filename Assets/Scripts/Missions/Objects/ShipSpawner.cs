using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipSpawner : MonoBehaviour {

    public int danger;
    public int count;
    public string group;
    public ShipLogicEnum command;

    void Start()
    {
        Invoke("Spawn", 1f);
    }

    void Spawn()
    {
        List<GameObject> shipstospaun = new List<GameObject>();

        foreach (var ship in ContextManagerGamePro.Instance().itemships)
        {
            if (ship.danger == danger)
            {
                shipstospaun.Add(ship.ship.gameObject);
            }
        }

        for (int i = 0; i < count; i++)
        {
            int r = shipstospaun.Count > 1 ? Random.Range(0, shipstospaun.Count) : 0;
            GameObject obj = Instantiate(shipstospaun[r]);
            AIship ai = obj.GetComponent<AIship>();
            Ship sh = obj.GetComponent<Ship>();
            ai.Logic = command;
            sh.actiongroup = group;

            obj.transform.position = this.transform.position;
        }
    }
}
