using com.czeeep.spell.magicmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace com.czeeep.spell.biom
{
    public class MineZone : ModificatorZone
    {
        public override Vector3 DirToPlayer()
        {
            Vector3 dir = _player.transform.position - _zoneCenter.position;

            //dir.y += 02f;

            dir = dir.normalized;

            Debug.DrawLine(_player.transform.position, _player.transform.position + dir, Color.cyan, 5f);
            //EditorApplication.isPaused = true;

            
            return dir;
        }
    }

}
