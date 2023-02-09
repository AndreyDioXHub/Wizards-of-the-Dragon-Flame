using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWorld : MonoBehaviour
{
    [SerializeField]
    private SpheresElements _element;
    [SerializeField]
    private int _count;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var inventory = other.GetComponent<CastModel>();

            inventory.AddSphere(_element.ToString(), _count);
            //Debug.Log($"SphereWorld: Added {_element}: {_count}");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
