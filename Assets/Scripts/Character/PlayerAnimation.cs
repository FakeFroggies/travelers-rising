using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunning : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
        if(animator.GetBool("isRun") && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isSprint", true);
        }
        else
        {
            animator.SetBool("isSprint", false);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isJump", true);
        }
        else
        {
            animator.SetBool("isJump", false);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
}
