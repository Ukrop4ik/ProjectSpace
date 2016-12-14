using UnityEngine;
using System.Collections;

public class PlayershipControl : MonoBehaviour {

    Ship playership;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playership = ContextManagerGamePro.Instance().playership;
            playership.shieldregenbool = !playership.shieldregenbool;
        }

	
	}
}
