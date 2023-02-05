using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInventory : MonoBehaviour
{
    //public string element = "water";

    private int _activeSpheresCount = 5;
    private Dictionary<string, int> _spheres = new Dictionary<string, int>();
    [SerializeField]
    private List<string> _activeSpheres = new List<string>();
    //base: water, life, shield, freze, razor, magic, earth, fire
    //meta: steam, poison, ice 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cast()
    {
        Debug.Log("Cast");
        int icount = _activeSpheres.Count;

        List<string> removeElemrnts = new List<string>(); 

        for (int i = 0; i < icount - 1; i++)
        {
            switch (_activeSpheres[i])
            {
                case "water":
                    if(RemoveElement("water", 1))
                    {
                        removeElemrnts.Add("water");
                    }
                    break;
                case "life":
                    if (RemoveElement("life", 1))
                    {
                        removeElemrnts.Add("life");
                    }
                    break;
                case "shield":
                    if (RemoveElement("shield", 1))
                    {
                        removeElemrnts.Add("shield");
                    }
                    break;
                case "freze":
                    if (RemoveElement("freze", 1))
                    {
                        removeElemrnts.Add("freze");
                    }
                    break;
                case "razor":
                    if (RemoveElement("razor", 1))
                    {
                        removeElemrnts.Add("razor");
                    }
                    break;
                case "magic":
                    if (RemoveElement("magic", 1))
                    {
                        removeElemrnts.Add("magic");
                    }
                    break;
                case "earth":
                    if (RemoveElement("earth", 1))
                    {
                        removeElemrnts.Add("earth");
                    }
                    break;
                case "fire":
                    if (RemoveElement("fire", 1))
                    {
                        removeElemrnts.Add("fire");
                    }
                    break;
                case "steam":
                    if (RemoveElement("water", 1) | RemoveElement("fire", 1))
                    {
                        removeElemrnts.Add("steam");
                    }
                    break;
                case "poison":
                    if (RemoveElement("water", 1) | RemoveElement("magic", 1))
                    {
                        removeElemrnts.Add("poison");
                    }
                    break;
                case "ice":
                    if (RemoveElement("water", 1) | RemoveElement("freze", 1))
                    {
                        removeElemrnts.Add("ice");
                    }
                    break;
                default:
                    break;
            }
        }

        foreach(var el in removeElemrnts)
        {
            _activeSpheres.Remove(el);
        }
    }

    public void CalculateElement(string element)
    {
        string product = element;

        int icount = _activeSpheres.Count;


        var setupedellements = _activeSpheres.FindAll(e => e == element);

        if(setupedellements.Count < _spheres[element])
        {
            for (int i = 0; i < icount - 1; i++)
            {
                switch (_activeSpheres[i])
                {
                    case "water":
                        switch (element)
                        {
                            case "fire":
                                _activeSpheres.Remove("water");
                                i = icount;
                                product = "steam";
                                break;
                            case "freze":
                                _activeSpheres.Remove("water");
                                i = icount;
                                product = "ice";
                                break;
                            case "magic":
                                _activeSpheres.Remove("water");
                                i = icount;
                                product = "poison";
                                break;
                            default:
                                product = element;
                                break;
                        }
                        break;
                    case "life":
                        break;
                    case "shield":
                        break;
                    case "freze":
                        switch (element)
                        {
                            case "water":
                                _activeSpheres.Remove("freze");
                                i = icount;
                                product = "ice";
                                break;
                            default:
                                product = element;
                                break;
                        }
                        break;
                    case "razor":
                        break;
                    case "magic":
                        switch (element)
                        {
                            case "water":
                                _activeSpheres.Remove("magic");
                                i = icount;
                                product = "poison";
                                break;
                            default:
                                product = element;
                                break;
                        }
                        break;
                    case "earth":
                        break;
                    case "fire":
                        switch (element)
                        {
                            case "water":
                                _activeSpheres.Remove("fire");
                                i = icount;
                                product = "steam";
                                break;
                            default:
                                product = element;
                                break;
                        }
                        break;
                    default:
                        break;
                }

            }

            if (_activeSpheres.Count < _activeSpheresCount)
            {
                _activeSpheres.Add(product);
            }
            else
            {
                _activeSpheres.RemoveAt(0);
                _activeSpheres.Add(product);
            }
        }

        
    }

    public bool RemoveElement(string key, int value)
    {
        bool empty = false;

        if (_spheres.TryGetValue(key, out int valuecur))
        {
            if(valuecur == 0)
            {
                _spheres.Remove(key);
                empty = true;
            }
            else
            {
                _spheres[key] -= value;
                if(_spheres[key] == 0)
                {
                    _spheres.Remove(key);
                    empty = true;
                }
                else
                {
                    empty = false;
                }
            }
        }
        else
        {
            empty = true;
        }

        return empty;
    }

    public void SetUpSphereByKey(string key)
    {
        if (_spheres.TryGetValue(key, out int value))
        {
            //Debug.Log($"GetSphereByKey: {key}: {value} ");

            CalculateElement(key);
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
