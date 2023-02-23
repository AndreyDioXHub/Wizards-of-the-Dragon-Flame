using com.czeeep.network.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointView : MonoBehaviour
{
    public static HitPointView Instance;

    [SerializeField]
    private Image _hitPoint;
    [SerializeField]
    private Image _shieldPoint;


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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DrawHP(float hpFill, float spFill)
    {
        _hitPoint.fillAmount = hpFill;
        _shieldPoint.fillAmount = spFill;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
