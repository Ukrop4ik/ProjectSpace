using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections;

public class Localization : MonoBehaviour
{
    public bool isRuntime;

    string langType;
    public string Id;
    Text textfield;

    void Start()
    {
        textfield = GetComponent<Text>();

        if (!isRuntime)
        {
            ContextManagerGamePro.Instance().loca.Add(this);
            CreateText();
        }

    }

    public void CreateText()
    {
        langType = PlayerPrefs.GetString("Lang");

        switch (langType)
        {
            case "English":
                textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][Id][langType].ToString();
                break;

            case "Russian":
                textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][Id][langType].ToString();
                break;

            default:
                break;
        }
    }

    // This function is called when the object becomes enabled and active
    public void OnEnable()
    {
        if (!isRuntime) return;

        CreateText();
    }

    // This function is called when the MonoBehaviour will be destroyed
    public void OnDestroy()
    {
        ContextManagerGamePro.Instance().loca.Remove(this);
    }
}
