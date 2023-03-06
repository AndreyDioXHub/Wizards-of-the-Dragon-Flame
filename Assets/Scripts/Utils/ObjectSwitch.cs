using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace com.cyraxchel.utils {
    public class ObjectSwitch : MonoBehaviour {

        public UnityEvent<int> OnObjectSwitched;

        public GameObject[] ObjectsPool;

        public int FirstEnabled = -1;

        public int CurrentObjectIndex = -1;

        [Tooltip("Reset current state to start after reenable component")]
        public bool ResetOnEnable = false;
        bool inited = false;

        void Awake() {
            if (FirstEnabled > -1) {
                EnableObject(FirstEnabled);
            }
            inited = true;
        }
        /// <summary>
        /// Переключение на новый объект
        /// </summary>
        /// <param name="idEnabled"></param>
        public void EnableObject(int idEnabled) {
            for (int i = 0; i < ObjectsPool.Length; i++) {
                ObjectsPool[i].SetActive(false);
            }
            if (idEnabled < ObjectsPool.Length && idEnabled > -1) {
                ObjectsPool[idEnabled].SetActive(true);
            }
            CurrentObjectIndex = idEnabled;
            OnObjectSwitched.Invoke(CurrentObjectIndex);

        }

        private void OnEnable() {
            if (ResetOnEnable && inited && (FirstEnabled > -1)) {
                EnableObject(FirstEnabled);
            }
        }
        /// <summary>
        /// Загрузка указанной сцены
        /// </summary>
        /// <param name="levelID"></param>
        public void LoadLevel(int levelID) {
            SceneManager.LoadScene(levelID);
        }

        public void Next() {
            if (CurrentObjectIndex < ObjectsPool.Length) CurrentObjectIndex++;
            EnableObject(CurrentObjectIndex);
        }

        public void Previous() {
            if (CurrentObjectIndex > 0) CurrentObjectIndex--;
            EnableObject(CurrentObjectIndex);
        }
    }
}
