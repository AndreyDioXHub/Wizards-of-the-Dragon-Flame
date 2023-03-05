using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _partAnchors = new List<Transform>();
    [SerializeField]
    private float _laserLenght = 20f;
    [SerializeField]
    private float _laserLenghtPisechkaPercent = 1.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        foreach (var part in _partAnchors)
        {
            float partScale = _laserLenght * _laserLenghtPisechkaPercent;

            if (Physics.Raycast(part.position, part.forward, out RaycastHit hit, _laserLenght))
            {
                Debug.DrawLine(part.position, hit.point, Color.red);
                partScale = (Vector3.Distance(part.position, hit.point) / _laserLenght);
                partScale = partScale * _laserLenght * _laserLenghtPisechkaPercent;

                part.localScale = new Vector3(1, 1, partScale);

            }
            else
            {
                Debug.DrawLine(part.position, part.position+ part.forward* _laserLenght* _laserLenghtPisechkaPercent, Color.red);
                part.localScale = new Vector3(1, 1, partScale);
            }
        }
    }
}