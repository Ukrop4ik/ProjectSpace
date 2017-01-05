using UnityEngine;
using System.Collections;

public class mission_kill_pirates : Mission {

    // Use this for initialization
    void Start()
    {

        Invoke("StartMission", 2);

    }


    void StartMission()
    {
        SetCondition(WinConditionEnum.Custom);
        InvokeRepeating("CheckPirateKill", 0, 1);
        CreateNavigationArrow("enemy", ArrowTypeEnum.Enemy);
        Invoke("CreateMainObjective", 2);

    }

    void CheckPirateKill()
    {

        if (GetShips("enemy").Count < 1)
        {
            Invoke("Win", 3);
            CancelInvoke("CheckPirateKill");
            CancelInvoke("CheckMainObj");
            ObjectiveComplite("Aurora_Station_name");

        }
    }

    void CreateMainObjective()
    {
        CreateObjective("space_killship_obj", GetShips("enemy").Count);
        InvokeRepeating("CheckMainObj", 0, 1);
    }
    void CheckMainObj()
    {
        SetObjectiveCounter("Aurora_Station_name", GetShips("enemy").Count);
    }
    void Win()
    {
        MissionRes.Win();
    }
}
