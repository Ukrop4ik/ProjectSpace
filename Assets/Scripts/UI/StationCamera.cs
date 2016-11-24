using UnityEngine;
using System.Collections;

public class StationCamera : MonoBehaviour {

    public GameObject target;

    void Update()
    {
        if (target.transform.childCount > 0)
        {
           // transform.LookAt(target.transform.GetChild(0));
            this.transform.position = new Vector3(target.transform.GetChild(0).position.x, 10, target.transform.GetChild(0).position.z);
        }

    }
}
