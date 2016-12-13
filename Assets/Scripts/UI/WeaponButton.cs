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
        if (ContextManagerGamePro.Instance().selectedship == null)
        {
            thisbutton.interactable = false;
            status = false;
            return;
        }

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

        status = !status;

        if (status)
        {
            if (weapon)
                if (ContextManagerGamePro.Instance().selectedship)
                weapon.target = ContextManagerGamePro.Instance().selectedship.transform;
        }
        else
        {
            if (weapon)
                weapon.target = null;
        }
            
    }
    public void SetWeaponToButton(Weapon _weapon)
    {
        weapon = _weapon;
        GetComponent<Image>().sprite = weapon.transform.parent.gameObject.GetComponent<Item>().itemsprite;
    }
}
