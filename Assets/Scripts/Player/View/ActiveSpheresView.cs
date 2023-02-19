using com.czeeep.network.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpheresView : MonoBehaviour
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private GameObject _activeElementViewPrefab;
    [SerializeField]
    private List<GameObject> _spheresUI;
    [SerializeField]
    private List<GameObject> _spheresPlayer;

    public void ShowSphere(List<string> activeSpheres)
    {
        /*List<GameObject> childs = new List<GameObject>();

        for (int i = 0; i < _content.childCount; i++)
        {
            childs.Add(_content.GetChild(i).gameObject);
        }

        foreach (GameObject child in childs)
        {
            child.transform.SetParent(null);
            Destroy(child);
        }

        childs = null;*/

        foreach (GameObject child in _spheresUI)
        {
            //child.transform.SetParent(null);
            Destroy(child);
        }

        foreach (GameObject child in _spheresPlayer)
        {
            //child.transform.SetParent(null);
            Destroy(child);
        }

        for (int i=0; i< activeSpheres.Count; i++)
        {
            GameObject goUI = Instantiate(_activeElementViewPrefab, _content);//.GetComponent<ActiveElementView>().Init(activeSpheres[i]);
            goUI.GetComponent<ActiveElementView>().Init(activeSpheres[i]);
            _spheresUI.Add(goUI);

            GameObject goP = new GameObject($"e{activeSpheres[i]}");
            goP.transform.SetParent(PlayerNetwork.LocalPlayerInstance.transform);
            _spheresPlayer.Add(goP);
        }


    }
}
