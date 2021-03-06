﻿using UnityEngine;
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
    float firedist = 1000;
    public GameObject targetobj;

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
    public void UseWeaponVsAI(Transform trans)
    {


        foreach (Weapon weapon in ship.ComponentController.ShipWeapons)
        {
            weapon.target = trans;
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

        switch (ship.Faction)
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

        if (Logic == ShipLogicEnum.ChaosAi)
        {
            if (targetobj != null)
            {
                if (!targetobj.activeInHierarchy)
                {
                    targetobj = null;
                }
                else
                {
                    ship.navtarget = targetobj.transform.position;
                    UseWeaponVsAI(targetobj.transform);
                }

            }

            else
            {
                List<GameObject> ships = new List<GameObject>();

                foreach (GameObject obj in sceneres.enemis)
                {
                    if (!obj) continue;

                    foreach (FactionEnum faction in ship.enemysfaction)
                    {
                        if (faction == (FactionEnum)obj.GetComponent<Ship>().Faction)
                        {

                            ships.Add(obj);
                        }
                    }
                }
                if (ships.Count > 0)
                {
                    foreach (GameObject _ship in ships)
                    {
                        int index = Random.Range((int)0, ships.Count);
                        targetobj = ships[index];
                        return;
                    }
                }
            }
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

        switch (ship.Faction)
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
