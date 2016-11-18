using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Localization : MonoBehaviour {

    int langType;

    Text textfield;

    [SerializeField]
    string[] localization = new string[2];

    void Start()
    {
        textfield = GetComponent<Text>();
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
}
