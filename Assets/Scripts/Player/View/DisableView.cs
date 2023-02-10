using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableView : MonoBehaviour
{
    public static DisableView Instance;

    [SerializeField]
    private Transform _content;
    [SerializeField]
    private GameObject _activeElementViewPrefab;

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

    public void AddNewDisable(string key, out ActiveElementView view)
    {
        view = Instantiate(_activeElementViewPrefab, _content).GetComponent<ActiveElementView>();
        view.Init(key);
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
