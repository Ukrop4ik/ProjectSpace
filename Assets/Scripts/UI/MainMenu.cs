using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour {

    public Button startgamebutton;
    public Button exitbutton;
    public Button creditsbutton;
    public Dropdown lang;

    


    // Use this for initialization
    void Start () {
        lang.onValueChanged.AddListener(delegate { LangDropdownValueChangeHandler(lang); });

        switch (PlayerPrefs.GetString("Lang"))
        {
            case "English":
                lang.value = 0;
                break;
            case "Russian":
                lang.value = 1;
                break;
            default:
                lang.value = 0;
                break;
        }


    }
	
	// Update is called once per frame
	void Update ()
    {
        
	
	}

    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SetupProfile();
        ContextManagerGamePro.Instance().PreviousScene = "MainMenu";
        SceneManager.LoadScene("Station");
    }
    private void SetupProfile()
    {
        var g = ContextManagerGamePro.Instance();
        var needToCreateProfile = true;
        if (g.Profile != null)
        {
             needToCreateProfile = DoSomeThingWithPreviousProfile();
        }

        if (needToCreateProfile)
        {
            var p = Instantiate(Resources.Load<GameObject>("Profile"));
            g.Profile = p.GetComponent<Profile>();
        }

    }
    private bool DoSomeThingWithPreviousProfile()
    {
        // если игрок уже играет, то его текущее состояние потеряется созданием новой игры
        // и нужно сделать типа окошко "Вы уверены, что хотите создать новую игру?"
        return true;
    }

    private void LangDropdownValueChangeHandler(Dropdown target)
    {
        PlayerPrefs.SetInt("LangInt", target.value);
        switch (target.value)
        {
            case 0:
                PlayerPrefs.SetString("Lang", "English");
                break;
            case 1:
                PlayerPrefs.SetString("Lang", "Russian");
                break;
            default:
                PlayerPrefs.SetString("Lang", "English");
                break;
        }
        PlayerPrefs.Save();
        Debug.Log( "Lang set in: " + PlayerPrefs.GetInt("Lang"));
        if (!ContextManagerGamePro.Instance()) return;
        for (int i = 0; i < ContextManagerGamePro.Instance().loca.Count; i++)
        {
            ContextManagerGamePro.Instance().loca[i].CreateText();
        }
    }
    void Destroy()
    {
        lang.onValueChanged.RemoveAllListeners();
    }


    [ContextMenu("SaveProfile")]
    public void TestSave()
    {
        SaveManager.SaveProfile("Test");
    }
}
