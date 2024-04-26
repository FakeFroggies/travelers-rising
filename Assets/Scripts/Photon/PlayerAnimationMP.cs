using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerRunningMP : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    PhotonView view;
    void Start()
    {
        animator = GetComponent<Animator>();
        view = GetComponentInParent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                animator.SetBool("isRun", true);
            }
            else
            {
                animator.SetBool("isRun", false);
            }
                animator.SetBool("isSprint", animator.GetBool("isRun") && Input.GetKey(KeyCode.LeftShift));
                animator.SetBool("isJump", Input.GetKey(KeyCode.Space));
                animator.SetBool("Attack", Input.GetKey(KeyCode.Mouse0));
        }
    }
}
