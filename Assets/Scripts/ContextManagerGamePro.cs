using UnityEngine;
using System.Collections.Generic;

public class ContextManagerGamePro : MonoBehaviour {

	public string PreviousScene { get; set; }
    public Profile Profile { get; set; }
    public Map Map { get; set; }
    public List<Unit> unitslist = new List<Unit>();
    public List<Ship> shiplist = new List<Ship>();
    public Vector3 navpoint;
    public Ship playership;
    public Ship selectedship;
    public SpaceUI SpaceUi;
    public GameObject playershipobj;

    public Select.selecttype SelectedType;    

    void Start () {
        _context = this;
        DontDestroyOnLoad(this.gameObject);
	}

    void Update()
    {
    }

    private static ContextManagerGamePro _context;
    public static ContextManagerGamePro Instance() { return _context; }


}
