using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneRes : MonoBehaviour {


	void Start () {
        LoadBattleUI();
    }


    void LoadBattleUI()
    {
        var g = Instantiate(Resources.Load("BattleUI") as GameObject);
        g.transform.SetParent(this.transform);
    }
}
