using com.czeeep.network.player;
using com.czeeep.spell.modificator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace com.czeeep.spell.magicmodel
{
    public class MagicModel : MonoBehaviour
    {
        public static MagicModel Instance;

        /* [SerializeField]
         private TextMeshProUGUI _countTextWater, _countTextLife, _countTextShield, _countTextFreze,
             _countTextRazor, _countTextDark, _countTextEarth, _countTextFire; */
        //public string element = "water";
        public List<string> ActiveSpheres { get => _activeSpheres; }

        [SerializeField]
        private PlayerNetwork _player;
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

        private int _consumptionCount = 1;
        private int _activeSpheresCount = 5;

        [SerializeField]
        private List<SphereModificator> _sphereModificators = new List<SphereModificator>();
        [SerializeField]
        private List<string> _activeSpheres = new List<string>();
        private Dictionary<string, int> _spheres = new Dictionary<string, int>();
        /*private Dictionary<int, MetaSphere> _metaSpheres = new Dictionary<int, MetaSphere>();
        private Dictionary<string, List<string>> _castSequences = new Dictionary<string, List<string>>();*/

        /*private Dictionary<string, string> _modificatorsLis = new Dictionary<string, string>()
    {
        {"life","LifeModificator" },
        {"fire","FireModificator" },
        {"water","WaterModificator" },
        {"earth","EarthModificator" },
        {"freze","FrezeModificator" },
        {"razor","RazorModificator" },
        {"dark","DarkModificator" },
        {"steam","SteamModificator" },
        {"poison","PoisonModificator" },
        {"ice","FireModificator" },
        {"shield","ShieldModificator" },
        {"stun","StunModificator" },
    };*/
        //base: water, life, shield, freze, razor, dark, earth, fire
        //meta: steam, poison, ice 

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            FillDictonaryes();
            //CollectModificators();
            _castTimeCur = _castTime;
            ShowSphere();
        }

        public void Init(PlayerNetwork player)
        {
            _player = player;
        }

        public void CollectModificators()
        {
            _sphereModificators.Clear();
            _sphereModificators = new List<SphereModificator>();
            
            var sphereModificators = _player.gameObject.GetComponentsInChildren<SphereModificator>();

            foreach(var sm in sphereModificators)
            {
                _sphereModificators.Add(sm);

            }
            //_sphereModificators = _player.gameObject.GetComponentsInChildren<SphereModificator>();

            /*_sphereModificators = gameObject.GetComponents<SphereModificator>();
            var sphereModificators = gameObject.GetComponentsInChildren<SphereModificator>();
            foreach(var sm in sphereModificators)
            {
                Debug.Log($"{sm.gameObject.name}");
            }*/
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

            foreach (string sp in _activeSpheres)
            {
                activeSpheres.Add(sp);
            }

            _activeSpheres = new List<string>();

            foreach (var sp in activeSpheres)
            {
                AddSpheretoActive(sp);
            }
            ShowSphere();
        }

        [ContextMenu("Fill Dictonaryes")]
        public void FillDictonaryes()
        {
            /*_metaSpheres.Clear();
            _metaSpheres = new Dictionary<int, MetaSphere>();

            _metaSpheres.Add(0b_00001000001, new MetaSphere("LifeDark", MetaSphereType.cost));
            _metaSpheres.Add(0b_00100000001, new MetaSphere(SpheresElements.water.ToString(), MetaSphereType.element));
            _metaSpheres.Add(0b_00000000110, new MetaSphere(SpheresElements.steam.ToString(), MetaSphereType.element));
            _metaSpheres.Add(0b_00000010010, new MetaSphere("FireFreze", MetaSphereType.cost));
            _metaSpheres.Add(0b_00001000010, new MetaSphere("Explosion", MetaSphereType.damage));
            _metaSpheres.Add(0b_00100000010, new MetaSphere(SpheresElements.dark.ToString(), MetaSphereType.element));
            _metaSpheres.Add(0b_01000000010, new MetaSphere(SpheresElements.water.ToString(), MetaSphereType.element));
            _metaSpheres.Add(0b_00000010100, new MetaSphere(SpheresElements.ice.ToString(), MetaSphereType.element));
            _metaSpheres.Add(0b_00000100100, new MetaSphere("razor", MetaSphereType.damage));//Electro
            _metaSpheres.Add(0b_00001000100, new MetaSphere(SpheresElements.poison.ToString(), MetaSphereType.element));
            _metaSpheres.Add(0b_00000101000, new MetaSphere("EarthRazor", MetaSphereType.cost));
            _metaSpheres.Add(0b_00010010000, new MetaSphere(SpheresElements.water.ToString(), MetaSphereType.element));

            _castSequences.Clear();
            _castSequences = new Dictionary<string, List<string>>();

            _castSequences.Add(SpheresElements.steam.ToString(), new List<string>() { SpheresElements.water.ToString(), SpheresElements.fire.ToString() });
            _castSequences.Add(SpheresElements.ice.ToString(), new List<string>() { SpheresElements.water.ToString(), SpheresElements.freze.ToString() });
            _castSequences.Add(SpheresElements.poison.ToString(), new List<string>() { SpheresElements.water.ToString(), SpheresElements.dark.ToString() });*/

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

                        if (MagicConst.META_SPHERES.TryGetValue((int)resultKey, out MetaSphere meta))
                        {
                            switch (meta.type)
                            {
                                case MetaSphereType.element:
                                    product = meta.name;
                                    result = true;
                                    //_activeSpheres.Remove(_activeSpheres[i]);
                                    break;
                                case MetaSphereType.cost:
                                    //do cost here
                                    break;
                                case MetaSphereType.damage:
                                    AddModificator(meta.name, 1, PlayerNetwork.LocalPlayerInstance.transform.forward);
                                    break;
                                default:
                                    break;
                            }

                            iscross = true;

                            try
                            {
                                _activeSpheres.Remove(_activeSpheres[i]);
                            }
                            catch(ArgumentOutOfRangeException e) 
                            {
                                Debug.LogWarning("CalculateElement index was removed early");
                            }

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
            if (MagicConst.MELT_CAST_SEQUENCES.TryGetValue(element, out List<string> sequence))
            {
                foreach (string sub in sequence)
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
            foreach (var sp in _activeSpheres)
            {
                ReturnSphereToInventory(sp);
            }

            _activeSpheres = null;
            _activeSpheres = new List<string>();

            ShowSphere();
        }
        public void ReturnAllSphereToInventory(string element)
        {
            List<string> returnal = new List<string>();

            foreach (var sp in _activeSpheres)
            {
                if (sp.Equals(element))
                {
                    ReturnSphereToInventory(sp);
                    returnal.Add(sp);
                    // _activeSpheres.Remove(sp);
                }
            }

            foreach (var r in returnal)
            {
                _activeSpheres.Remove(r);
            }

            /*_activeSpheres = null;
            _activeSpheres = new List<string>();*/

            ShowSphere();
        }

        [ContextMenu("Drop All Spheres Into World")]
        public void DropAllSpheresIntoWorld()
        {
            ReturnAllSphereToInventory();

            int i = 0;

            foreach (var sphere in _spheres)
            {
                if (Enum.TryParse(sphere.Key, out SpheresElements elementEnum))
                {
                    i++;
                    var go = Instantiate(Resources.Load<GameObject>("SphereWorld"));
                    go.name = $"sphere world: {sphere.Key}";
                    go.GetComponent<SphereWorld>().Init(elementEnum, sphere.Value);
                    go.transform.position = _player.transform.position + new Vector3(i, 0.2f, i);
                }
            }

            i = 0;

            _spheres.Clear();
            _spheres = new Dictionary<string, int>();

            ShowSphere();
        }

        public bool CheckEatIncomingKeytoModificator(string key, int power, out int powerLeft)
        {
            bool modificatorEatSphere = false;
            int icount = _sphereModificators.Count;
            powerLeft = 0;

            if (_sphereModificators.Count > 0)
            {
                for (int i = 0; i < icount; i++)
                {
                    //CheckCancel ��������� ��� ������� ���� ��������
                    powerLeft = _sphereModificators[i].CheckCancel(key, power, out modificatorEatSphere);

                    if (modificatorEatSphere)
                    {
                        i = icount;
                    }
                }
            }

            return modificatorEatSphere;
        }

        [ContextMenu("Add Modificator")]
        public void AddModificator()
        {
            AddModificator(MagicConst.KNOCKOUT, 1, PlayerNetwork.LocalPlayerInstance.transform.forward);
        }


        public void AddModificator(string key, int power, Vector3 direction)
        {
            Debug.Log($"AddModificator {key} ");
            CollectModificators();

            /*if (hideSpheres)
            {
                ReturnAllSphereToInventory();
            }*/

            bool modificatorEatModificator = CheckEatIncomingKeytoModificator(key, power, out int powerLeft);

            if (modificatorEatModificator)
            {
                if (powerLeft > 0)
                {
                    power = powerLeft;
                }
                else
                {
                    return;
                }
            }

            bool needNew = true;

            string len = _sphereModificators.Count.ToString();
            string eq = "";

            foreach(var sm in _sphereModificators)
            {

                Debug.Log($"AddModificator {sm.Info.key} == {key} = {sm.Info.key.Equals(key)}");
            }

            foreach(var sm in _sphereModificators)
            {
                eq += $"{sm.Info.key} {key} {sm.Info.key.Equals(key)} \n";

                if (sm.Info.key.Equals(key))
                {
                    sm.AddPower(power, direction);
                    needNew = false;
                    break;
                }
            }

            Debug.Log($"AddModificator needNew: {needNew} \n{len}\n{eq}");

            if (needNew)
            {
                try
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(MagicConst.MODIFICATOR_BY_KEY[key]), _player.ModelBackPackModificator);
                    SphereModificator modificator = go.GetComponent<SphereModificator>();
                    modificator.Init(key, power, direction);

                }
                catch (KeyNotFoundException e)
                {
                    Debug.LogWarning($"_modificatorsLis has no key: {key}");
                }
                //go.name = $"m:{key}:1";
            }
        }


        public void AddSpheretoActive(string key)
        {

            CollectModificators();



            if (_spheres.TryGetValue(key, out int value))
            {
                if (value > 0)
                {
                    _spheres[key] -= _consumptionCount;


                    bool modificatorEatSphere = CheckEatIncomingKeytoModificator(key, 1, out int powerLeft);

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

            AfterWelding();

            /*
            if (_activeSpheres.Count >= 2)
            {
                bool resultAfterAdding = false;
                string resultElementAfterAdding = CalculateElement(_activeSpheres[_activeSpheres.Count - 1], out resultAfterAdding);

                if (resultAfterAdding)
                {
                    _activeSpheres[_activeSpheres.Count - 1] = resultElementAfterAdding;
                }

            }*/
            //Debug.Log($"resultElement1 {result1} {resultElement1} ");

            ShowSphere();
        }

        public void AfterWelding()
        {
            if (_activeSpheres.Count >= 2)
            {
                bool resultAfterAdding = false;
                string resultElementAfterAdding = CalculateElement(_activeSpheres[_activeSpheres.Count - 1], out resultAfterAdding);

                if (resultAfterAdding)
                {
                    _activeSpheres[_activeSpheres.Count - 1] = resultElementAfterAdding;

                    if (_activeSpheres.Count >= 2)
                    {
                        bool resultAfterAdding2 = false;
                        string resultElementAfterAdding2 = CalculateElement(_activeSpheres[_activeSpheres.Count - 1], out resultAfterAdding2);

                        if (resultAfterAdding2)
                        {
                            _activeSpheres[_activeSpheres.Count - 1] = resultElementAfterAdding2;
                            //AfterWelding();
                        }
                    }

                    //AfterWelding();
                }
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
        none = 0, //none
        life = 1, //life
        fire = 2, //fire
        water = 4, //water
        earth = 8, //earth
        freeze = 16, //freeze
        razor = 32, //razor
        dark = 64, //dark
        shield = 128, //shield
        steam = 256, //steam
        poison = 512, //poison
        ice = 1024, //ice
        fireball1 = 2048, //fireball1
        darkball = 4096, //darkball
        speedup = 8192, //speedup
        minerazor = 16384, //minerazor
        minedark = 32768, //minedark
        force1 = 65536, //force1
        icicle1 = 131072, //icicle1
        fireball2 = 262144, //fireball2
        mineice = 524288, //mineice
        fireball3 = 1048576, //fireball3
        unmodificator = 2097152, //unmodificator
        fireball4 = 4194304, //fireball4
        force2 = 8388608, //force2
        icicle2 = 16777216, //icicle2
        icicle3 = 33554432, //icicle3
        blast = 67108864 //blast
    }
    /*
    public enum SpheresElements {
        none = 0,             //unknown
        life = 0b_1,//life
        fire = 0b_10,//fire
        water = 0b_100,//water
        earth = 0b_1000,//earth
        freeze = 0b_10000,//freeze
        razor = 0b_100000,//razor
        dark = 0b_1000000,//dark
        shield = 0b_10000000,//shield
        steam = 0b_100000000,//steam
        poison = 0b_1000000000,//poison
        ice = 0b_10000000000,//ice
        fireball1 = 0b_100000000000,
        darkball = 0b_1000000000000,
        force1 = 0b_10000000000000,
        speedup = 0b_100000000000000,
        minedark = 0b_1000000000000000,
        icicle1 = 0b_10000000000000000,
        fireball2 = 0b_100000000000000000,
        darkfireball = 0b_1000000000000000000,
        mineice = 0b_10000000000000000000,
        fireball3 = 0b_100000000000000000000,
        unmodificator = 0b_1000000000000000000000,
        fireball4 = 0b_10000000000000000000000,
        force2 = 0b_100000000000000000000000,
        icicle2 = 0b_1000000000000000000000000,
        icicle3 = 0b_10000000000000000000000000,
        blast = 0b_100000000000000000000000000,
    }*/

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
}


