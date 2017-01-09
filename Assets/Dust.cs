using UnityEngine;
using System.Collections;

public class Dust : MonoBehaviour
{
    public void Awake()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
