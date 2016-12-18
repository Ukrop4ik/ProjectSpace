using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {


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

    [ContextMenu("Create")]
    void Create()
    {
        grid = new GameObject[gridX, gridY];

        int i = 0;
        int a = 0;

        for (i = 0;  i < gridX; i++)
        {

            for (a = 0; a < gridY; a++)
            {

                grid[i, a] = Instantiate(gridelement);
                grid[i, a].GetComponent<GridElementData>().gridelement = grid[i, a];
                grid[i, a].GetComponent<GridElementData>().pos = new Vector2(i, a);
                grid[i, a].GetComponent<GridElementData>().text.text = i + " , " + a;
                grid[i, a].GetComponent<GridElementData>().gridelement.transform.SetParent(this.transform.GetChild(0));
                grid[i, a].transform.localScale = Vector3.one;
            }
        }
    }
    void Update()
    {
       
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

    [ContextMenu("CreateMission")]
    public void CreateMission()
    {
        int danger = 2;

    }
}
