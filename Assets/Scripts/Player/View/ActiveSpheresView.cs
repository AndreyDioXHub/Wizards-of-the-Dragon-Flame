using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpheresView : MonoBehaviour
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private GameObject _activeElementViewPrefab;

    public void ShowSphere(List<string> activeSpheres)
    {
        List<GameObject> childs = new List<GameObject>();

        for (int i = 0; i < _content.childCount; i++)
        {
            childs.Add(_content.GetChild(i).gameObject);
        }

        foreach (GameObject child in childs)
        {
            child.transform.SetParent(null);
            Destroy(child);
        }

        childs = null;

        for(int i=0; i< activeSpheres.Count; i++)
        {
            Instantiate(_activeElementViewPrefab, _content).GetComponent<ActiveElementView>().Init(activeSpheres[i]);
        }
    }
}
