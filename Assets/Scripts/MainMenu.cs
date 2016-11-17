using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Button startgamebutton;
    public Button exitbutton;
    public Button creditsbutton;

    


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartCredits()
    {
        ContextManagerGamePro.Instance().PreviousScene = "MainMenu";
        SceneManager.LoadScene("Credits");
    }
    public void Back()
    {
        SceneManager.LoadScene(ContextManagerGamePro.Instance().PreviousScene);
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
}
