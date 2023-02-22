using com.czeeep.network;
using com.czeeep.spell.magicmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereWorld : MonoBehaviour
{
    [SerializeField]
    private SpheresElements _element;
    [SerializeField]
    private int _count;
    [SerializeField]
    private SpriteRenderer _image;

    int m_index = -1;
    string hashKey = string.Empty;
    BitSphere bit_sphere;

    public bool SilentDestroy { get; internal set; } = false;

    private void Start()
    {
        
    }

    public void Init(SpheresElements element, int count, int _index = -1)
    {
        m_index = _index;
        _element = element;
        _count = count;
        switch (element)
        {
            case SpheresElements.life:
                _image.color = new Color(0, 1, 0, 1);
                break;
            case SpheresElements.fire:
                _image.color = new Color(1, 0.33f, 0, 1);
                break;
            case SpheresElements.water:
                _image.color = new Color(0.56f, 0.62f, 1, 1);
                break;
            case SpheresElements.earth:
                _image.color = new Color(0.54f, 1, 0.38f, 1);
                break;
            case SpheresElements.freze:
                _image.color = new Color(0.85f, 1, 0.9f, 1);
                break;
            case SpheresElements.razor:
                _image.color = new Color(0, 1, 1, 1);
                break;
            case SpheresElements.dark:
                _image.color = new Color(0, 0, 0, 1);
                break;
            case SpheresElements.shield:
                _image.color = new Color(1, 0.9f, 0, 1);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //var inventory = other.GetComponent<MagicModel>();

            MagicModel.Instance.AddSphere(_element.ToString(), _count);
            //Debug.Log($"SphereWorld: Added {_element}: {_count}");
            Destroy(gameObject);
        }
    }

    internal byte[] GetHashData() {
        byte[] bmask = BitSphere.ConvertSphere6((int)_element, _count, transform.position);
        bit_sphere = BitSphere.ConvertToSphere(bmask);
        UpdatepositionByBitSphere();
        return bmask;

    }

    private void UpdatepositionByBitSphere() {
        if(bit_sphere != null) {
            Vector3 pos = transform.position;
            Vector3 bpos = bit_sphere.GetPosition();
            bpos.y = pos.y;
            transform.position = bpos;
        }
    }

    internal void SetIndex(string key) {
        hashKey = key;
    }

    public int GetElementType() {
        return (int)_element;
    }

    void OnDestroy() {
        if(!SilentDestroy) {
            GameManager.Instance.sphereManager.WillDestroyed(m_index);
        }
    }

    void Update()
    {
        
    }
}
