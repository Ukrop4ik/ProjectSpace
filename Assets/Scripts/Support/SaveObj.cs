using UnityEngine;
using System.Collections;

public class SaveObj  {

    public string Type;
    public string ID;
    public float posX, posY, posZ;
    public float value;

    public SaveObj(string Id, string type)
    {
        this.ID = Id;
        this.Type = type;
    }
    public SaveObj(float _posx, float _posy, float _posz)
    {
        this.posX = _posx;
        this.posY = _posy;
        this.posZ = _posz;
    }
    public SaveObj(float value)
    {
        this.value = value;
    }
}
