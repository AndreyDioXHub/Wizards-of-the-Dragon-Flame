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
    private Animator _animator;
    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private Material _lifeMTL, _fireMTL, _waterMTL, _earthMTL, _freezeMTL, _razorMTL, _darkMTL, _shieldMTL;

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

            GameObject eatEffect = Instantiate(Resources.Load<GameObject>("WorldSphereEatEffect"));
            eatEffect.transform.position = transform.position;

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
        RaycastHit hit_unused;
        Ray ray = new Ray(transform.position, Vector3.down);


        //Physics.Raycast(ray, out hit, rayDistance);
        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);

        if(hits != null && hits.Length > 0) {
            foreach (var hit in hits) {
                if(hit.collider != null) {
                    if(hit.transform.tag == ModificatorZone.Tag) {
                        ModificatorZone mz = hit.collider.gameObject.GetComponent<ModificatorZone>();
                        _count = mz.GetSphereCountByType(_element, _count);
                    }
                    if(hit.collider is TerrainCollider) {
                        Vector3 pos = transform.position;
                        //Debug.Log($"Exist collider point height = {hit.point}, current Y: {transform.position.y}");
                        pos.y = hit.point.y + adjustHeight;
                        transform.position = pos;
                    }
                }
            }
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

        _animator.SetInteger("RandomAnim", UnityEngine.Random.Range(0, 4));

        switch (element) {
            case SpheresElements.life:
                _renderer.material = _lifeMTL;
                break;
            case SpheresElements.fire:
                _renderer.material = _fireMTL;
                break;
            case SpheresElements.water:
                _renderer.material = _waterMTL;
                //Destroy(_image.gameObject);
                //Instantiate(Resources.Load<GameObject>("WaterSphereParticles"), transform);
                //_image.color = new Color(0.56f, 0.62f, 1, 1);
                break;
            case SpheresElements.earth:
                _renderer.material = _earthMTL;
                break;
            case SpheresElements.freeze:
                _renderer.material = _freezeMTL;
                break;
            case SpheresElements.razor:
                _renderer.material = _razorMTL;
                break;
            case SpheresElements.dark:
                _renderer.material = _darkMTL;
                break;
            case SpheresElements.shield:
                _renderer.material = _shieldMTL;
                break;
            default:
                break;
        }
        //Debug.Log($"Set color for sphere: {element.ToString()} as {ColorUtility.ToHtmlStringRGB(_image.color)}");
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
