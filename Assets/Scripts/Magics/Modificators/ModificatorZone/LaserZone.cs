using com.czeeep.spell.magicmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.czeeep.spell.biom
{
    public class LaserZone : ModificatorZone
    {
        [SerializeField]
        private List<Transform> _partAnchors = new List<Transform>();
        [SerializeField]
        private float _laserLenght = 20f;
        [SerializeField]
        private float _laserLenghtPisechkaPercent = 1.01f;
        [SerializeField]
        private LayerMask _otherThinkMask;
        [SerializeField]
        private LayerMask _laserMask;
        [SerializeField]
        private bool _checkCros = true;


        public override void Start()
        {
            base.Start();

            /*foreach (var part in _partAnchors)
            {
                part.gameObject.layer = LayerMask.NameToLayer(MagicConst.MY_LAZER_MASK); 
                for(int i=0; i< part.childCount; i++)
                {
                    part.GetChild(i).gameObject.layer = LayerMask.NameToLayer(MagicConst.MY_LAZER_MASK); 
                }
            }*/
        }


        public override void Update()
        {
            base.Update();

            foreach (var part in _partAnchors)
            {
                float partScale = _laserLenght * _laserLenghtPisechkaPercent;

                if (Physics.Raycast(part.position, part.forward, out RaycastHit hit, _laserLenght, _otherThinkMask))
                {

                    Debug.DrawLine(part.position, hit.point, Color.red);
                    partScale = (Vector3.Distance(part.position, hit.point) / _laserLenght);
                    partScale = partScale * _laserLenght * _laserLenghtPisechkaPercent;

                    part.localScale = new Vector3(1, 1, partScale);

                }
                else
                {
                    Debug.DrawLine(part.position, part.position + part.forward * _laserLenght * _laserLenghtPisechkaPercent, Color.red);
                    part.localScale = new Vector3(1, 1, partScale);
                }
            }

            if (_checkCros)
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit laserHit, _laserLenght, _laserMask))
                {

                    Debug.DrawLine(transform.position, laserHit.point, Color.green);
                    var allPartScale = (Vector3.Distance(transform.position, laserHit.point) / _laserLenght);
                    allPartScale = allPartScale * _laserLenght * _laserLenghtPisechkaPercent;

                    foreach (var part in _partAnchors)
                    {
                        part.localScale = new Vector3(1, 1, allPartScale);
                    }

                }
                else
                {
                    Debug.DrawLine(transform.position, transform.position + transform.forward * _laserLenght * _laserLenghtPisechkaPercent, Color.green);
                }
            }
            

        }

    }

}
