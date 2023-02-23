using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public static FollowCamera Instance;

    public Camera SelfCamera { get => _selfCamera; }

    [SerializeField]
    private Camera _selfCamera;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private bool _ignorePlayerHeight;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Init(Transform target)
    {
        _target = target;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            transform.position = _target.position + _offset;
        }

        if (_ignorePlayerHeight)
        {
            transform.position = new Vector3(transform.position.x, _offset.y, transform.position.z);
        }
    }
}
