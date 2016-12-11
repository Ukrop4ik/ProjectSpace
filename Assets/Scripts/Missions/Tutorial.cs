using UnityEngine;
using System.Collections.Generic;

public class Tutorial : Mission {



    // Use this for initialization
    void Start() {

        Invoke("StartMission", 2);

    }


    void StartMission()
    {
        SetCondition(WinConditionEnum.Custom);
        RadioMessage("Aurora_Station_name", "tutorial_mission_dialog_1", 15f, Resources.Load<Sprite>("image/defaultavatar"));
        InvokeRepeating("UpdateMission", 0, 1f);
        InvokeRepeating("PlayerInTriggerArea", 0, 1f);
        InvokeRepeating("TurnOffRegenShield", 0, 0.1f);
        CreateNavigationArrowToObj("TriggerArea", ArrowTypeEnum.Event);
        CreateUiMarker("TriggerArea", MarkerTypeEnum.MoveTo);
        HideShip("Trigger");
        HideShip("Raptor");
        HideShip("Prisoner");

    }

    void UpdateMission()
    {
    }

    void Win()
    {
        CancelInvoke();
        MissionRes.Win();
    }

    void PlayerInTriggerArea()
    {
        if (IsPlayerInArea("TriggerArea"))
        {
            RadioMessage("Aurora_Station_name", "tutorial_mission_dialog_2", 10, Resources.Load<Sprite>("image/defaultavatar"));
            Debug.Log("in area");
            CreateNavigationArrow("Training", ArrowTypeEnum.Enemy);
            CancelInvoke("PlayerInTriggerArea");
            InvokeRepeating("CheckTraining", 0, 1f);
            DestroyArea("TriggerArea");
        } 
    }

    void CheckTraining()
    {
        if (GetShips("Training").Count < 1)
        {
            RadioMessage("Aurora_Station_name", "tutorial_mission_dialog_3", 5, Resources.Load<Sprite>("image/defaultavatar"));
            Invoke("AlonePirateSay", 7f);
            ShowShip("Trigger");
            CreateNavigationArrow("Trigger", ArrowTypeEnum.Enemy);
            CancelInvoke("CheckTraining");
            ShipCommand("Trigger", ShipLogicEnum.MoveAndAttack);
            InvokeRepeating("CheckPirateKill", 0, 1f);
        }
    }

    void CheckPirateKill()
    {
        if (GetShips("Trigger").Count < 1)
        {
            RadioMessage("tutorial_pirate_name", "tutorial_mission_pirate_dialog_2", 3, Resources.Load<Sprite>("image/defaultavatar"));
            CancelInvoke("CheckPirateKill");
            CancelInvoke("TurnOffRegenShield");
            InvokeRepeating("CheckShild", 0, 1f);
        }
    }

    void CheckShild()
    {
        if (GetPlayerShild() > 50)
        {
            Debug.Log("Shild regen task end");
            CancelInvoke("CheckShild");
            ShowShip("Raptor");
            CreateNavigationArrow("Raptor", ArrowTypeEnum.Enemy);
            ShipCommand("Raptor", ShipLogicEnum.MoveAndAttack);
            ShowShip("Prisoner");
            ShipCommand("Prisoner", ShipLogicEnum.MoveToPlayer);
            CreateNavigationArrow("Prisoner", ArrowTypeEnum.Ally);
        }
    }

    void TurnOffRegenShield()
    {
        GetPlayerShip().shieldregenbool = false;
    }

    void AlonePirateSay()
    {
        RadioMessage("tutorial_pirate_name", "tutorial_mission_pirate_dialog_1", 3, Resources.Load<Sprite>("image/defaultavatar"));
    }

}
