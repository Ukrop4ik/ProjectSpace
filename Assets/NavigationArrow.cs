using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NavigationArrow : MonoBehaviour {

    public Transform player;
    public Transform target;


	// Use this for initialization
	void Start () {

       
	}

    // Update is called once per frame
    void Update() {

        if (target == null || player == null)
        {
            Destroy(this.gameObject);
            return;
        }

        if (Vector3.Distance(target.position, player.position) < 20)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
        }
        else 
        {
            this.gameObject.GetComponent<Image>().enabled = true;
        }
        var direction = (target.transform.position - player.transform.position);
        direction.y = direction.z;
        direction.z = 0;
        var rotator = Quaternion.FromToRotation(Vector3.up, direction);
        transform.rotation = rotator;
    }
}
