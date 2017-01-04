using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Objectiv : MonoBehaviour {


    public Text objectivtext;
    public Text objectivcounter;

    string maxvaluestring = "";

    bool end;

    public void CreateText(string text)
    {
        objectivtext.text = text;
    }
    public void CreateCounter(int maxvalue)
    {
        objectivcounter.text = "0" + " / "  + maxvalue.ToString();
        maxvaluestring = maxvalue.ToString();

        if (maxvalue == 0)
        {
            objectivcounter.text = "";
        }
    }
    public void UpdateCounter(int count)
    {
        if (end) return;
        objectivcounter.text = count.ToString() + " / " + maxvaluestring;
    }
    [ContextMenu("End")]
    public void End()
    {
        objectivtext.color = Color.green;
        objectivcounter.color = Color.green;
        end = true;
    }
    [ContextMenu("Cancel")]
    public void Cancel()
    {
        objectivtext.color = Color.red;
        objectivcounter.color = Color.red;
    }

}
