using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereModificator : MonoBehaviour
{
    [SerializeField]
    protected string key;
    [SerializeField]
    protected ActiveElementView _element;


    public virtual void CheckCancel(string sphere, out bool isCancel)
    {
        isCancel = false;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        DisableView.Instance.AddNewDisable(key, out _element);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void DestroyModificator()
    {
        Destroy(_element.gameObject);
        Destroy(this);
    }
}
