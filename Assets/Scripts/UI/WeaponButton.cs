using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponButton : MonoBehaviour {

    public Image statusimage;
    Button thisbutton;
    Weapon weapon;
    public int keynumber;
    public bool status = false;

	// Use this for initialization
	void Start ()
    {
        thisbutton = this.gameObject.GetComponent<Button>();
        statusimage.color = Color.green;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ContextManagerGamePro.Instance().selectedship == null || ContextManagerGamePro.Instance().selectedship.Faction != FactionEnum.Pirate)
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

        StatusColour();
	}

    void StatusColour()
    {

        if (weapon.isReload)
        {
            statusimage.color = Color.yellow;
        }
        else
        {
            if (ContextManagerGamePro.Instance().selectedship == null || ContextManagerGamePro.Instance().selectedship.Faction != FactionEnum.Pirate)
            {
                statusimage.color = Color.red;
            }
            if (ContextManagerGamePro.Instance().selectedship.Faction == FactionEnum.Pirate)
            {
                statusimage.color = Color.green;
            }
        }
    }

    public void UseButton()
    {
        if (ContextManagerGamePro.Instance().selectedship.Faction != FactionEnum.Pirate) return;

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
    public void UseButtonRm()
    {
        if (ContextManagerGamePro.Instance().selectedship.Faction != FactionEnum.Pirate) return;

        if (weapon)
        {
            if (ContextManagerGamePro.Instance().selectedship)
                weapon.target = ContextManagerGamePro.Instance().selectedship.transform;
        }

        
    }
    public void SetWeaponToButton(Weapon _weapon)
    {
        weapon = _weapon;
        GetComponent<Image>().sprite = weapon.transform.parent.gameObject.GetComponent<Item>().itemsprite;
    }
}
