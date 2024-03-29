using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    bool isAttacking = false;
    public Transform attackPoint;
    public float attackRange = 0.5f;

    private Rigidbody2D rb;

    public LayerMask enemyLayers;
    public ShockWaveManager shockWaveManager;
    public LayerMask bossLayers;

    public PlayerMovement playerMovement;
    public CharacterController2D characterController;
    //private GameManager gameManager;

    public int attackDamage = 10;
    private PlayerHealth health;

    void Start()
    {

        //PauseMenu.GameisPaused = false;
        health = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        //gameManager = FindObjectOfType<GameManager>();

    }

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {

            if (!characterController.m_Running)
            {
                Attack(attackPoint, attackRange);
            }


        }

        if (Input.GetKeyDown(KeyCode.E))
        {

            Special();

        }

        if (isAttacking)
        {

            playerMovement.canMove = false;

        }
        else
        {

            playerMovement.canMove = true;

        }

        if (health.godMode)
        {

            attackDamage = 1000;

        }
        else
        {

            attackDamage = 10;

        }

    }

    private void Attack(Transform attackPoint, float attackRange)
    {

        if (playerMovement.canMove)//&& !PauseMenu.GameisPaused)
        {



            //Play animation
            int attackRandomizer = Random.Range(0, 2);
            animator.SetInteger("attackRandomizer", attackRandomizer);
            if (!animator.GetBool("isJumping") && !animator.GetBool("isFalling"))
            {

                if (attackRandomizer == 0)
                {

                    attackPoint.transform.position = new Vector2(attackPoint.position.x, transform.position.y - 0.2f);

                }
                else if (attackRandomizer == 1)
                {

                    attackPoint.transform.position = new Vector2(attackPoint.position.x, transform.position.y - 0.8f);

                }

            }
            else
            {

                attackPoint.transform.position = new Vector2(attackPoint.position.x, transform.position.y + 0.1f);

            }

            //FindObjectOfType<AudioManager>().Play("PlayerAttack");
            animator.SetTrigger("attack");
            StartCoroutine(stopMovement());

            // Detect enemies in range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            // Collider2D[] hitBosses = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, bossLayers);

            // Damage
            foreach (Collider2D enemy in hitEnemies)
            {

                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

            }

            //foreach (Collider2D boss in hitBosses)
            //{
            //if (boss.GetComponent<Boss>() != null)
            //{

            //boss.GetComponent<Boss>().TakeDamage(attackDamage);

            //}
            //else
            //{

            //boss.GetComponent<BigBoss>().TakeDamage(attackDamage);

            //}


            //}

            if (animator.GetBool("isCrouching"))
            {

                attackPoint.transform.position = new Vector2(attackPoint.position.x, transform.position.y - 0.8f);
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                animator.SetTrigger("attack");
                StartCoroutine(stopMovement());

            }
            else
            {

                animator.SetBool("isFalling", false);
                animator.SetBool("isJumping", false);
                attackPoint.transform.position = new Vector2(attackPoint.position.x, transform.position.y - 0.2f);
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                animator.SetTrigger("attack");
                StartCoroutine(stopMovement());

            }


        }



    }

    private void Special()
    {
        if (characterController.m_Grounded && !playerMovement.crouch && !characterController.m_Running)
        {
            StartCoroutine(temp());
        }


    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {

            return;

        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator stopMovement()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;

    }

    IEnumerator temp()
    {
        Vector2 oldAttackPointCords = attackPoint.transform.position; attackPoint.transform.position = transform.position;
        float oldAttackRange = attackRange; attackRange = 3.5f;
        animator.SetBool("isSpecial", true);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        isAttacking = true;
        shockWaveManager.CallShockWave();
        yield return new WaitForSeconds(2f);
        animator.SetBool("isSpecial", false);
        attackPoint.transform.position = oldAttackPointCords;
        attackRange = oldAttackRange;
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.layer == 9)
        {

            health.TakeDamage(1, new float[] { 2000, 200 });
            if (health.currentHealth <= 0)
            {

                //gameManager.killerName = "SPIKE";
                //Debug.Log("Death by SPIKE");

            }

        }

    }
}
