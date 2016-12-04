using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProfilePanel : MonoBehaviour
{
    Profile profile;


    public Text profilename;
    public Text credits;
    public Text fame;

    void Start()
    {
        
    }

    public void OnEnable()
    {
        profile = ContextManagerGamePro.Instance().Profile;

        profilename.text = profile.profilename;
        credits.text = profile.credits.ToString();
        fame.text = profile.fame.ToString();
    }
}
