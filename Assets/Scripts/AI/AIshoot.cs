using UnityEngine;
using System.Collections;

public class AIshoot : MonoBehaviour {

    SceneRes SceneRes;

    void Start()
    {
        Invoke("Init", 2);
    }

    void Init()
    {
        SceneRes = gameObject.GetComponent<SceneRes>();
        InvokeRepeating("SlowUpdate", 0, 0.5f);
    }

    void SlowUpdate()
    {
        if (SceneRes.pirateships.Count > 0)
            foreach (GameObject pirateship in SceneRes.pirateships)
            {

            }
    }
	
}
