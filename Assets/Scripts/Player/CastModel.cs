using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastModel : MonoBehaviour
{
    //public string element = "water";
    [SerializeField]
    private float _castTime = 1;
    [SerializeField]
    private float _castTimeCur;

    private int _consumptionCount = 1;
    private int _activeSpheresCount = 5;
    private Dictionary<string, int> _spheres = new Dictionary<string, int>();
    [SerializeField]
    private List<string> _activeSpheres = new List<string>();
    //base: water, life, shield, freze, razor, magic, earth, fire
    //meta: steam, poison, ice 

    // Start is called before the first frame update
    void Start()
    {
        _castTimeCur = _castTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastStop()
    {
        _castTimeCur = _castTime;
    }

    public void CastUpdate()
    {
        if (_activeSpheres.Count > 0)
        {
            _castTimeCur += Time.deltaTime;

            if (_castTimeCur >= _castTime)
            {
                _castTimeCur = 0;
                ReloadActiveSpheres();
            }
        }
        else
        {
            CastStop();
        }

        //Debug.Log("Cast");

        //int icount = _activeSpheres.Count;
    }

    public void ReloadActiveSpheres()
    {
        Debug.Log("Cast");

        List<string> activeSpheres = new List<string>();

        foreach(string sp in _activeSpheres)
        {
            activeSpheres.Add(sp);
        }

        _activeSpheres = new List<string>();

        foreach(var sp in activeSpheres)
        {
            AddSpheretoActive(sp);
        }
    }

    public string CalculateElement(string element)
    {
        string product = element;

        int icount = _activeSpheres.Count;

        return product;
    }


    public void AddSpheretoActive(string key)
    {
        if (_spheres.TryGetValue(key, out int value))
        {
            if (value > 0)
            {
                _spheres[key] -= _consumptionCount;

                if (_activeSpheres.Count < _activeSpheresCount)
                {
                    _activeSpheres.Add(CalculateElement(key));
                }
                else
                {
                    _activeSpheres.RemoveAt(0);
                    _activeSpheres.Add(CalculateElement(key));
                }
            }
            else
            {
                _spheres.Remove(key);
            }
        }
        else
        {
            Debug.Log($"GetSphereByKey: {key}: empty");
        }
    }

    [ContextMenu("Show Sphere")]
    public void ShowSphere()
    {
        foreach(var element in _spheres)
        {
            Debug.Log($"ShowSphere {element.Key} {element.Value}");
        }
    }

    public void AddSphere(string key, int value)
    {
        if (_spheres.TryGetValue(key, out int valuecur))
        {
            _spheres[key] += value;
        }
        else
        {
            _spheres.Add(key, value);
        }
    }

}
