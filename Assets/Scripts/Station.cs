using UnityEngine;
using System.Collections;

public class Station : MonoBehaviour {

	void Start ()
    {

        Invoke("Load", 0.5f);
	}
	
	void Update () {
	
	}

    void AutoSave()
    {
       
    }
    void Load()
    {
        ContextManagerGamePro.Instance().StaticMetods.LoadProfileData();

        StationUI ui = GameObject.Find("StationUI").GetComponent<StationUI>();
        if (ui.PlayerActualShip.transform.childCount > 0)
        {
            return;
        }
        ContextManagerGamePro.Instance().StaticMetods.LoadShipData();

    }
}
