using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimation : MonoBehaviour
{
    [SerializeField]
    private List<Texture> _spriteKeys = new List<Texture>();
    [SerializeField]
    private float _time = 0.07f;
    [SerializeField]
    private float _timeCur;
    [SerializeField]
    private int _index = 0;
    [SerializeField]
    private List<Material> _particleMTL = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _timeCur += Time.deltaTime;
        if(_timeCur> _time)
        {
            foreach(var t in _particleMTL)
            {
                t.SetTexture("_BaseMap", _spriteKeys[_index]);
            }

            _index++;

            if (_index > _spriteKeys.Count - 1)
            {
                _index = 0;
            }
            _timeCur = 0;
        }
    }

}
