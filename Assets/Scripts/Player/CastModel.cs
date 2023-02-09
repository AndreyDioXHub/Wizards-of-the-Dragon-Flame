using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CastModel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _countTextWater, _countTextLife, _countTextShield, _countTextFreze,
        _countTextRazor, _countTextDark, _countTextEarth, _countTextFire; 
    //public string element = "water";
    [SerializeField]
    private float _castTime = 1;
    [SerializeField]
    private float _castTimeCur;

    private int _consumptionCount = 1;
    private int _activeSpheresCount = 5;
    private Dictionary<string, int> _spheres = new Dictionary<string, int>();
    private Dictionary<int, MetaSphere> _metaSpheres = new Dictionary<int, MetaSphere>();
    private Dictionary<string, List<string>> _castSequences = new Dictionary<string, List<string>>();
    [SerializeField]
    private List<string> _activeSpheres = new List<string>();

    //base: water, life, shield, freze, razor, dark, earth, fire
    //meta: steam, poison, ice 

    // Start is called before the first frame update
    void Start()
    {
        FillDictonaryes();
        ShowSphere();
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
        ShowSphere();
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

    [ContextMenu("Fill Dictonaryes")]
    public void FillDictonaryes()
    {
        _metaSpheres.Clear();
        _metaSpheres = new Dictionary<int, MetaSphere>();

        _metaSpheres.Add(0b_00001000001, new MetaSphere("LifeDark", MetaSphereType.cost));
        _metaSpheres.Add(0b_00100000001, new MetaSphere("water", MetaSphereType.element));
        _metaSpheres.Add(0b_00000000110, new MetaSphere("steam", MetaSphereType.element));
        _metaSpheres.Add(0b_00000010010, new MetaSphere("FireFreze", MetaSphereType.cost));
        _metaSpheres.Add(0b_00001000010, new MetaSphere("explosion", MetaSphereType.damage));
        _metaSpheres.Add(0b_00100000010, new MetaSphere("dark", MetaSphereType.element));
        _metaSpheres.Add(0b_01000000010, new MetaSphere("water", MetaSphereType.element));
        _metaSpheres.Add(0b_00000010100, new MetaSphere("ice", MetaSphereType.element));
        _metaSpheres.Add(0b_00000100100, new MetaSphere("Electro", MetaSphereType.damage));
        _metaSpheres.Add(0b_00001000100, new MetaSphere("poison", MetaSphereType.element));
        _metaSpheres.Add(0b_00000101000, new MetaSphere("EarthRazor", MetaSphereType.cost));
        _metaSpheres.Add(0b_00010010000, new MetaSphere("water", MetaSphereType.element));

        _castSequences.Clear();
        _castSequences = new Dictionary<string, List<string>>();

        _castSequences.Add(SpheresElements.steam.ToString(), new List<string>() {SpheresElements.water.ToString(), SpheresElements.fire.ToString()});
        _castSequences.Add(SpheresElements.ice.ToString(), new List<string>() {SpheresElements.water.ToString(), SpheresElements.freze.ToString()});
        _castSequences.Add(SpheresElements.poison.ToString(), new List<string>() {SpheresElements.water.ToString(), SpheresElements.dark.ToString()});
    }

    public string CalculateElement(string element, out bool result)
    {
        ShowSphere();
        result = false;
        string product = element;

        int icount = _activeSpheres.Count;

        bool iscross = false;

        if (Enum.TryParse(element, out SpheresElements elementEnum))
        {
            for (int i = 0; i < icount; i++)
            {
                if (Enum.TryParse(_activeSpheres[i], out SpheresElements elementActiveEnum))
                {
                    var resultKey = (elementActiveEnum | elementEnum);

                    if (_metaSpheres.TryGetValue((int)resultKey, out MetaSphere meta))
                    {
                        switch (meta.type)
                        {
                            case MetaSphereType.element:
                                product = meta.name;
                                result = true;
                                break;
                            case MetaSphereType.cost:
                                //do cost here
                                break;
                            case MetaSphereType.damage:
                                //do damage here
                                break;
                            default:
                                break;
                        }

                        iscross = true;
                        _activeSpheres.Remove(_activeSpheres[i]);
                        i = icount;
                    }
                }
            }
        }

        if (!iscross)
        {
            result = true;
            product = element;
        }

        return product;
    }

    public bool CalculateDisable(string key)
    {
        ShowSphere();
        bool result = false;

        return result;
    } 

    public void ReturnSphereToInventory(string element)
    {
        if (_castSequences.TryGetValue(element, out List<string> sequence))
        {
            foreach(string sub in sequence)
            {
                AddSphere(sub, 1);
            }
        }
        else
        {
            AddSphere(element, 1);
        }
    }

    public void AddSpheretoActive(string key)
    {
        ShowSphere();
        if (_spheres.TryGetValue(key, out int value))
        {
            if (value > 0)
            {
                _spheres[key] -= _consumptionCount;

                bool result = false;
                string resultElement = CalculateElement(key, out result);

                if (result)
                {
                    if (_activeSpheres.Count < _activeSpheresCount)
                    {
                        _activeSpheres.Add(resultElement);
                    }
                    else
                    {
                        ReturnSphereToInventory(_activeSpheres[0]);
                        _activeSpheres.RemoveAt(0);
                        _activeSpheres.Add(resultElement);
                    }
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
    public void AddSphere(string key, int value)
    {
        ShowSphere();
        if (_spheres.TryGetValue(key, out int valuecur))
        {
            _spheres[key] += value;
        }
        else
        {
            _spheres.Add(key, value);
        }
    }

    [ContextMenu("Show Sphere")]
    public void ShowSphere()
    {

        int value = 0;

        if (_spheres.TryGetValue(SpheresElements.water.ToString(), out value))
        {
            _countTextWater.text = $"x{value}";
        }
        else
        {
            _countTextWater.text = $"x0";
        }
        if (_spheres.TryGetValue(SpheresElements.life.ToString(), out value))
        {
            _countTextLife.text = $"x{value}";
        }
        else
        {
            _countTextLife.text = $"x0";
        }
        if (_spheres.TryGetValue(SpheresElements.shield.ToString(), out value))
        {
            _countTextShield.text = $"x{value}";
        }
        else
        {
            _countTextShield.text = $"x0";
        }
        if (_spheres.TryGetValue(SpheresElements.freze.ToString(), out value))
        {
            _countTextFreze.text = $"x{value}";
        }
        else
        {
            _countTextFreze.text = $"x0";
        }
        if (_spheres.TryGetValue(SpheresElements.razor.ToString(), out value))
        {
            _countTextRazor.text = $"x{value}";
        }
        else
        {
            _countTextRazor.text = $"x0";
        }
        if (_spheres.TryGetValue(SpheresElements.dark.ToString(), out value))
        {
            _countTextDark.text = $"x{value}";
        }
        else
        {
            _countTextDark.text = $"x0";
        }
        if (_spheres.TryGetValue(SpheresElements.earth.ToString(), out value))
        {
            _countTextEarth.text = $"x{value}";
        }
        else
        {
            _countTextEarth.text = $"x0";
        }
        if (_spheres.TryGetValue(SpheresElements.fire.ToString(), out value))
        {
            _countTextFire.text = $"x{value}";
        }
        else
        {
            _countTextFire.text = $"x0";
        }
        
        /*_countTextWater, _countTextLife, _countTextShield, _countTextFreze,
        _countTextRazor, _countTextMagic, _countTextEarth, _countTextFire;*/

        //Debug.Log($"ShowSphere {(element1 | element2)} {(MetaSpheres)(element1 | element2)}");

        /*
        _metaSpheres.Clear();
        _metaSpheres = new Dictionary<int, string>();

        _metaSpheres.Add(0b_00001000001, "costLifeDark");
        _metaSpheres.Add(0b_00100000001, "water");
        _metaSpheres.Add(0b_00000000110, "steam");
        _metaSpheres.Add(0b_00000010010, "costFireFreze");
        _metaSpheres.Add(0b_00100000010, "dark");
        _metaSpheres.Add(0b_01000000010, "water");
        _metaSpheres.Add(0b_00000010100, "ice");
        _metaSpheres.Add(0b_00000100100, "damageElectro");
        _metaSpheres.Add(0b_00001000100, "poison");
        _metaSpheres.Add(0b_00000101000, "costEarthRazor");
        _metaSpheres.Add(0b_00010010000, "water");


        var resultKey = (element1 | element2);

        if (_metaSpheres.TryGetValue((int)resultKey, out string value))
        {
            Debug.Log($"Meta Sphere is {value}");
        }
        else
        {
            Debug.Log($"Meta Sphere is empty");
        }*/

        /*Debug.Log($"ShowSphere {(int)SpheresElements.lif}");
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

        Debug.Log($"ShowSphere {SpheresElements.fre | SpheresElements.ste} {0b_00010010000}");*/


        /*foreach(var element in _spheres)
        {
            Debug.Log($"ShowSphere {element.Key} {element.Value}");
        }*/
    }


}

public enum SpheresElements
{
    life = 0b_00000000001,//life
    fire = 0b_00000000010,//fire
    water = 0b_00000000100,//water
    earth = 0b_00000001000,//earth
    freze = 0b_00000010000,//freze
    razor = 0b_00000100000,//razor
    dark = 0b_00001000000,//dark
    steam = 0b_00010000000,//steam
    poison = 0b_00100000000,//poison
    ice = 0b_01000000000,//ice
    shield = 0b_10000000000//shield
}

[Serializable]
public class MetaSphere
{
    public string name;
    public MetaSphereType type;
    public MetaSphere(string name, MetaSphereType type)
    {
        this.name = name;
        this.type = type;
    }
}

public enum MetaSphereType
{
    element,
    cost,
    damage
}

/*
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
}*/
