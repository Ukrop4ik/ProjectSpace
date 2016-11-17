using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public Vector2 debudmoduleVector;
    int countelementtobuild;

    public ShipComponent component;

    public GameObject gridelement;

    public bool placeMod = true;

    public float elementsize = 0;
    public int gridX = 0;
    public int gridY = 0;
    public float positionXstep = 0;
    public float positionYstep = 0;
    
    public GameObject[,] grid;

    public int testA = 0;
    public int testB = 0;

    Vector2 buildXY;

    void Start()
    {
        grid = new GameObject[gridX, gridY];
        float Xstep = 0;
        float Ystep = 0;

        int i = 0;
        int a = 0;

        for (i = 0;  i < gridX; i++)
        {

            for (a = 0; a < gridY; a++)
            {

                grid[i, a] = Instantiate(gridelement);
                grid[i, a].GetComponent<GridElementData>().gridelement = grid[i, a];               
                grid[i, a].GetComponent<GridElementData>().gridelement.transform.SetParent(this.transform);
                grid[i, a].GetComponent<GridElementData>().gridelement.transform.localPosition = new Vector3(Xstep, 0, Ystep);
                grid[i, a].GetComponent<GridElementData>().pos = new Vector2(i, a);
                Xstep += positionXstep;
            }
            Xstep = 0;
            Ystep += positionYstep;
        }
    }
    void Update()
    {
        if (placeMod)
        {
            if (component)
            {

            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

                if (hit.collider.tag == "Grid")
                {

                    int Xpos = (int)hit.collider.gameObject.GetComponent<GridElementData>().pos.x;
                    int Ypos = (int)hit.collider.gameObject.GetComponent<GridElementData>().pos.y;

                    if (CanPlaceInGrid(gridX, gridY, hit.collider.gameObject.GetComponent<GridElementData>()) && grid[Xpos, Ypos].GetComponent<GridElementData>().isEmpty)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {

                            int x = Xpos;
                            int y = Ypos;

                            GameObject obj = Instantiate(component).gameObject;
                            obj.transform.SetParent(grid[x, y].transform);
                            obj.transform.position = grid[x, y].transform.position;
                            obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y -10, this.transform.position.z);

                            for (x = Xpos; x < Xpos + testA; x++)
                            {
                                for (y = Ypos; y < Ypos + testB; y++)
                                {
                                    grid[x, y].gameObject.GetComponent<GridElementData>().isEmpty = false;
 
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private bool IsIn(int x, int y, int w, int h)
    {
        return x >= 0 && x < w && y >= 0 && y < h;
    }

    bool CanPlaceInGrid(int w, int h, GridElementData element)
    {
        for (var sy = 0; sy < testB; ++sy)
        {
            for (var sx = 0; sx < testA; ++sx)
            {
                Debug.Log("" + element.pos.x + " " + element.pos.y);
                var x = (int)(element.pos.x + 0.5f) + sy;
                var y = (int)(element.pos.y + 0.5f) + sx;
                if (!IsIn(x, y, w, h) || !grid[x, y].GetComponent<GridElementData>().isEmpty)
                {
                   
                    return false;
                }
            }
        }
        return true;
    }
}
