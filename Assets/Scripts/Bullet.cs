﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{


    public int Damage;
    public int DamageType;
    public float distancelife;
    public Ship Aship;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        var dl = Vector3.forward * (Time.deltaTime * 1000);
        distancelife -= dl.magnitude;


        if (distancelife <= 0)
        {
            Destroy(gameObject);
        }

        int NPClayer = 9;
        int NpcMask = 1 << NPClayer;

        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, 3, NpcMask))
        {

            if (hit.collider.gameObject.tag == "Ship")
            {


                Ship ship = hit.collider.gameObject.GetComponent<Ship>();
                ship.Damage(Aship, Damage, DamageType);
            

            }

            Destroy(this.gameObject);
        }
    }
}
