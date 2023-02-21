using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.network {
    public class BitSphere {

        static readonly int MAGIC_SCALE = 20;

        /// <summary>
        /// BIT MASK : ID (2x byte) | TYPE (byte) | AMOUNT (byte) | XPOS (2x byte) | YPOS (2x byte)
        /// TOTAL: 8 bytes length
        /// </summary>

        [SerializeField]
        public ushort xposition;
        [SerializeField]
        public ushort zposition;
        [SerializeField]
        public ushort sphereID;
        [SerializeField]
        public byte sphereType;
        [SerializeField]
        public byte amount;

        static Dictionary<byte, int> ByteToSphereComparison = new Dictionary<byte, int>() {
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

        static Dictionary<int, byte> SphereToByteComparison = new Dictionary<int, byte>() {
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

        #region CONSTRUCTOR

        public BitSphere() {
        }

        public BitSphere(int sphereId, int sphereType, int amount, Vector3 position) {
            this.sphereID = (ushort)sphereId;
            this.sphereType = 0;
            SphereToByteComparison.TryGetValue(sphereType, out this.sphereType);
            this.amount = (byte)amount;
            xposition = (ushort)(position.x * MAGIC_SCALE);
            zposition = (ushort)(position.z * MAGIC_SCALE);
        }

        public BitSphere(byte[] message) {
            byte[] partial = new byte[2];
            if (message.Length > 6) {    //With ID
                Array.Copy(message, 0, partial, 0, 2);
                sphereID = BitConverter.ToUInt16(partial);
                byte[] _msg = new byte[6];
                Array.Copy(message, 2, _msg, 0, 6);
                message = _msg;
            }
            sphereType = message[0];
            amount = message[1];
            Array.Copy(message, 2, partial, 0, 2);
            xposition = BitConverter.ToUInt16(partial);
            Array.Copy(message, 4, partial, 0, 2);
            zposition = BitConverter.ToUInt16(partial);
        }

        #endregion

        #region PUBLIC METHODS

        public Vector3 GetPosition() {
            Vector3 pos = Vector3.zero;
            pos.x = xposition / MAGIC_SCALE;
            pos.z = zposition / MAGIC_SCALE;
            return pos;
        }

        public int GetIntSphereType() {
            int _type;
            if(!ByteToSphereComparison.TryGetValue(sphereType, out _type)) {
                _type = 0;
            }
            return _type;
        }

        #endregion

        #region TO BYTE CONVERTER

        public static byte[] ConvertSphere8(int sphereId, int sphereType, int amount, Vector3 position) {
            List<byte> total = new List<byte>();
            //Sphere ID
            total.AddRange(BitConverter.GetBytes(sphereId));                    //2     ID
            //Sphere Type to byte type
            byte spid;
            if(SphereToByteComparison.TryGetValue(sphereType, out spid)) {      //1     TYPE
                total.Add(spid);
            } else {
                total.Add(0);
            }
            //Amount in sphere
            total.AddRange(BitConverter.GetBytes((byte)amount));                //1     AMOUNT

            //Position
            total.AddRange(GetPositionToByte(position, 0));                     //2     X
            total.AddRange(GetPositionToByte(position, 2));                     //2     Y

            return total.ToArray();
        }
        /// <summary>
        /// Convert sphere object to 6-byte array for netword data transfer
        /// Struct: TYPE (1 byte) | AMOUNT (1 byte) | X POS (2 byte) | Y POS (2 byte)
        /// </summary>
        /// <param name="sphereType">enum SpheresElements in Int</param>
        /// <param name="amount">Amount for current sphere</param>
        /// <param name="position">2d position (Y axis ignored)</param>
        /// <returns>byte[6]</returns>
        public static byte[] ConvertSphere6(int sphereType, int amount, Vector3 position) {
            List<byte> total = new List<byte>();

            //Sphere Type to byte type
            byte spid;
            if (SphereToByteComparison.TryGetValue(sphereType, out spid)) {      //1     TYPE
                total.Add(spid);
            } else {
                total.Add(0);
            }

            //Amount in sphere
            total.AddRange(BitConverter.GetBytes((byte)amount));                //1     AMOUNT

            //Position
            total.AddRange(GetPositionToByte(position, 0));                     //2     X
            total.AddRange(GetPositionToByte(position, 2));                     //2     Y

            return total.ToArray();
        }

        #endregion

        #region FROM BYTE TO BITSPHERE

        public BitSphere ConvertToSphere(byte[] message) {
            BitSphere _element = new BitSphere(message);
            return _element;
        }

        #endregion

        #region PRIVATE STATIC
        private static byte[] GetPositionToByte(Vector3 position, int v) {
            ushort scaleint = (ushort)(position[v] * MAGIC_SCALE);
            return BitConverter.GetBytes(scaleint);
        }
        #endregion
    }
}