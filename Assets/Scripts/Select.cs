using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {

    public AIship ship;

    public enum selecttype
    {
        Ship,
        Station,
        Asteroid,
        Conteiner,
        None
    }

    public selecttype SelectType;

    // Use this for initialization
    void Start () {

        GameObject selectedobj;

        if (SelectType != Select.selecttype.Conteiner)
        {
            selectedobj = transform.parent.gameObject;

            if (selectedobj.tag == "Ship")
            {
                SelectType = selecttype.Ship;
                ship = transform.parent.GetComponent<AIship>();
            }
        }    

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
