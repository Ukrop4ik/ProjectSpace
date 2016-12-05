using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections;

public class Localization : MonoBehaviour
{
    public bool isRuntime;
    public bool isReverse;

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

    [ContextMenu("Create")]
    public void CreateText()
    {
        langType = PlayerPrefs.GetString("Lang");

        switch (langType)
        {
            case "English":
                if (isReverse)
                {

                    textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][textfield.text][langType].ToString();
                }
                else
                {
                    textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][Id][langType].ToString();
                }

                break;

            case "Russian":
                if (isReverse)
                {
                    textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][textfield.text][langType].ToString();
                }
                else
                {
                    textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][Id][langType].ToString();
                }
                break;

            default:
                break;
        }
    }
    [ContextMenu("Create")]
    public void CreateText(string ID)
    {
        langType = PlayerPrefs.GetString("Lang");
        textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][ID][langType].ToString();
    } 

    // This function is called when the object becomes enabled and active
    public void OnEnable()
    {
        if (!isRuntime) return;
        if (!ContextManagerGamePro.Instance().loca.Contains(this))
        {
            ContextManagerGamePro.Instance().loca.Add(this);
        }
       
        CreateText();
    }

    // This function is called when the MonoBehaviour will be destroyed
    public void OnDestroy()
    {
        ContextManagerGamePro.Instance().loca.Remove(this);
    }
}
