using UnityEngine;
using System.Collections;

public class NavPoint : MonoBehaviour {

    [SerializeField]
    float lifetime = 2f;

    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
