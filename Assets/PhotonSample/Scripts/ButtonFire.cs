using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonFire : MonoBehaviour
{
    public UnityEvent OnPointerPush = new UnityEvent();
    public UnityEvent OnPointerUpped = new UnityEvent();
    // Start is called before the first frame update

    public void ProcessDown() {
        Debug.Log("ProcessDown");
        OnPointerPush.Invoke();
    }
    public void ProcessUp() {
        Debug.Log("ProcessUp");
        OnPointerUpped.Invoke();
    }

}
