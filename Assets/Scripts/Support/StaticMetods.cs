using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StaticMetods : MonoBehaviour {

    public void UseButtonSwither(GameObject obj)
    {
        ObjectActivator.Activate(obj, obj.activeInHierarchy);
    }
    public void BackToPreviosScene()
    {
        SceneManager.LoadScene(ContextManagerGamePro.Instance().PreviousScene);
    }
}
