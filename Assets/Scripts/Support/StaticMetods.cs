using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using LitJson;
using System.IO;

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
    public void EnableObject(GameObject obj)
    {
        obj.SetActive(true);
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
    [ContextMenu("LoadShipData")]
    public void LoadShipData()
    {
        SaveManager.CreateShipFromFile();
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

    [ContextMenu("CreateItemList")]
    public void CreateItemList()
    {
        ItemInList emptyitem = new ItemInList("emtyID", 0);
        ItemList list = new ItemList(new List<ItemInList> {emptyitem});

        JsonData jsonData = JsonMapper.ToJson(list);
        File.WriteAllText(Application.streamingAssetsPath + "/items.json", jsonData.ToString());
    }

    public List<string> GetItemFromList(int _rar, int _count)
    {
        string jsonstring = File.ReadAllText(Application.streamingAssetsPath + "/items.json");
        JsonData data = JsonMapper.ToObject(jsonstring);

        int rar = _rar;
        int count = Random.Range((int)1, _count + 1);

        List<ItemInList> list = new List<ItemInList>();
        List<ItemInList> sorteditem = new List<ItemInList>();
        List<string> finalitemID = new List<string>();

        for (int i = 0; i < data["items"].Count; i++)
        {
            ItemInList item = new ItemInList(data["items"][i]["ID"].ToString(), (int)data["items"][i]["Rare"]);
            list.Add(item);
        }

        foreach (ItemInList item in list)
        {
            if (item.Rare <= rar)
            {
                sorteditem.Add(item);
            }
            else
            {
                continue;
            }
        }

        for (int i = 0; i < count; i++)
        {
            int indx = Random.Range(0, sorteditem.Count);
            finalitemID.Add(sorteditem[indx].ID);
        }

        return finalitemID;
    }

    public class ItemList
    {
        public List<ItemInList> items;

        public ItemList(List<ItemInList> items)
        {
            this.items = items;
        }
    }
    public class ItemInList
    {
        public string ID;
        public int Rare;

        public ItemInList(string Id, int rare)
        {
            this.ID = Id;
            this.Rare = rare;
        }
    }
}
