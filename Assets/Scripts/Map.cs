using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour {

    public Button testbut;



    void Start()
    {
        ContextManagerGamePro.Instance().Map = gameObject.GetComponent<Map>();
    }

    public void StartBattle(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
