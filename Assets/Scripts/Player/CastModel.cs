using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CastModel : MonoBehaviour
{
    public SpheresElements element1;
    public SpheresElements element2;
    public string e;

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
    //base: water, life, shield, freze, razor, dark, earth, fire
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
        //Debug.Log($"ShowSphere {(element1 | element2)} {(MetaSpheres)(element1 | element2)}");

        Debug.Log($"ShowSphere {(int)SpheresElements.lif}");
        Debug.Log($"ShowSphere {(int)SpheresElements.fir}");
        Debug.Log($"ShowSphere {(int)SpheresElements.wat}");
        Debug.Log($"ShowSphere {(int)SpheresElements.ear}");
        Debug.Log($"ShowSphere {(int)SpheresElements.fre}");
        Debug.Log($"ShowSphere {(int)SpheresElements.raz}");
        Debug.Log($"ShowSphere {(int)SpheresElements.dar}");
        Debug.Log($"ShowSphere {(int)SpheresElements.ste}");
        Debug.Log($"ShowSphere {(int)SpheresElements.poi}");
        Debug.Log($"ShowSphere {(int)SpheresElements.ice}");
        Debug.Log($"ShowSphere {(int)SpheresElements.shi}");

        Debug.Log($"ShowSphere ____________________________");


        Debug.Log($"ShowSphere {SpheresElements.lif | SpheresElements.dar} {0b_00001000001}");
        Debug.Log($"ShowSphere {SpheresElements.lif | SpheresElements.poi} {0b_00100000001}");

        Debug.Log($"ShowSphere {SpheresElements.fir | SpheresElements.wat} {0b_00000000110}");
        Debug.Log($"ShowSphere {SpheresElements.fir | SpheresElements.fre} {0b_00000010010}");
        Debug.Log($"ShowSphere {SpheresElements.fir | SpheresElements.poi} {0b_00100000010}");
        Debug.Log($"ShowSphere {SpheresElements.fir | SpheresElements.ice} {0b_01000000010}");

        Debug.Log($"ShowSphere {SpheresElements.wat | SpheresElements.fre} {0b_00000010100}");
        Debug.Log($"ShowSphere {SpheresElements.wat | SpheresElements.raz} {0b_00000100100}");
        Debug.Log($"ShowSphere {SpheresElements.wat | SpheresElements.dar} {0b_00001000100}");

        Debug.Log($"ShowSphere {SpheresElements.ear | SpheresElements.raz} {0b_00000101000}");

        Debug.Log($"ShowSphere {SpheresElements.fre | SpheresElements.ste} {0b_00010010000}");


        /*foreach(var element in _spheres)
        {
            Debug.Log($"ShowSphere {element.Key} {element.Value}");
        }*/
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

public enum SpheresElements
{
    lif = 0b_00000000001,//life
    fir = 0b_00000000010,//fire
    wat = 0b_00000000100,//water
    ear = 0b_00000001000,//earth
    fre = 0b_00000010000,//freze
    raz = 0b_00000100000,//razor
    dar = 0b_00001000000,//dark
    ste = 0b_00010000000,//steam
    poi = 0b_00100000000,//poison
    ice = 0b_01000000000,//ice
    shi = 0b_10000000000//shield
}

public enum MetaSpheres
{
    cos1 = 0b_00001000001,//cost
    wat1 = 0b_00100000001,//water
    stea = 0b_00000000110,//steam
    cos2 = 0b_00000010010,//cost
    dark = 0b_00100000010,//dark
    wat2 = 0b_01000000010,//water
    ice1 = 0b_00000010100,//ice
    dael = 0b_00000100100,//damage_electro
    pois = 0b_00001000100,//poison
    cos3 = 0b_00000101000,//cost
    wat3 = 0b_00010010000,//water
}
