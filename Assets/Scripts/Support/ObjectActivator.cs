using UnityEngine;
using System.Collections;

public class ObjectActivator : MonoBehaviour {

    public static void Activate(GameObject obj, bool isClosed)
    {
        bool b = !isClosed;
        obj.SetActive(b);
    }
    public static void Close(GameObject obj)
    {
        obj.SetActive(false);
    }
    public static void Open(GameObject obj)
    {

        obj.SetActive(true);
    }
}
