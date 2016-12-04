using UnityEngine;
using System.Collections;

public class AIship : MonoBehaviour
{
    Ship ship;
    Ship playership;
    private bool isPlayer;
    private SceneRes sceneres;
    public float AgroDist;
    public float DistanceToPlayer;
    public ShipLogicEnum Logic;

    public void UseWeaponVsPlayer()
    {
        if (isPlayer)
        {
            foreach (Weapon weapon in ship.ComponentController.ShipWeapons)
            {
                weapon.target = playership.transform;
            }

        }
    }
    public void UseWeaponVsTarget(Transform target)
    {
        foreach (Weapon weapon in ship.ComponentController.ShipWeapons)
        {
            weapon.target = target;
        }
    }

    void Start()
    {
        ship = GetComponent<Ship>();
        sceneres = GameObject.Find("Scene").GetComponent<SceneRes>();
        sceneres.enemis.Add(this.gameObject);
        InvokeRepeating("SlowUpdate", 0, 0.2f);

    }

    void SlowUpdate()
    {
        if (ContextManagerGamePro.Instance().playership != null)
        {
            playership = ContextManagerGamePro.Instance().playership;
            isPlayer = true;
        }
        else if (playership == null)
        {
            isPlayer = false;
        }

        if (isPlayer)
        {
            DistanceToPlayer = Vector3.Distance(this.gameObject.transform.position, playership.transform.position);
        }

        switch (Logic)
        {
            case ShipLogicEnum.Stay:
                ship.navtarget = this.transform.position;
                break;
            case ShipLogicEnum.MoveToPlayer:
                ship.navtarget = playership.transform.position;
                break;
            case ShipLogicEnum.MoveAndAttack:
                ship.navtarget = playership.transform.position;
                UseWeaponVsPlayer();
                break;
            default:
                break;
        }
    
    }
    void OnDestroy()
    {
        sceneres.enemis.Remove(this.gameObject);
        return;
    }

}
