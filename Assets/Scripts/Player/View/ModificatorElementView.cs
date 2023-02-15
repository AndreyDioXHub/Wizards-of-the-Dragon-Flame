using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModificatorElementView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _count;
    [SerializeField]
    private Image _bar;

    public void UpdateInfo(string name, int power, float fill)
    {
        _name.text = name;
        _count.text = $"x{power}";
        _bar.fillAmount = fill;
    }
}
