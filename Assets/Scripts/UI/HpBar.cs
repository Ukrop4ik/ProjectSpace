using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpBar : MonoBehaviour {

    public GameObject obj;
    public Image shield;
    public Image HP;
    void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position);
    }
}
