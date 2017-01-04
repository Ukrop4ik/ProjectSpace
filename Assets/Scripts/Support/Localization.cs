using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections;

public class Localization : MonoBehaviour
{
    public bool isRuntime;
    public bool isReverse;
    public bool manual;

    string langType;
    public string Id;

    void Start()
    {
        

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
        Text textfield = GetComponent<Text>();
        switch (langType)
        {
            case "English":
                if (isReverse)
                {
                    try
                    {
                        textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][textfield.text][langType].ToString();
                    }
                    catch
                    {
                        textfield.text = Id;
                    }

                }
                else
                {
                    try
                    {
                        textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][Id][langType].ToString();
                    }
                    catch
                    {
                        textfield.text = Id;
                    }

                }

                break;

            case "Russian":
                if (isReverse)
                {
                    try
                    {
                        textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][textfield.text][langType].ToString();
                    }
                    catch
                    {
                        textfield.text = Id;
                    }
                }
                else
                {
                    try
                    {
                        textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][Id][langType].ToString();
                    }
                    catch
                    {
                        textfield.text = Id;
                    }
                }
                break;

            default:
                break;
        }
    }
    [ContextMenu("Create")]
    public void CreateText(string ID)
    {
        Text textfield = GetComponent<Text>();
        langType = PlayerPrefs.GetString("Lang");
        try
        {
            textfield.text = ContextManagerGamePro.Instance().ResourceManager.LocalizationData["Localization"][ID][langType].ToString();
        }
        catch
        {
            textfield.text = Id;
        }
    } 

    // This function is called when the object becomes enabled and active
    public void OnEnable()
    {
        if (!isRuntime || manual) return;
        if (!ContextManagerGamePro.Instance().loca.Contains(this))
        {
            ContextManagerGamePro.Instance().loca.Add(this);
        }
       
        CreateText();
    }

    // This function is called when the MonoBehaviour will be destroyed
    public void OnDestroy()
    {
        if (ContextManagerGamePro.Instance().loca.Contains(this))
            ContextManagerGamePro.Instance().loca.Remove(this);
    }
}
