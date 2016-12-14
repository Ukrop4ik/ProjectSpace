using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {

    public bool Autotarget;
    [SerializeField]
    GameObject fire_point;
    [SerializeField]
    GameObject bullet;
    public Transform target;
    public Transform LookPoint;
    public string Id;
    public Ship ship;
    public string weaponname = "";
    float dist;
    public int damagetype = 1;
    public int damage = 0;
    public float reloadtime;
    public float RotationSpeed;
    public float RotationSpeedMod = 1;
    public float Range;
    public float RangeMod = 1;
    public float Reload;
    public float ReloadMod = 1;
    public int energycost = 1;
    public int shootcount = 1;
    public float shootdelay = 0.2f;
    private float _accuracy = 0;
    public float accuracy = 100;
    [HideInInspector]
    public bool activate = false;
    public bool isTarget = false;
    public bool isReload = false;
    public bool TargetInLine = false;
    public bool lowenergy = false;

    Queue<ShootElement> shootqueue = new Queue<ShootElement>();

    void Start()
    {
        ship = transform.parent.parent.GetComponent<ComponentSlot>().ship;
        transform.localPosition = Vector3.zero;
        reloadtime = Reload;
    }
    [ContextMenu("Shoot")]
    public void Shoot()
    {
        if (lowenergy) return;

        ship.energy -= energycost;

        CreateBullet();
    }
    void Update()
    {
        _accuracy = Mathf.Max(0, _accuracy - Time.deltaTime);

        if (!ship)
        {
            ship = transform.parent.parent.GetComponent<ComponentSlot>().ship;
        }

        reloadtime -= Time.deltaTime * ReloadMod;

        if (reloadtime <= 0)
        {
            isReload = false;
            reloadtime = 0;
        }
        else if (reloadtime > 0 && shootqueue.Count == 0 && !isReload)
        {
            isReload = true;
            for (int i = 0; i < shootcount; i++)
            {
                shootqueue.Enqueue(new ShootElement(shootdelay));
            }
        }

        if (ship.energy < energycost)
        {
            lowenergy = true;
            
        }
        else
        {
            lowenergy = false;
        }

        if (target)
        {
            isTarget = true;
            TargetInLine = TurretMotion(Vector3.Distance(target.position, transform.position), Range, RangeMod, RotationSpeedMod);

            if (TargetInLine && !isReload)
            {

                if (shootqueue.Count > 0)
                {
                    var shhot = shootqueue.Peek();
                    shhot.shoottime -= Time.deltaTime;

                    if (shhot.shoottime <= 0)
                    {
                        Shoot();
                        shootqueue.Dequeue();
                    }
                }
                else
                {
                    reloadtime = Reload;
                }
            }

        }
        else
        {
            isTarget = false;
            ReturnPos();
        }

        if (!isTarget)
        {
            target = null;
        }

    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
    bool TurretMotion(float dist, float range, float rangemod, float rotationmod)
    {               
        if (dist < range * rangemod)
        {

            Vector3 targetdirection = target.transform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetdirection, ((RotationSpeed * 0.1f) * Time.deltaTime)*rotationmod, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);

            Vector3 fwd = fire_point.transform.TransformDirection(Vector3.forward);

            RaycastHit hit;

            int NPClayer = 9;
            int NpcMask = 1 << NPClayer;
            bool test;

            if (Physics.Raycast(fire_point.transform.position, fwd, out hit, 1000, NpcMask))
            {


                if (hit.collider.gameObject == target.gameObject)
                {
                    
                    test = true;

                }
                else
                {
                    //Debug.Log("Target not in line!: " + hit.collider.gameObject);
                    test = false;
                }
                return test;
            }           
        }
        return false;

    }
    void ReturnPos()
    {
        if (LookPoint)
        {
            Vector3 targetdirection = LookPoint.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetdirection, (RotationSpeed * 0.1f) * Time.deltaTime, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

    }
  
    void CreateBullet()
    {
        GameObject _bullet = Instantiate(bullet);

        _bullet.transform.position = fire_point.transform.position;
        _bullet.transform.rotation = fire_point.transform.rotation;

        _bullet.GetComponent<Bullet>().Damage = damage;
        _bullet.GetComponent<Bullet>().DamageType = damagetype;
        _bullet.GetComponent<Bullet>().Aship = ship;


        _bullet.transform.Rotate(new Vector3(0, Random.Range(-_accuracy, _accuracy), 0));
        _accuracy = Mathf.Min(5, _accuracy + Time.deltaTime * accuracy);

        _bullet.gameObject.GetComponent<Rigidbody>().AddForce(_bullet.transform.forward * 250, ForceMode.Impulse);

    }

    public class ShootElement
    {
        public float shoottime;

        public ShootElement(float time)
        {
            this.shoottime = time;
        }
    }

}
