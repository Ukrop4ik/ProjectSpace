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
        InvokeRepeating("CheckPirateKill", 0, 2);
        CreateNavigationArrow("enemy", ArrowTypeEnum.Enemy);
    }

    void CheckPirateKill()
    {
        if (GetShips("enemy").Count < 1)
        {
            MissionRes.Win();
        }
    }
}
