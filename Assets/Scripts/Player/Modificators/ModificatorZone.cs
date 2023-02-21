using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificatorZone : MonoBehaviour
{

    [SerializeField]
    private Tick _tick;

    [SerializeField]
    private string _element;
    [SerializeField]
    private int _power = 1;

    [SerializeField]
    private Collider _other;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateInfo(string element, int power)
    {
        _element = element;
        _power = power;
    }

    public void DestroyZone()
    {
        Destroy(gameObject);
    } 

    private void OnTriggerEnter(Collider other)
    {
        _tick.UpdateTick();
        _other = other;
        AddModificator();
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
                //Debug.Log("UpdateTick AddModificator ");
                MagicModel.Instance.AddModificator(_element, _power);
            }
        }
    }
}
