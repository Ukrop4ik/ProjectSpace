using UnityEngine;
using System.Collections.Generic;

public class Tutorial : Mission {

   

	// Use this for initialization
	void Start () {

        Invoke("StartMission", 2);	
      
	}

    void StartMission()
    {
       MissionBuilder.condition = WinConditionEnum.Custom;
       RadioMessage("credits", "ShieldGeneratorSmallMk1_info", 5f, Resources.Load<Sprite>("image/defaultavatar"));
       InvokeRepeating("UpdateMission", 0, 1f);
    }
    void UpdateMission()
    {
        List<Ship> ships = new List<Ship>();
        foreach (GameObject ship in MissionRes.enemis)
        {
            if (!ship) continue;
            if (ship.GetComponent<Ship>().actiongroup == "Trigger")
            {
                ships.Add(ship.GetComponent<Ship>());
            }
        }

        if (ships.Count == 0) Win();
    }

    void Win()
    {
        CancelInvoke();
        MissionRes.Win();
    }

}
