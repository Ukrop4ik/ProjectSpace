using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Escape : MonoBehaviour
{
    bool status = false;

    public void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void OnDestroy()
    {
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void ToStation()
    {
        GameObject obj = GameObject.Find("GameContext");
        ContextManagerGamePro.Instance().playership.transform.SetParent(obj.transform.GetChild(0).transform);
        ContextManagerGamePro.Instance().navpoint = Vector3.zero;
        SceneManager.LoadScene("Station");
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

}
