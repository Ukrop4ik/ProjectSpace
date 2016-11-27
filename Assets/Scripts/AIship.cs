using UnityEngine;
using System.Collections;

public class AIship : MonoBehaviour
{

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

    public float kinetikarmorvalue = 0;
    public float energyarmorvalue = 0;
    public float explosivearmorvalue = 0;

    public Ship playership;
    SceneRes sceneres;
    bool isPlayer = false;

    NavMeshAgent agent;

    void Start()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.stoppingDistance = 20;
        HP = maxHP;

        sceneres = GameObject.Find("Scene").GetComponent<SceneRes>();
        sceneres.enemis.Add(this.gameObject);

        InvokeRepeating("SlowUpdate", 0, 0.2f);
    }

    void Update()
    {

    }

    void SlowUpdate()
    {
        if (ContextManagerGamePro.Instance().playership != null && !playership)
        {
            playership = ContextManagerGamePro.Instance().playership;
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }
      
        if (isPlayer)
        {
            agent.destination = playership.transform.position;
        }
    }

    public void Damage(int value, int damagetype)
    {

        if (damagetype <= 3)
        {

            if (shield > 0)
            {
                shield -= value;
            }
            else
            {
                Debug.Log((int)(value * kinetikarmorvalue));
                HP -= (int)(value * kinetikarmorvalue);
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

    public void OnDestroy()
    {
        sceneres.enemis.Remove(this.gameObject);
    }
}
