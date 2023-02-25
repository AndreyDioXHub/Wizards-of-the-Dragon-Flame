using com.czeeep.network;
using com.czeeep.spell.biom;
using com.czeeep.spell.magicmodel;
using Photon.Pun;
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

    [Header("Config:")]
    [SerializeField]
    float startHeight = 120;
    [SerializeField]
    float rayDistance = 500;
    [SerializeField]
    float adjustHeight = 1.4f;

    int m_index = -1;
    string hashKey = string.Empty;
    BitSphere bit_sphere;

    public bool SilentDestroy { get; internal set; } = false;
    #region MonoBehaviour callbacks

    
    private void Awake()
    {
        //SetStartPosition();
    }

    private void Start() {
        //GetBiomUnderSphere();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Network variant
            if(PhotonNetwork.IsConnected) {
                GameManager.Instance.sphereManager.WillDestroyed(m_index);
            } else {
                MagicModel.Instance.AddSphere(_element.ToString(), _count);
                //Debug.Log($"SphereWorld: Added {_element}: {_count}");
                Destroy(gameObject);
            }

        }
    }

    void OnDestroy() {
        
    }

    void Update() {

    }

    #endregion

    #region Private Methods
    public void GetBiomUnderSphere() {
        SetStartPosition();
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, rayDistance);

        if (hit.collider != null) {
            
            //If biom
            if(hit.collider.tag == ModificatorZone.Tag) {
                
                //Detect count state
                ModificatorZone mz = hit.collider.gameObject.GetComponent<ModificatorZone>();
                //mz.ZoneBiom
                Debug.Log($"<b>Found biom {mz.ZoneBiom}</b>");
            }
            //Then get height
            Vector3 pos = transform.position;
            Debug.Log($"Exist collider point height = {hit.point}, current Y: {transform.position.y}");
            pos.y = hit.point.y + adjustHeight;
            transform.position = pos;
        }
    }

    private void SetStartPosition() {
        Vector3 pos = transform.position;
        pos.y = startHeight;
        transform.position = pos;
    }
    #endregion

    #region Public Methods

    public void Init(SpheresElements element, int count, int _index = -1) {
        m_index = _index;
        _element = element;
        _count = count;
        switch (element) {
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
            case SpheresElements.freeze:
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
    internal byte[] GetHashData() {
        byte[] bmask = BitSphere.ConvertSphere6((int)_element, _count, transform.position);
        bit_sphere = BitSphere.ConvertToSphere(bmask);
        UpdatePositionByBitSphere();
        return bmask;

    }

    internal void UpdatePositionByBitSphere() {
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

    #endregion

    
}
