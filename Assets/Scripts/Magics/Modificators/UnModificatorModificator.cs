using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class UnModificatorModificator : SphereModificator
    {
        public override void Init(string key, int power, Vector3 fromHitWhereDirection)
        {
            base.Init(key, power, fromHitWhereDirection);
            //MagicModel.Instance.ReturnAllSphereToInventory();
            Transform parent = transform.parent;
            List<GameObject> childs = new List<GameObject>();

            for (int i = 0; i < parent.childCount; i++)
            {
                if(parent.GetChild(i).gameObject != gameObject)
                {
                    childs.Add(parent.GetChild(i).gameObject);
                }
            }

            foreach (GameObject child in childs)
            {
                child.GetComponent<SphereModificator>().DestroyModificator();
            }

            childs = null;
        }

        public override int CheckCancel(string sphere, int power, out bool isCancel)
        {
            base.CheckCancel(sphere, power, out isCancel);

            int incomingPowerleft = 0;
            isCancel = true;

            _element.UpdateInfo(_info.key, _info.power, 1);
            return incomingPowerleft;
        }
    }
}
