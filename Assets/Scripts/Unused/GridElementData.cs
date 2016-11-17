using UnityEngine;
using System.Collections;

public class GridElementData : MonoBehaviour {

    public bool isEmpty;
    public GameObject gridelement;
    public Vector2 pos;

    void Update()
    {
        if (isEmpty && gridelement != null)
        {
            gridelement.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            gridelement.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
