using UnityEngine;
using System.Collections.Generic;

public class Tutorial : Mission {

   

	// Use this for initialization
	void Start () {

        Invoke("StartMission", 2);	
      
	}

    void StartMission()
    {
       SetCondition(WinConditionEnum.Custom);
       RadioMessage("credits", "ShieldGeneratorSmallMk1_info", 5f, Resources.Load<Sprite>("image/defaultavatar"));
       InvokeRepeating("UpdateMission", 0, 1f);
    }
    void UpdateMission()
    {
        List<Ship> ships = new List<Ship>();
        foreach (GameObject ship in Mission.MissionRes.enemis)
        {
            if (!ship) continue;
            if (ship.GetComponent<Ship>().actiongroup == "Trigger")
            {
                ships.Add(ship.GetComponent<Ship>());
            }
        }
        PlayerInTriggerArea();
        PlayerInTriggerArea2();

        if (ships.Count == 0) Win();
    }

    void Win()
    {
        CancelInvoke();
        MissionRes.Win();
    }

    void PlayerInTriggerArea()
    {
        if (IsPlayerInArea("TriggerArea")) Debug.Log("in area");
    }
    void PlayerInTriggerArea2()
    {
        if (IsPlayerInArea("TriggerArea2")) Debug.Log("in area 2");
    }

}
