using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

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
    public int firecount = 1;
    [HideInInspector]
    public float reload_timer;
    [HideInInspector]
    public bool activate = false;
    public bool isTarget = false;
    public bool isReload = false;
    public bool TargetInLine = false;
    public bool lowenergy = false;
    void Start()
    {
        reload_timer = Reload;
        ship = transform.parent.parent.GetComponent<ComponentSlot>().ship;
        transform.localPosition = Vector3.zero;
        reloadtime = 0;
        reloadtime = Reload;
    }
    [ContextMenu("Shoot")]
    public void Shoot()
    {
        if (isReload) return;
        if (lowenergy) return;

        ship.energy -= energycost;
        reloadtime = Reload;

        CreateBullet();


    }
    void Update()
    {

        if (!ship)
        {
            ship = transform.parent.parent.GetComponent<ComponentSlot>().ship;
        }

        reloadtime -= ReloadTimer(reloadtime, Time.deltaTime, ReloadMod);

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
        }
        else
        {
            isTarget = false;
            ReturnPos();
        }
            

        if (!activate) reload_timer -= Time.deltaTime;

        if (reload_timer <= 0)
        {
            activate = true;
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
                    Shoot();
                }
                else
                {
                    Debug.Log("Target not in line!: " + hit.collider.gameObject);
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
    float ReloadTimer(float timer, float time, float reloadmod)
    {
        if (timer <=0)
        {
            isReload = false;
            return timer;
        }
        else
        {
            isReload = true;
            return time * reloadmod;
        }
    }
    void CreateBullet()
    {
        GameObject _bullet = Instantiate(bullet);

        _bullet.transform.position = fire_point.transform.position;
        _bullet.transform.rotation = fire_point.transform.rotation;
        _bullet.gameObject.GetComponent<Rigidbody>().AddForce(_bullet.transform.forward * 250, ForceMode.Impulse);
    }

}
