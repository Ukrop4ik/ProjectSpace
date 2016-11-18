using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Localization : MonoBehaviour
{

    int langType;

    Text textfield;

    [SerializeField]
    string[] localization = new string[2];

    void Start()
    {

        ContextManagerGamePro.Instance().loca.Add(this);
        textfield = GetComponent<Text>();
        CreateText();

    }

    public void CreateText()
    {
        langType = PlayerPrefs.GetInt("Lang");

        switch (langType)
        {
            case 0:
                textfield.text = localization[0];
                break;

            case 1:
                textfield.text = localization[1];
                break;

            default:
                break;
        }
    }

    // This function is called when the MonoBehaviour will be destroyed
    public void OnDestroy()
    {
        ContextManagerGamePro.Instance().loca.Remove(this);
    }
}
