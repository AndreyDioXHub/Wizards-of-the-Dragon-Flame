using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereModificator : MonoBehaviour
{/*
    [SerializeField]
    private SpheresElements _element;*/

    public virtual void CheckCancel(string sphere, out bool isCancel)
    {
        isCancel = false;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
