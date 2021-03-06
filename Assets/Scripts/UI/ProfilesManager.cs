﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using LitJson;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ProfilesManager : MonoBehaviour {

    
    public Transform content;
    public Button NewProfileButton;
    public Button profilebuttonPrefab;
    public InputField inputNewProfileName;


    void Start()
    {

        if (File.Exists(Application.persistentDataPath + "/profilelist.json"))
        {
            string jsonstring = File.ReadAllText(Application.persistentDataPath + "/profilelist.json");
            JsonData jsonDataProfiles = JsonMapper.ToObject(jsonstring);

            for (int i = 0; i < jsonDataProfiles["profilesnames"].Count; i++)
            {
                Button but = Instantiate(profilebuttonPrefab);
                but.transform.SetParent(content.transform);
                Text text = but.transform.GetChild(0).gameObject.GetComponent<Text>();
                text.text = jsonDataProfiles["profilesnames"][i].ToString();
            }
        }
    }

    public void CreateNewProfile()
    {
        
        Button but = Instantiate(profilebuttonPrefab);
        but.transform.SetParent(content.transform);
        Text text = but.transform.GetChild(0).gameObject.GetComponent<Text>();
        text.text = inputNewProfileName.text;

        // добавление в список профилей, либо создание нового списка, если такой отсутствует
        if (!File.Exists(Application.persistentDataPath + "/profilelist.json"))
        {
            ProfileList _profiles = new ProfileList(new List<string>() {  });
            JsonData _jsonDataProfiles = JsonMapper.ToJson(_profiles);
            File.WriteAllText(Application.persistentDataPath + "/profilelist.json", _jsonDataProfiles.ToString());
        }

            string jsonstring = File.ReadAllText(Application.persistentDataPath + "/profilelist.json");
            JsonData jsonDataProfiles = JsonMapper.ToObject(jsonstring);

            List<string> newlist = new List<string>();

            for (int i = 0; i < jsonDataProfiles["profilesnames"].Count; i++)
            {
                newlist.Add(jsonDataProfiles["profilesnames"][i].ToString());
            }

            newlist.Add(text.text);

            ProfileList profiles = new ProfileList(newlist);
            jsonDataProfiles = JsonMapper.ToJson(profiles);
            File.WriteAllText(Application.persistentDataPath + "/profilelist.json", jsonDataProfiles.ToString());

            SaveManager.CreateEmptyProfile(text.text);
        

    }

    public void DeleteProfile(Text text)
    {

        string jsonstring = File.ReadAllText(Application.persistentDataPath + "/profilelist.json");
        JsonData jsonDataProfiles = JsonMapper.ToObject(jsonstring);

        List<string> newlist = new List<string>();

        for (int i = 0; i < jsonDataProfiles["profilesnames"].Count; i++)
        {
            newlist.Add(jsonDataProfiles["profilesnames"][i].ToString());
        }

        newlist.Remove(text.text);

        ProfileList profiles = new ProfileList(newlist);
        jsonDataProfiles = JsonMapper.ToJson(profiles);
        File.WriteAllText(Application.persistentDataPath + "/profilelist.json", jsonDataProfiles.ToString());
        File.Delete(Application.persistentDataPath + "/" + text.text + ".json");
    }

    public void ProfileButtonClick(Text buttontext)
    {
        CreateProfileObj(buttontext.text);
        ContextManagerGamePro.Instance().PreviousScene = "MainMenu";
        SceneManager.LoadScene("Station");
    }

   private void CreateProfileObj(string name)
    {
        GameObject profileobj = new GameObject();
        profileobj.name = "Profile";
        profileobj.AddComponent<Profile>();
        Profile created = profileobj.GetComponent<Profile>();
        created.profilename = name;
        ContextManagerGamePro.Instance().Profile = created;
 
    }

    private class ProfileList
    {
        public List<string> profilesnames;

        public ProfileList()
        {
        }

        public ProfileList(List<string> names)
        {
            this.profilesnames = names;
        }
    }
}

