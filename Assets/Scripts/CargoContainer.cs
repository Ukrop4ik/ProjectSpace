using UnityEngine;
using System.Collections.Generic;

public class CargoContainer : MonoBehaviour {

    public List<Item> itemlist = new List<Item>();
    LootPanel lootpanel;
    GameObject lootUI;
    bool isShipConteiner = false;
    public float dist;
    public float UseDist = 10f;
	// Use this for initialization
	void Start ()
    {
        if (transform.parent != null)
            isShipConteiner = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ContextManagerGamePro.Instance())
        dist = Vector3.Distance(this.gameObject.transform.position, ContextManagerGamePro.Instance().playership.transform.position);

        if (itemlist.Count > transform.childCount)
        {
           AddItemsToList();
        }
        if (isShipConteiner)
        {
            if (transform.parent == null)
                isShipConteiner = false;
        }
	
	}

    [ContextMenu("Open")]
    public void Open()
    {
        if (dist > UseDist) return;
        AddItemsToList();
        lootpanel = ContextManagerGamePro.Instance().SpaceUi.lootpanel;
        lootpanel.conteiner = this;
        lootUI = lootpanel.gameObject;
        lootUI.SetActive(true);
        foreach (Item item in itemlist)
        {
            item.transform.SetParent(lootpanel.lootlistpanel.transform);
            item.transform.localScale = Vector3.one;
        }
    }
    public void Close()
    {
        ContextManagerGamePro.Instance().SpaceUi.lootpanel.gameObject.SetActive(false);
    }
    public void AddItemsToList()
    {
        itemlist.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            itemlist.Add(transform.GetChild(i).GetComponent<Item>());
        }
    } 
}
