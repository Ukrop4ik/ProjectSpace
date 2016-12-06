using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour

{
    private string jsonString;
    public JsonData LocalizationData;

    void Start()
    {
       // CreateLocalizationFile();
        LoadLocalization();
    }

    [ContextMenu("LoadLocal")]
    public void LoadLocalization()
    {
        string jsonstring = File.ReadAllText(Application.dataPath + "/StreamingAssets" + "/Localization.json");
        LocalizationData = JsonMapper.ToObject(jsonstring);

   
    }

    public class LocalizationComplex
    {
        public List<LocalizationId> Localization;

        public LocalizationComplex()
        { }
        public LocalizationComplex(List<LocalizationId> idlist)
        {
            this.Localization = idlist;
        }
    }

    public class LocalizationId
    {
        public Dictionary<string, Trans> Translate;

        public LocalizationId(Dictionary<string, Trans> Translate)
        {
            this.Translate = Translate;
        }
    }

    public class Trans
    {
        public string English;
        public string Russian;

        public Trans()
        { }
        public Trans(string eng, string rus)
        {
            this.English = eng;
            this.Russian = rus;
        }
    }

    [ContextMenu("CreateLocalizationFile")]
   public void CreateLocalizationFile()
    {
        Debug.Log("persistentDataPath: " + Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + "/Localization.json")) return;
        File.WriteAllText(Application.persistentDataPath + "/Localization.json", File.ReadAllText(Application.dataPath + "/Resources/Localization.json"));
        Debug.Log(Application.persistentDataPath + "/Localization.json" + " file create");
    }
}




