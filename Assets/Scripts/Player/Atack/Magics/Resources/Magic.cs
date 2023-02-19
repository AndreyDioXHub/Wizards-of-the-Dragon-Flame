using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField]
    protected MagicInfo _magicInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"cast {_magicInfo.name}: {_magicInfo.power}");
    }

    public void Init()
    {
    }

    public void UpdateInfo(MagicInfo magicInfo)
    {
        _magicInfo = magicInfo;
        name = $"c:{_magicInfo.name}:{_magicInfo.direction}:{_magicInfo.power}";
    }

    public void DestroyMagic()
    {
        Destroy(gameObject);
    }
}

[Serializable]
public class MagicInfo
{
    public string name;
    public CastDirection direction;
    public int power;

    public MagicInfo(string name, CastDirection direction, int power)
    {
        this.name = name;
        this.direction = direction;
        this.power = power;
    }

}
