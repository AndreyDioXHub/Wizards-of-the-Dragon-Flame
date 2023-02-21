using com.czeeep.network.player;
using com.czeeep.spell.staffmodel;
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
    [SerializeField]
    private Transform _localBackPack;

    public void ShowSphere(List<string> activeSpheres)
    {
        if(_localBackPack == null)
        {
            _localBackPack = PlayerNetwork.LocalPlayerInstance.GetComponent<PlayerNetwork>().ModelBackPackSphere;
        }

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
            goUI.GetComponent<ModificatorElementView>().UpdateInfo(activeSpheres[i], 1);
            _spheresUI.Add(goUI);

            GameObject goP = new GameObject($"{activeSpheres[i]}");
            goP.transform.SetParent(_localBackPack);
            _spheresPlayer.Add(goP);
        }


    }

    private void Update()
    {
        if (_spheresUI != null)
        {
            try
            {
                foreach (var sui in _spheresUI)
                {
                    if (sui != null)
                    {
                        sui.GetComponent<ModificatorElementView>().UpdateInfo(StaffModel.Instance.GetTickFill());
                    }
                }
            }
            catch(MissingReferenceException e)
            {

            }
        }
    }
}
