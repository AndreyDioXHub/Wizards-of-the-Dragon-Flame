using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CastModel : MonoBehaviour
{
    /* [SerializeField]
     private TextMeshProUGUI _countTextWater, _countTextLife, _countTextShield, _countTextFreze,
         _countTextRazor, _countTextDark, _countTextEarth, _countTextFire; */
    //public string element = "water";
    public List<string> ActiveSpheres { get => _activeSpheres; }

    [SerializeField]
    private SpheresView _viewInventory;
    [SerializeField]
    private ActiveSpheresView _viewActiveSpheres;
    [SerializeField]
    private float _castTime = 1;
    [SerializeField]
    private float _castTimeCur;
    [SerializeField]
    private float _castTimeReloadingCur;
    [SerializeField]
    private bool _readyToNewCast = true;

    [SerializeField]
    private SphereModificator[] _sphereModificators;
    private int _consumptionCount = 1;
    private int _activeSpheresCount = 5;
    private Dictionary<string, int> _spheres = new Dictionary<string, int>();
    private Dictionary<int, MetaSphere> _metaSpheres = new Dictionary<int, MetaSphere>();
    private Dictionary<string, List<string>> _castSequences = new Dictionary<string, List<string>>();
    //private Dictionary<string, T> _modificatorsLis = new Dictionary<string, T>();
    [SerializeField]
    private List<string> _activeSpheres = new List<string>();

    //base: water, life, shield, freze, razor, dark, earth, fire
    //meta: steam, poison, ice 

    // Start is called before the first frame update
    void Start()
    {
        FillDictonaryes();
        CollectModificators();
        _castTimeCur = _castTime;
        ShowSphere();
    }

    public void CollectModificators()
    {
        _sphereModificators = gameObject.GetComponents<SphereModificator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<string> GetActiveSpheres()
    {
        return _activeSpheres;
        //ReloadActiveSpheres();
    }

    public void ReloadActiveSpheres()
    {
        //Debug.Log("Cast");

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
        ShowSphere();
    }

    [ContextMenu("Fill Dictonaryes")]
    public void FillDictonaryes()
    {
        _metaSpheres.Clear();
        _metaSpheres = new Dictionary<int, MetaSphere>();

        _metaSpheres.Add(0b_00001000001, new MetaSphere("LifeDark", MetaSphereType.cost));
        _metaSpheres.Add(0b_00100000001, new MetaSphere(SpheresElements.water.ToString(), MetaSphereType.element));
        _metaSpheres.Add(0b_00000000110, new MetaSphere(SpheresElements.steam.ToString(), MetaSphereType.element));
        _metaSpheres.Add(0b_00000010010, new MetaSphere("FireFreze", MetaSphereType.cost));
        _metaSpheres.Add(0b_00001000010, new MetaSphere("Explosion", MetaSphereType.damage));
        _metaSpheres.Add(0b_00100000010, new MetaSphere(SpheresElements.dark.ToString(), MetaSphereType.element));
        _metaSpheres.Add(0b_01000000010, new MetaSphere(SpheresElements.water.ToString(), MetaSphereType.element));
        _metaSpheres.Add(0b_00000010100, new MetaSphere(SpheresElements.ice.ToString(), MetaSphereType.element));
        _metaSpheres.Add(0b_00000100100, new MetaSphere("Electro", MetaSphereType.damage));
        _metaSpheres.Add(0b_00001000100, new MetaSphere(SpheresElements.poison.ToString(), MetaSphereType.element));
        _metaSpheres.Add(0b_00000101000, new MetaSphere("EarthRazor", MetaSphereType.cost));
        _metaSpheres.Add(0b_00010010000, new MetaSphere(SpheresElements.water.ToString(), MetaSphereType.element));

        _castSequences.Clear();
        _castSequences = new Dictionary<string, List<string>>();

        _castSequences.Add(SpheresElements.steam.ToString(), new List<string>() {SpheresElements.water.ToString(), SpheresElements.fire.ToString()});
        _castSequences.Add(SpheresElements.ice.ToString(), new List<string>() {SpheresElements.water.ToString(), SpheresElements.freze.ToString()});
        _castSequences.Add(SpheresElements.poison.ToString(), new List<string>() {SpheresElements.water.ToString(), SpheresElements.dark.ToString()});

        /*_modificatorsLis.Clear();
        _modificatorsLis = new Dictionary<string, Type>();
        _modificatorsLis.Add(SpheresElements.fire.ToString(), typeof(FireModificator));*/
    }

    public string CalculateElement(string element, out bool result)
    {
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

        ShowSphere();
        return product;
    }

    public bool CalculateDisable(string key)
    {
        bool result = false;

        ShowSphere();
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

    public void ReturnAllSphereToInventory()
    {
        foreach(var sp in _activeSpheres)
        {
            ReturnSphereToInventory(sp);
        }

        _activeSpheres = null;
        _activeSpheres = new List<string>();

        ShowSphere();
    }

    public bool CheckDisable(string key)
    {
        bool modificatorEatSphere = false;
        int icount = _sphereModificators.Length;

        for(int i=0; i< icount; i++)
        {
            _sphereModificators[i].CheckCancel(key, out modificatorEatSphere);

            if (modificatorEatSphere)
            {
                i = icount;
            }
        }

        return modificatorEatSphere;
    }

    /*
    public void AddModificator(string key, int power)
    {
        CollectModificators();

        bool needNew = true;

        for(int i=0; i < _sphereModificators.Length - 1; i++)
        {
            if(_sphereModificators[i].Key == key)
            {
                _sphereModificators[i].AddPower(power);
                needNew = false;
                i = _sphereModificators.Length;
            }
        }

        if (needNew)
        {
           
        }
    }*/


    public void AddSpheretoActive(string key)
    {

        CollectModificators();

        

        if (_spheres.TryGetValue(key, out int value))
        {
            if (value > 0)
            {
                _spheres[key] -= _consumptionCount;

                bool modificatorEatSphere = CheckDisable(key);
                if (modificatorEatSphere)
                {
                    //show cost
                }
                else
                {
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

        ShowSphere();
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

        ShowSphere();
    }

    [ContextMenu("Show Sphere")]
    public void ShowSphere()
    {
        _viewActiveSpheres.ShowSphere(_activeSpheres);
        _viewInventory.ShowSphere(_spheres);
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
public enum CastDirection
{
    forward,
    self
}
