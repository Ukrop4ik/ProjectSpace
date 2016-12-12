using UnityEngine;
using System.Collections.Generic;

public class AIship : MonoBehaviour
{
    Ship ship;
    Ship playership;
    private bool isPlayer;
    private SceneRes sceneres;
    public float AgroDist;
    public float DistanceToPlayer;
    public ShipLogicEnum Logic;
    public ShootLogicEnum ShootLogic;
    public FactionEnum Faction;
    float firedist = 1000;
    List<FactionEnum> enemysfaction = new List<FactionEnum>();

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
        Invoke("Firedist", 2);

        switch (Faction)
        {
            case FactionEnum.Pirate:
                sceneres.pirateships.Add(this.gameObject);
                break;
            case FactionEnum.Guard:
                sceneres.guardships.Add(this.gameObject);
                break;
            case FactionEnum.Prisoner:
                sceneres.prisonerships.Add(this.gameObject);
                break;
            default:
                break;
        }
    }

    void SlowUpdate()
    {
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
        if (ship.navtarget == null) return;
        switch (Logic)
        {
            case ShipLogicEnum.Stay:
                foreach (Weapon weapon in ship.ComponentController.ShipWeapons)
                {
                    weapon.target = null;
                    weapon.isTarget = false;
                }
                ship.navtarget = this.transform.position;
                break;
            case ShipLogicEnum.MoveToPlayer:
                if (playership)
                ship.navtarget = playership.transform.position;
                break;
            case ShipLogicEnum.MoveAndAttack:
                if (playership)
                {
                    ship.navtarget = playership.transform.position;
                    UseWeaponVsPlayer();
                }
                break;
            default:
                break;
        }

        switch (ShootLogic)
        {
            case ShootLogicEnum.inRange:
                break;
            default:
                break;
        }
    
    }
    void OnDestroy()
    {
        sceneres.enemis.Remove(this.gameObject);

        switch (Faction)
        {
            case FactionEnum.Pirate:
                sceneres.pirateships.Remove(this.gameObject);
                break;
            case FactionEnum.Guard:
                sceneres.guardships.Remove(this.gameObject);
                break;
            case FactionEnum.Prisoner:
                sceneres.prisonerships.Remove(this.gameObject);
                break;
            default:
                break;
        }

        return;
    }

    public void CreateArrow(ArrowTypeEnum type)
    {
        sceneres.CreateUiArrow(this.gameObject, type);
    }

    void Firedist()
    {
        foreach (Weapon weapon in ship.ComponentController.ShipWeapons)
        {
            if (weapon.Range < firedist)
            {
                firedist = weapon.Range;
            }          
        }
        ship.SetAgentStopping(firedist - 5);

    }
}
