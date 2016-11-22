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
    [ContextMenu("SaveGame")]
    public void SaveGame()
    {
        SaveManager.SaveProfile(ContextManagerGamePro.Instance().Profile.profilename, ContextManagerGamePro.Instance().playership);
    }
    [ContextMenu("LoadProfileData")]
    public void LoadProfileData()
    {
        SaveManager.LoadProfile();
    }
    public string GetShipId(Ship ship)
    {
        return ship.itemID;
    }
}
