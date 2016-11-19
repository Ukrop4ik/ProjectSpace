using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class DropPanel : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name +  " Drop  to " + gameObject.name);
        eventData.pointerDrag.transform.SetParent(gameObject.transform);

        if (eventData.pointerDrag.tag == "item" && gameObject.name == "DropZone")
        {
            eventData.pointerDrag.GetComponent<Item>().TakeItem();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}
