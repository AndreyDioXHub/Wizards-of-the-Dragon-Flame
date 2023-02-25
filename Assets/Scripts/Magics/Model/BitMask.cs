using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitMask : MonoBehaviour
{
    public int id1 = 0;
    public int id2 = 0;
    public int sum;
    public string rersult;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Covert")]
    public void Covert()
    {
        sum = id1 | id2;
        string binary = Convert.ToString(sum, 2);
        rersult = $"0b_{binary}";
    }
}
