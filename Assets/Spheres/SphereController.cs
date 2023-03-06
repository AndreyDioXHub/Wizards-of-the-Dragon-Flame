using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    [SerializeField]
    private List<Material> _iceMTLs;

    [SerializeField]
    private Vector2 _offcet;
    [SerializeField]
    private float _speed = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _offcet.x += _speed * Time.deltaTime;
        _offcet.y += _speed * Time.deltaTime;

        if (_offcet.x > 10)
        {
            _offcet.x = 0;
        }
        if (_offcet.y > 10)
        {
            _offcet.y = 0;
        }
        foreach(var ice in _iceMTLs)
        {
            ice.SetVector("_Offset", _offcet);
        }
    }
}
