using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;

public class ResourceManager : MonoBehaviour

{
    private string jsonString;
    private JsonData itemData;

    

    void Start()
    {
        Item item = new Item(10, "test", new string[] {"dwd", "ddd", "d22" });

        itemData = JsonMapper.ToJson(item);

        File.WriteAllText(Application.dataPath + "/Test.json", itemData.ToString());

    }

    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string[] strings { get; set; }

        public Item(int id, string name, string[] strings)
        {
            this.id = id;
            this.name = name;
            this.strings = strings;
        }
    }
}




