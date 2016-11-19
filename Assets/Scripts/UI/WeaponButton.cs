using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponButton : MonoBehaviour {

    Button thisbutton;
    Weapon weapon;
    public int keynumber;
    public bool status = false;

	// Use this for initialization
	void Start ()
    {
        thisbutton = this.gameObject.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ContextManagerGamePro.Instance().SelectedType == Select.selecttype.Ship)
        {
            thisbutton.interactable = true;
        }
        else
        {
            thisbutton.interactable = false;
        }

	}

    public void UseButton()
    {
        if (ContextManagerGamePro.Instance().selectedship == null) return;

        status = !status;

        if (status)
        {
            weapon.target = ContextManagerGamePro.Instance().selectedship.transform;
        }
        else
        {
            weapon.target = null;
        }
            
    }
    public void SetWeaponToButton(Weapon _weapon)
    {
        weapon = _weapon;
    }
}
