using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    public GameObject navtarget;
    public GameObject target;
    public Weapon weapon;

    NavMeshAgent agent;

    public bool isplayerUnit = false;
    public bool shieldregenbool = true;
    public bool ismove = false;

    public bool testDamage = false;
    public bool dead = false;
    public int testDamageValue = 10;
    public int testdamagetype = 1;

    public string unitname = "";

    public int HP = 100;
    public int maxHP = 100;

    public int maxshield = 100;
    public int shieldregen = 10;
    public int shield = 0;
    public int shieldregencost = 15;

    public int maxenergy = 100;
    public int energy = 100;
    public int enegyregen = 5;




    public Dictionary<int, float> armor = new Dictionary<int, float>();

    public float kinetikarmorvalue = 0;
    public float energyarmorvalue = 0;
    public float explosivearmorvalue = 0;

    void Start () {

        agent = GetComponent<NavMeshAgent>();

        armor.Add(1, 1f);
        armor.Add(2, 1f);
        armor.Add(3, 1f);
        InvokeRepeating("DebugUpdate", 0, 0.5f);
        try
        {
            if (isplayerUnit)
            {
                if (!ContextManagerGamePro.Instance().unitslist.Contains(this))
                    ContextManagerGamePro.Instance().unitslist.Add(this);
            }
        }
        catch
        {
            Debug.LogError("Profile not found! Cant add Unit to profile!");
        }

    }

    public void Damage(int value, int damagetype)
    {

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
                Dead();
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

    void Update()
    {
        if (shield < 0) shield = 0;
        if (energy < 0) energy = 0;
        if (HP < 0) HP = 0;

        if (shield > maxshield) shield = maxshield;
        if (HP > maxHP) HP = maxHP;
        if (energy > maxshield) energy = maxenergy;

        if (shield == maxshield) shieldregenbool = false;


    }

    public void DebugUpdate()
    {
        if (dead)
        {
            Dead();
        }
        else
        {
            if (ismove && navtarget != null)
            {
                agent.destination = navtarget.transform.position;
            }
            else
            {
                agent.destination = this.transform.position;
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

            if (testDamage) Damage(testDamageValue, testdamagetype);

            Debug.Log("Update Unit");
        }
        
    }

    public void Dead()
    {
        agent.destination = new Vector3(0,0,0);

        if (ContextManagerGamePro.Instance().unitslist.Contains(this))
            ContextManagerGamePro.Instance().unitslist.Remove(this);

        Destroy(this.gameObject);
        Debug.Log("Unit " + unitname + " dead!");
    }
}
