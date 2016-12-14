using UnityEngine;
using System.Collections;

public class PointManager : MonoBehaviour {

    public GameObject navpoint;
    public Vector3 navcoord;
    bool pause = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pause = !pause;
            if (pause)
            {
                Time.timeScale = 0.1f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (hit.collider.tag == "Grid")
                {
                    GameObject obj = Instantiate(navpoint);
                    obj.transform.position = hit.point;
                    navcoord = hit.point;
                    ContextManagerGamePro.Instance().navpoint = navcoord;
                }
                
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag == "Select")
                {
                    Selection(hit.collider.gameObject.GetComponent<Select>());
                }
            }
            
        }
    }

    public void Selection(Select select)
    {
        switch (select.SelectType)
        {
            case Select.selecttype.Ship:
                ContextManagerGamePro.Instance().selectedship = select.ship;
                ContextManagerGamePro.Instance().SelectedType = Select.selecttype.Ship;
                break;
             default:
                break;
        }
    }
}
