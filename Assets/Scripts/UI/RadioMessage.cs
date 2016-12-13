using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadioMessage : MonoBehaviour {

    public Text header;
    public Text radiotext;
    public Image image;
    public float timetoshow;

    public void Create(string headerID, string radioID,  Sprite sprite)
    {

      //  Debug.Log(headerID + " " + radioID);

        header.text = headerID;
        radiotext.text = radioID;
        image.overrideSprite = sprite;

      //  Invoke("Off", timetoshow);

        this.gameObject.SetActive(true);
    }

    void Off()
    {
        this.gameObject.SetActive(false);
    }
}
