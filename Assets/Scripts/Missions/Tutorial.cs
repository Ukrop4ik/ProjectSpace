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
           // Debug.Log("in area");
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
            RadioMessage("Aurora_Station_name", "tutorial_mission_dialog_4", 10, Resources.Load<Sprite>("image/defaultavatar"));
            RadioMessage("Aurora_Station_name", "tutorial_mission_dialog_5", 10, Resources.Load<Sprite>("image/defaultavatar"));
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
            ShipCommand("Raptor", ShipLogicEnum.Stay);
            RadioMessage("Aurora_Station_name", "tutorial_mission_dialog_6", 10, Resources.Load<Sprite>("image/defaultavatar"));
            Invoke("ShowPrisoner", 10);
            InvokeRepeating("CheckRaptor", 0, 1f);
        }
    }

    void TurnOffRegenShield()
    {
        GetPlayerShip().shieldregenbool = false;
        GetPlayerShip().shield = 0;
    }

    void AlonePirateSay()
    {
        RadioMessage("tutorial_pirate_name", "tutorial_mission_pirate_dialog_1", 3, Resources.Load<Sprite>("image/defaultavatar"));
    }
    void StartAiPrissoner()
    {
        ShipCommand("Prisoner", ShipLogicEnum.ChaosAi);
    }

    void EnemyStartAttack()
    {
        ShipCommand("Raptor", ShipLogicEnum.ChaosAi);
    }
    void ShowPrisoner()
    {
        ShowShip("Prisoner");
        ShipCommand("Prisoner", ShipLogicEnum.MoveToPlayer);
        CreateNavigationArrow("Prisoner", ArrowTypeEnum.Ally);
        RadioMessage("prisoner_jo_luck", "tutorial_mission_dialog_7", 7, Resources.Load<Sprite>("image/defaultavatar"));
        RadioMessage("prisoner_jo_luck", "tutorial_mission_dialog_10", 7, Resources.Load<Sprite>("image/defaultavatar"));

        Invoke("StartAiPrissoner", 10);
        Invoke("EnemyStartAttack", 10);
    }
    void CheckRaptor()
    {
        if (GetShips("Raptor").Count < 1)
        {
            RadioMessage("prisoner_jo_luck", "tutorial_mission_dialog_8", 7, Resources.Load<Sprite>("image/defaultavatar"));
            RadioMessage("Aurora_Station_name", "tutorial_mission_dialog_9", 7, Resources.Load<Sprite>("image/defaultavatar"));
            Invoke("End", 14);
            CancelInvoke("CheckRaptor");
        }
    }
    void End()
    {
        Win();
    }
}
