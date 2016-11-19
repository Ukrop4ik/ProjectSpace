using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProfilesManager : MonoBehaviour {

    
    public Transform content;
    public Button NewProfileButton;
    public Button profilebuttonPrefab;
    MainMenu mainmenu;

    void Start()
    {
        mainmenu = GetComponent<MainMenu>();
    }

    public void CreateButtons()
    {

    }
}

public class ProfileButton
{
    public string profilename;

    public ProfileButton()
    {
    }
    public ProfileButton(string name)
    {
        this.profilename = name;
    }

}
