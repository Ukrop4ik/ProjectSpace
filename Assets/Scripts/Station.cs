using UnityEngine;
using System.Collections;

public class Station : MonoBehaviour {

	void Start ()
    {
        if (ContextManagerGamePro.Instance().PreviousScene != "MainMenu")
        {
            Debug.Log("Auto Save");
            Invoke("AutoSave", 1f);
        }
        else
        {
            ContextManagerGamePro.Instance().StaticMetods.LoadProfileData();
        }
	}
	
	void Update () {
	
	}

    void AutoSave()
    {
        ContextManagerGamePro.Instance().StaticMetods.SaveGame();
    }
}
