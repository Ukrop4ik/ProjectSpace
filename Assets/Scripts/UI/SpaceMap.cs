using UnityEngine;
using System.Collections;

public class SpaceMap : MonoBehaviour {

    public int xSize = 0;
    public int ySize = 0;
    public GameObject gridelement;
    public MapGridElement[][] grid;

    [ContextMenu("CreateGrid")]
    public void CreateGrid()
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int b = 0; b < ySize; i++)
            {
                GameObject obj = Instantiate(gridelement);
                //obj.transform.SetParent(this.transform.GetChild(0));
            }
        }
    }
}
