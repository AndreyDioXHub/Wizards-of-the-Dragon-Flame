using System;
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

        static Dictionary<byte, int> SphereToByteComparisom = new Dictionary<byte, int>() {
            {0, 0 }, //None
            {1, 1 }, //life
            {2, 2 }, //fire
            {3, 4 }, //water
            {4, 8 }, //earth
            {5, 16 }, //freeze
            {6, 32 }, //razor
            {7, 64 }, //dark
            {8, 1024 }  //shield
        };

        static Dictionary<int, byte> ByteToSphereComparisom = new Dictionary<int, byte>() {
            {0, 0 }, //None
            {1, 1 }, //life
            {2, 2 }, //fire
            {4, 3 }, //water
            {8, 4 }, //earth
            {16, 5 }, //freeze
            {32, 6 }, //razor
            {64, 7 }, //dark
            {1024, 8 }  //shield
        };

        public static byte[] ConvertSphere(int sphereId, int sphereType, int amount, Vector3 position) {
            List<byte> total = new List<byte>();

            total.AddRange(BitConverter.GetBytes(sphereId));
            total.AddRange(BitConverter.GetBytes(sphereId));
            
            
            //total.Add()

            return total.ToArray();
        }
    }
}