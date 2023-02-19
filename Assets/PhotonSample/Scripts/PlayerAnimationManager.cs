using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.cyraxchel.pun {
    public class PlayerAnimationManager : MonoBehaviourPun {

        Animator animator;
        [SerializeField]
        float directionDampTime = 0.25f;

        public Joystick joystick;

        // Start is called before the first frame update
        void Start() {
            animator = GetComponent<Animator>();
            if(!animator) {
                Debug.LogError("PlayerAnimatorManager is Missings Animator Component");
            }
        }

        // Update is called once per frame
        void Update() {
            if(photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
                return;
            }

            if (!animator) return;

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Base Layer.Run")) {
                if (Input.GetButtonDown("Fire2")) {
                    animator.SetTrigger("Jump");
                }
            }


            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if(joystick != null) {
                h = joystick.Horizontal;
                v = joystick.Vertical;
            }

            if (v < 0) v = 0;
            animator.SetFloat("Speed", h * h + v * v);
            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
        }

        internal void SetControl(Joystick _joystick) {
            joystick = _joystick;
        }
    }
}