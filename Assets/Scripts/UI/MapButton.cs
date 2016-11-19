using UnityEngine;
using System.Collections;

public class MapButton : MonoBehaviour {

    [SerializeField]
    private string scene = "";

    public void ClickButton()
    {
        ContextManagerGamePro.Instance().PreviousScene = "Camp";
        ContextManagerGamePro.Instance().Map.StartBattle(scene);
    }
}
