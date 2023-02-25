using com.czeeep.spell.magicmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SpheresView : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _countTextWater, _countTextLife, _countTextShield, _countTextFreze,
        _countTextRazor, _countTextDark, _countTextEarth, _countTextFire;


    [ContextMenu("Show Sphere")]
    public void ShowSphere(Dictionary<string, int> spheres)
    {

        int value = 0;

        if (spheres.TryGetValue(SpheresElements.water.ToString(), out value))
        {
            _countTextWater.text = $"x{value}";
        }
        else
        {
            _countTextWater.text = $"x0";
        }
        if (spheres.TryGetValue(SpheresElements.life.ToString(), out value))
        {
            _countTextLife.text = $"x{value}";
        }
        else
        {
            _countTextLife.text = $"x0";
        }
        if (spheres.TryGetValue(SpheresElements.shield.ToString(), out value))
        {
            _countTextShield.text = $"x{value}";
        }
        else
        {
            _countTextShield.text = $"x0";
        }
        if (spheres.TryGetValue(SpheresElements.freeze.ToString(), out value))
        {
            _countTextFreze.text = $"x{value}";
        }
        else
        {
            _countTextFreze.text = $"x0";
        }
        if (spheres.TryGetValue(SpheresElements.razor.ToString(), out value))
        {
            _countTextRazor.text = $"x{value}";
        }
        else
        {
            _countTextRazor.text = $"x0";
        }
        if (spheres.TryGetValue(SpheresElements.dark.ToString(), out value))
        {
            _countTextDark.text = $"x{value}";
        }
        else
        {
            _countTextDark.text = $"x0";
        }
        if (spheres.TryGetValue(SpheresElements.earth.ToString(), out value))
        {
            _countTextEarth.text = $"x{value}";
        }
        else
        {
            _countTextEarth.text = $"x0";
        }
        if (spheres.TryGetValue(SpheresElements.fire.ToString(), out value))
        {
            _countTextFire.text = $"x{value}";
        }
        else
        {
            _countTextFire.text = $"x0";
        }
    }

}
