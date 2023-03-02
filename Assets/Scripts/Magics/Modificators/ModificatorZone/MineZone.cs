using com.czeeep.spell.magicmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.czeeep.spell.biom
{
    public class MineZone : ModificatorZone
    {
        public override Vector3 DirToPlayer()
        {
            Vector3 dir = _player.transform.position - _zoneCenter.position;

            dir = dir.normalized;
            
            return dir;
        }
    }

}
