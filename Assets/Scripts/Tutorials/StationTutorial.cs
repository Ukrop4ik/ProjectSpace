using UnityEngine;
using System.Collections;

public class StationTutorial : MonoBehaviour {

    public GameObject tutorialdialog1;
    public GameObject tutorialdialog2;
    public GameObject tutorialdialog3;

    void Start()
    {
        Invoke("StartTutorial", 1f);
    } 

    void StartTutorial()
    {
        if (ContextManagerGamePro.Instance().Profile.isTutorialPart1)
        {
            tutorialdialog1.SetActive(true);
            return;
        }
        else if (ContextManagerGamePro.Instance().Profile.isTutorialPart2)
        {
           // tutorialdialog2.SetActive(true);
            return;
        }
        else if (ContextManagerGamePro.Instance().Profile.isTutorialPart3)
        {
           // tutorialdialog3.SetActive(true);
            return;
        }
    }
}
