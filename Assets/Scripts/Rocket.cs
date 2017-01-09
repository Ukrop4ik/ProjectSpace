using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

    public int Damage;
    public int DamageType;
    public float lifetime;
    public Ship Aship;
    public Ship targetship;
    public float uptime;
    public float impuls;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        uptime -= Time.deltaTime;

        

        if (uptime > 0)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime, transform.position.y + Time.deltaTime * 1.5f, transform.position.z);
            return;
        } 

        lifetime -= Time.deltaTime;
        if (targetship)
        {
            transform.LookAt(targetship.transform);
            GetComponent<Rigidbody>().AddForce(transform.forward * impuls, ForceMode.Force);
        }
        else
        {
            Destroy(gameObject);
        }


        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }

        int NPClayer = 9;
        int NpcMask = 1 << NPClayer;

        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, 3, NpcMask))
        {

            if (hit.collider.gameObject.tag == "Ship" && hit.collider.gameObject != Aship.gameObject)
            {


                Ship ship = hit.collider.gameObject.GetComponent<Ship>();
                ship.Damage(Aship, Damage, DamageType);


            }

            Destroy(this.gameObject);
        }
    }
}
