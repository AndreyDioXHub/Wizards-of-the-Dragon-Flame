using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModificatorElementView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _count;

    public void UpdateInfo(string name, int power)
    {
        _name.text = name;
        _count.text = $"x{power}";
    }
}
