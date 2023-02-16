using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using com.czeeep.network.player;

namespace com.cyraxchel.pun {
    public class PlayerUI : MonoBehaviour {

        
        #region Public Fields
        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        TextMeshProUGUI playerNameText;

        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        Slider playerHealthSlider;

        [Tooltip("Pixel offset from the player target")]
        [SerializeField]
        Vector3 screenOffset = new Vector3(0, 30, 0);
        #endregion

        #region Private Fields

        PlayerNetwork target;
        float characterControllerHeight = 0f;
        Transform targetTransform;
        Renderer targetRenderer;
        CanvasGroup _canvasGroup;
        Vector3 targetPosition;

        #endregion

        #region MonoBehaviour Callbacks

        // Start is called before the first frame update
        void Awake() {
            transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        // Update is called once per frame
        void Update() {
            if(playerHealthSlider != null) {
                playerHealthSlider.value = target.Health;
            }
            if(target == null) {
                Destroy(gameObject);
                return;
            }
        }

        private void LateUpdate() {
            if(targetRenderer != null) {
                _canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }
            if(targetTransform != null) {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                transform.position = Camera.main.WorldToScreenPoint(targetPosition)+screenOffset;
            }
        }
        #endregion

        #region Public Methods

        public void SetTarget(PlayerNetwork _target) {
            if(_target == null) {
                Debug.LogError("Missing PlayerManager target for PlayerUI.SetTarget.", this);
                return;
            }
            target = _target;

            targetTransform = target.GetComponent<Transform>();
            targetRenderer = target.GetComponent<Renderer>();
            CharacterController characterController = target.GetComponent<CharacterController>();
            if(characterController != null) {
                characterControllerHeight = characterController.height;
            }

            if(playerNameText != null) {
                playerNameText.text = target.photonView.Owner.NickName;
            }
        }

        #endregion

    }
}