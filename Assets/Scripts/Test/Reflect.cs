using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    public float dot;
    public Transform hitpoint;
    public Transform reclected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        dot = Vector3.Dot(transform.forward, reclected.forward);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Vector3 reflect = Vector3.Reflect(transform.forward, hit.normal);
            reflect = reflect * Vector3.Distance(hit.point, transform.position);
            hitpoint.position = hit.point;

            Debug.DrawLine(hit.point, hit.point + reflect, Color.green);
        }
    }
}
