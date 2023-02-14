using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificatorView : MonoBehaviour
{
    public static ModificatorView Instance;

    [SerializeField]
    private Transform _content;
    [SerializeField]
    private GameObject _modificatorElementViewPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddNewModificator(string key, int power, out ModificatorElementView view)
    {
        view = Instantiate(_modificatorElementViewPrefab, _content).GetComponent<ModificatorElementView>();
        view.UpdateInfo(key, power);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
