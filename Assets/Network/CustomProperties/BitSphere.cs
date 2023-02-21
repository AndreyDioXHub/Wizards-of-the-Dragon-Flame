using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.network {
    public class BitSphere {

        /// <summary>
        /// BIT MASK : ID (2x byte) | TYPE (byte) | AMOUNT (byte) | XPOS (2x byte) | YPOS (2x byte)
        /// TOTAL: 8 bytes length
        /// </summary>

        [SerializeField]
        public ushort xposition;
        [SerializeField]
        public ushort yposition;
        [SerializeField]
        public ushort sphereID;
        [SerializeField]
        public byte sphereType;
        [SerializeField]
        public byte amount;


    }
}