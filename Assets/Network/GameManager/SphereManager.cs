using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.network {


    [Serializable]
    public class SphereManager {


        #region PrivateFields

        [SerializeField]
        GameObject spherePrefab;

        List<GameObject> _spheres = new List<GameObject>();

        #endregion

        #region Public Methods


        public SphereManager() {

        }

        public void CreateSpheres() {

        }

        protected void CreateSphere(Vector3 pos, Quaternion rotation, int elementType) {

        }
        #endregion

        public class SphereConfig {

            [Tooltip("Зона распределения сфер")]
            public Rect rect;
            [Tooltip("Максимальное количество сфер")]
            public int MaxSpheres = 100;
        }
    }
}