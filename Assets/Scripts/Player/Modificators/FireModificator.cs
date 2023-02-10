using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModificator : SphereModificator
{
    public override void Start()
    {
        base.Start();
        key = "fire";
        _element.Init(key);
    }

    public override void CheckCancel(string sphere, out bool isCancel)
    {
        base.CheckCancel(sphere, out isCancel);

        if(sphere == SpheresElements.water.ToString())
        {
            isCancel = true; 
            DestroyModificator();
        }
    }
}
