using UnityEngine;
using System.Collections;

public class TriggerArea : MonoBehaviour {

    public float Radius;
    public AreaTypeEnum Type;

    void OnDrawGizmos()
    {
        if (Type == AreaTypeEnum.Trigger) Gizmos.color = Color.green;
        if (Type == AreaTypeEnum.Agro) Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
