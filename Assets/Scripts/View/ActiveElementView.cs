using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveElementView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;

    public void Init(string name)
    {
        _name.text = name;
    }
}
