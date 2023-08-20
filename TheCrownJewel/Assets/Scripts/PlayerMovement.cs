using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    private PlayerHealth health;

    public float runSpeed = 40f;
    private float originalRunSpeed;
    private float bottomBarrier = -40f;

    float horizontalMove = 0f;
    bool jump = false;
    [HideInInspector] public bool crouch = false;
    bool attacking = false;
    [SerializeField] public Vector3 initialCords { get; private set; }

    public bool inCave = false;

    public bool canMove = true;

    void Start()
    {

        health = GetComponent<PlayerHealth>();
        originalRunSpeed = runSpeed;
        initialCords = transform.position;

    }
    void Update()
    {
        if (health.dead)
        {

            canMove = false;

        }

        if (canMove)
        {

            runSpeed = originalRunSpeed;

        }
        else
        {

            runSpeed = 0;

        }

        if (transform.position.y < bottomBarrier)
        {

            transform.position = initialCords;

        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;




        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (canMove)
        {

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                crouch = false;
                animator.SetBool("isJumping", true);
                if (controller.m_Grounded)
                {

                    //FindObjectOfType<AudioManager>().Play("Jump");

                }

            }

        }


        if (controller.m_Falling)
        {

            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);

        }

        if (canMove)
        {

            if (Input.GetButtonDown("Crouch"))
            {
                if (controller.m_Grounded)
                {

                    crouch = true;

                }

            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }

            if (!crouch)
            {
                if (horizontalMove != 0)
                {

                    if (Input.GetButton("Sprint"))
                    {

                        controller.m_Running = true;
                        animator.SetBool("isRunning", true);

                    }
                }

                if (Input.GetButtonUp("Sprint"))
                {

                    controller.m_Running = false;
                    animator.SetBool("isRunning", false);

                }
                if (horizontalMove == 0)
                {

                    controller.m_Running = false;
                    animator.SetBool("isRunning", false);

                }

            }


        }


    }

    public void OnLanding()
    {

        animator.SetBool("isFalling", false);

    }

    public void onCrouching(bool isCrouching)
    {

        animator.SetBool("isCrouching", isCrouching);

    }

    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }


}