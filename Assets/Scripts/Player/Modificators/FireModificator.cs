using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModificator : SphereModificator
{
    public override void CheckCancel(string sphere)
    {
        base.CheckCancel(sphere);

        if(sphere == SpheresElements.water.ToString())
        {
            Destroy(this);
        }
    }
}
