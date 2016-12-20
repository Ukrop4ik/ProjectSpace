using UnityEngine;
using System.Collections.Generic;

public class ContextManagerGamePro : MonoBehaviour {

    public Transform cargo;
    public string PreviousScene;
    public Profile Profile;
    public Map Map { get; set; }
    public List<Unit> unitslist = new List<Unit>();
    public List<Ship> shiplist = new List<Ship>();
    public List<ItemShip> itemships = new List<ItemShip>();
    public Vector3 navpoint;
    public Ship playership;
    public Ship selectedship;
    public SpaceUI SpaceUi;
    public GameObject playershipobj;
    public ResourceManager ResourceManager;
    public SaveManager SaveManager;
    public List<Localization> loca;
    public Select.selecttype SelectedType;
    public StaticMetods StaticMetods;
    void Start() {
        _context = this;
        DontDestroyOnLoad(this.gameObject);
        StaticMetods = GetComponent<StaticMetods>();
        ResourceManager = GetComponent<ResourceManager>();
        loca = new List<Localization>();
    }
    private static ContextManagerGamePro _context;
    public static ContextManagerGamePro Instance() { return _context; }

    public MiniMission loadedmission;


    [System.Serializable]
    public class ItemShip
    {
        public Ship ship;
        public int danger;
    }


}
