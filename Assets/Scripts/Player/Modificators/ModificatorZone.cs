using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificatorZone : MonoBehaviour
{

    [SerializeField]
    private Tick _tick;

    [SerializeField]
    private SpheresElements _element;

    [SerializeField]
    private Collider _other;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _tick.UpdateTick();
        _other = other;
        //AddModificator();
    }
    private void OnTriggerExit(Collider other)
    {
        _other = null;
    }

    public void AddModificator()
    {
        if (_other != null)
        {
            if (_other.tag == "Player")
            {
                MagicModel.Instance.AddModificator(_element.ToString(), 1);
            }
        }
    }
}
