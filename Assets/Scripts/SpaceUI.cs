using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpaceUI : MonoBehaviour {


    Camera maincamera;
    Canvas canvas;

    public Camera minimapcamera;
    public GameObject selectshippanel;
    public GameObject contentNavPAnel;
    public GameObject WeaponButtonPrefab;
    public GameObject SpellPanelWeapon;
    public Text navText;
    public LootPanel lootpanel;
    public Button lootpanelbutton;

    public List<Ship> navlistships = new List<Ship>();
    public Dictionary<KeyCode, WeaponButton> weaponbuttons = new Dictionary<KeyCode, WeaponButton>();

    public Image shieldbar;
    public Text shieldtext;
    public Text shieldregentext;
    public Image hpbar;
    public Text hptext;
    public Text hpregentext;
    public Image energybar;
    public Text energytext;
    public Text energyregentext;
    public Text speed;
    public Text acceleration;
    public Text SelectedShipTypeText;
    public Text SelectedShipNameText;
    public Text SelectedDist;


    public Text targetvectortext;
    public Text playervectortext;
    public Text targetdisttext;

    Ship playership;
    List<Ship> allships = new List<Ship>();

    public bool ready = false;

    void Start()
    {
        maincamera = Camera.main;
        minimapcamera = Instantiate(minimapcamera);
        canvas = this.gameObject.GetComponent<Canvas>();
        ContextManagerGamePro.Instance().SpaceUi = this;
       // canvas.worldCamera = maincamera;
       // canvas.planeDistance = 1;
        Invoke("UpdateModulePanel", 1f);

    }

    void Update()
    {
        if (ready)
        {
            if (playership == null)
            {
                ready = false;
            }
            else
            {
                if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse1))
                {

                    foreach (KeyValuePair<KeyCode, WeaponButton> kvp in weaponbuttons)
                    {
                        if (Input.GetKey(kvp.Key) || Input.GetKey(KeyCode.Alpha0))
                        {
                            kvp.Value.UseButton();
                        }
                    }
                }

                foreach (Ship ship in ContextManagerGamePro.Instance().shiplist)
                {
                    if (!allships.Contains(ship))
                    {
                        allships.Add(ship);
                        Transform tr =  Instantiate(navText).transform;
                        tr.localPosition = contentNavPAnel.transform.position;
                        tr.SetParent(contentNavPAnel.transform);
                        tr.localScale = new Vector3(1, 1, 1);
                        tr.localRotation = Quaternion.Euler(0,0,0);
                    }
                    
                }


                switch (ContextManagerGamePro.Instance().SelectedType)
                {
                    case Select.selecttype.Ship:
                        ObjectSelector(new GameObject[] { selectshippanel }, selectshippanel);
                        SelectedShipNameText.text = "Name: " + ContextManagerGamePro.Instance().selectedship.shipname;
                        SelectedShipTypeText.text = "Type:  Ship";
                        if (ContextManagerGamePro.Instance().selectedship)
                        SelectedDist.text = "Dist: " + Vector3.Distance(ContextManagerGamePro.Instance().selectedship.gameObject.transform.position, playership.gameObject.transform.position).ToString("0.00");
                        break;
                    default:
                        break;
                }

                minimapcamera.transform.position = playership.transform.position;

                shieldbar.fillAmount = Amount(playership.maxshield, playership.shield);         
                hpbar.fillAmount = Amount(playership.maxHP, playership.HP);
                energybar.fillAmount = Amount(playership.maxenergy, playership.energy);

                shieldtext.text = playership.shield + " / " + playership.maxshield;
                hptext.text = playership.HP + " / " + playership.maxHP;
                energytext.text = playership.energy + " / " + playership.maxenergy;

                if (playership.shieldregenbool)
                {
                    shieldregentext.text = "+ " + playership.shieldregen + " sec" + " / - " + playership.shieldregencost + " energy";
                }             
                else shieldregentext.text = "+ " + 0 + " sec" + " / - " + "0 energy";

                hpregentext.text = "+ " + 0 +  " sec";
                energyregentext.text = "+ " + playership.enegyregen + " sec";

                targetvectortext.text = "nav pos " + playership.navtarget.ToString();
                targetdisttext.text = "dist " + playership.navtargetdist.ToString("0.00");
                playervectortext.text = "player " + playership.transform.position;

                speed.text = playership.speed.ToString("0") + " / " + playership.maxspeed.ToString("0");
                acceleration.text = playership.acceleration.ToString("0");


            }
           
        }
        else
        {
            allships.Clear();
            List<GameObject> shipsobj = new List<GameObject>();
            shipsobj.AddRange(GameObject.FindGameObjectsWithTag("Ship"));
            foreach (GameObject ship in shipsobj)
            {
                if (ship.GetComponent<Ship>().isPlayerShip)
                {
                    playership = ship.GetComponent<Ship>();
                    ready = true;
                    break;
                }
                else
                {
                    Debug.Log("NoPlayerShip!");
                }
            }
        }
       
        
    }

    float Amount(float maxvalue, float value)
    {
        return value / maxvalue;
    }

    public void ObjectSelector(GameObject[] objects, GameObject activepanel)
    {
        foreach (GameObject _object in objects)
        {
            if (_object == activepanel)
            {
                _object.SetActive(true);
            }
            else
            {
                _object.SetActive(false);
            }
        }
    }

    [ContextMenu("WpdatwWeaponsinUI")]
    public void UpdateModulePanel()
    {
        // max number 57
        int weaponbuttonkeynumber = 49;

        weaponbuttons.Clear();

        foreach (Weapon weapon in ContextManagerGamePro.Instance().playership.ComponentController.ShipWeapons)
        {

            Debug.Log(weaponbuttonkeynumber);
            Transform tr = Instantiate(WeaponButtonPrefab).transform;
            tr.localPosition = SpellPanelWeapon.transform.position;
            tr.SetParent(SpellPanelWeapon.transform);
            tr.localScale = new Vector3(1, 1, 1);
            tr.localRotation = Quaternion.Euler(0, 0, 0);

            WeaponButton button = tr.gameObject.GetComponent<WeaponButton>();
            switch (weaponbuttonkeynumber)
            {
                case 49:
                    weaponbuttons.Add(KeyCode.Alpha1, button);
                    break;
                case 50:
                    weaponbuttons.Add(KeyCode.Alpha2, button);
                    break;
                case 51:
                    weaponbuttons.Add(KeyCode.Alpha3, button);
                    break;
                case 52:
                    weaponbuttons.Add(KeyCode.Alpha4, button);
                    break;
                case 53:
                    weaponbuttons.Add(KeyCode.Alpha5, button);
                    break;
                case 54:
                    weaponbuttons.Add(KeyCode.Alpha6, button);
                    break;
                case 55:
                    weaponbuttons.Add(KeyCode.Alpha7, button);
                    break;
                case 56:
                    weaponbuttons.Add(KeyCode.Alpha8, button);
                    break;
                case 57:
                    weaponbuttons.Add(KeyCode.Alpha9, button);
                    break;

                default:
                    break;
            }
            
            button.SetWeaponToButton(weapon);

            weaponbuttonkeynumber++;
        }
    }

    void SelectButtonFromKey()
    {

    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);

        if (lootpanel.conteiner)
        {
            if (lootpanel.lootlistpanel.transform.childCount > 0)
            {
                int count = lootpanel.lootlistpanel.transform.childCount;

                for (int i = 0; i < count; i++)
                {
                    Debug.Log(lootpanel.lootlistpanel.transform.childCount);
                    lootpanel.lootlistpanel.transform.GetChild(0).SetParent(lootpanel.conteiner.transform);
                }
            }
            else
            {
                Destroy(lootpanel.conteiner.gameObject);
            }
        }
    }



}
