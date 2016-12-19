using UnityEngine;
using System.Collections;

public class SceneBuilder : MonoBehaviour {

    public bool generete = false;

    public string SceneName;
    public int SceneLevel;
    public int SceneFame;
    public WinConditionEnum condition;

    void Start()
    {
        if (generete)
        {
            SceneName = ContextManagerGamePro.Instance().loadedmission.ID;
            SceneLevel = ContextManagerGamePro.Instance().loadedmission.danger;
            SceneFame = ContextManagerGamePro.Instance().loadedmission.fame;
        }
    }

}
