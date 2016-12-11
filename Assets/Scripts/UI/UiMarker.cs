using UnityEngine;
using System.Collections;

public class UiMarker : MonoBehaviour {

    public GameObject target;
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        } 
        this.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
    }
}
