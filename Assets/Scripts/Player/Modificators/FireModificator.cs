using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModificator : SphereModificator
{
    public override void CheckCancel(string sphere, out bool isCancel)
    {
        base.CheckCancel(sphere, out isCancel);

        if(sphere == SpheresElements.water.ToString())
        {
            isCancel = true;
            Destroy(this);
        }
    }
}
