using UnityEngine;
using System.Collections.Generic;

public class Ship : MonoBehaviour {


    // Корабль обновляет состояние раз в 1 сек

    public string itemID;
    public float brakeAcc;
    public Vector3 navtarget;
    public float navtargetdist;
    NavMeshAgent agent;
    public ComponentController ComponentController;
    public Cargo cargo { get; set; }

    public bool isPlayerShip = false;
    public bool shieldregenbool = true;
    public bool ismove = false;
    bool inStorage = false;

    public bool dead = false;

    public bool isAiShip = false;

    public string shipname = "";

    public int maxHP = 100;
    public int HP = 0;

    public float maxspeed = 5;
    public float speed = 0;
    public float acceleration = 1;

    public int maxshield = 0;
    public int shield = 0;
    public int shieldregen = 0;
    public int shieldregencost = 15;

    public int maxenergy = 100;
    public int energy = 0;
    public int enegyregen = 0;

    public FactionEnum Faction;

    public Dictionary<int, float> armor = new Dictionary<int, float>();

    public float kinetikarmorvalue = 0;
    public float energyarmorvalue = 0;
    public float explosivearmorvalue = 0;
    public Ship playership;
    private bool isPlayer;
    private SceneRes sceneres;
    public List<FactionEnum> enemysfaction = new List<FactionEnum>();
    public string actiongroup;
    public Dictionary<Ship, int> agrolist = new Dictionary<Ship, int>();

    void Start()
    {

        if (GetComponent<AIship>())
        {
            isAiShip = true;
        }

        agent = GetComponent<NavMeshAgent>();
        cargo = GetComponent<Cargo>();

        armor.Add(1, 1f);
        armor.Add(2, 1f);
        armor.Add(3, 1f);
        InvokeRepeating("DebugUpdate", 0, 1f);

        HP = maxHP;

        switch (Faction)
        {
            case FactionEnum.Pirate:
                enemysfaction.Add(FactionEnum.Guard);
                enemysfaction.Add(FactionEnum.Player);
                enemysfaction.Add(FactionEnum.Prisoner);
                break;
            case FactionEnum.Guard:
                enemysfaction.Add(FactionEnum.Pirate);
                break;
            case FactionEnum.Prisoner:
                enemysfaction.Add(FactionEnum.Pirate);
                break;
            default:
                break;
        }

    }



    void Update()
    {

        if (transform.parent != null)
        {
            inStorage = true;
            agent.enabled = false;

            return;
        }
        else
        {
            inStorage = false;
            agent.enabled = true;
        }
    

        if (shield < 0) shield = 0;
        if (energy < 0) energy = 0;
        if (HP < 0) HP = 0;
        if (speed < 0) speed = 0;

        if (speed > maxspeed) speed = maxspeed;
        if (shield > maxshield) shield = maxshield;
        if (HP > maxHP) HP = maxHP;
        if (energy >= maxenergy) energy = maxenergy;

        if (shield == maxshield) shieldregenbool = false;

        if (!agent.enabled) return;
        if (navtarget != null )
        {
            navtargetdist = agent.remainingDistance;
            agent.destination = navtarget;
        }
        

        if (agent.remainingDistance == 0)
        {
            ismove = false;
        }
        else
        {
            ismove = true;
            agent.destination = navtarget;
        }


        agent.speed = speed;
        agent.acceleration = speed;
    }

    public void DebugUpdate()
    {
        if (!gameObject.activeInHierarchy) return;

        if (gameObject.transform.parent != null) return;
        if (inStorage) return;

        if (!ContextManagerGamePro.Instance().shiplist.Contains(this))
            ContextManagerGamePro.Instance().shiplist.Add(this);
        if (isPlayerShip)
        {
            if (!ContextManagerGamePro.Instance().playership)
                ContextManagerGamePro.Instance().playership = this;
        }

        brakeAcc = maxspeed * maxspeed / (2 * 10);

        if (dead)
        {

        }
        else
        {
            if (isPlayerShip)
            {
                if (ContextManagerGamePro.Instance().navpoint != null)
                {
                    navtarget = ContextManagerGamePro.Instance().navpoint;
                }
            }

            if (agent.enabled && inRange(BrakingDistance(agent.remainingDistance, brakeAcc)))
            {
                speed -= Mathf.Min(speed, brakeAcc);
            }
            else
            {
                speed += acceleration;
            }

            float value = 1;
            if (armor.TryGetValue(1, out value)) kinetikarmorvalue = value;
            if (armor.TryGetValue(2, out value)) energyarmorvalue = value;
            if (armor.TryGetValue(3, out value)) explosivearmorvalue = value;

            energy += enegyregen;

            if (shieldregenbool && energy >= shieldregencost)
            {
                if (shield < maxshield)
                {
                    shield += shieldregen;
                    energy -= shieldregencost;
                }
            }
            else if (shieldregenbool)
            {
               // Debug.Log("No energy to regen shield!");
            }

        }

    }

    public void Damage(Ship ship, int value, int damagetype)
    {

        AddToAgroList(ship, value);

        if (damagetype <= 3)
        {

            float _value = 1;
            armor.TryGetValue(damagetype, out _value);

            if (shield > 0)
            {
                shield -= value;
            }
            else
            {
                HP -= (int)(value * _value);
            }

            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        if (damagetype == 4)
        {
            shield -= value;
        }
        if (damagetype == 5)
        {
            energy -= value;
        }

    }

    void AddToAgroList(Ship ship, int value)
    {
        if (agrolist.Count == 0)
        {
            agrolist.Add(ship, value);
            return;
        }

        if (!agrolist.ContainsKey(ship))
        {
            agrolist.Add(ship, value);

        }
        else
        {
            agrolist[ship] += value;

        }

    }

    public float BrakingDistance(float speed, float brakeAcc)
    {
        return speed *speed / (2.0f * brakeAcc);
    }

    bool inRange(float range)
    {
        return Vector3.SqrMagnitude(transform.position - navtarget) >= range * range;
    }

    void OnDestroy()
    {
        if (isPlayerShip)
        ContextManagerGamePro.Instance().playership = null;
       // conteiner.SetActive(true);
       // conteiner.transform.SetParent(null);
    }

    public void Save()
    {
        
    }

    public void SetAgentStopping(float value)
    {
        agent.stoppingDistance = value;
    }
    public void EnableAgent()
    {
        agent.enabled = true;
    }
}
