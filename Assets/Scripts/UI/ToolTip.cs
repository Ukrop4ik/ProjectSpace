using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolTip : MonoBehaviour
{

    public GameObject _name;
    public GameObject _info;


    public Localization nametext;
 
    public Localization infotext;

    public Text costtext;


    public void OnEnable()
    {
        Debug.Log("ToolTypeCreate");
        nametext = _name.GetComponent<Localization>();
        infotext = _info.GetComponent<Localization>();
    }
}
