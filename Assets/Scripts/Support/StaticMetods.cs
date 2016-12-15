using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;

public class StaticMetods : MonoBehaviour {

    [ContextMenu("CreateFactionTestInstance")]
    public void CreateFactionTestInstance()
    {

    }

    public void DestroyObj(GameObject obj)
    {
        Destroy(obj);
    }
    public void DesableObjects(GameObject obj)
    {
        obj.SetActive(false);
    }
    [ContextMenu("OpenProfileFolder")]
    public void OpenProfileFolder()
    {
        Process.Start(Application.persistentDataPath);
    }

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

    public void SetProfileTutorial1(bool b)
    {
        ContextManagerGamePro.Instance().Profile.isTutorialPart1 = b;
    }
    public void SetProfileTutorial2(bool b)
    {
        ContextManagerGamePro.Instance().Profile.isTutorialPart2 = b;
    }
    public void SetProfileTutorial3(bool b)
    {
        ContextManagerGamePro.Instance().Profile.isTutorialPart3 = b;
    }
}
