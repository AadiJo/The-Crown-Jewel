using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Slider healthBar;

    public bool dead = false;

    private float lerpSpeed = 0.55f;
    public bool godMode = false;
    private float time;
    private Animator animator;

    public float currentHealth;
    [HideInInspector] public float maxHealth = 30;
    //private GameManager gameManager;

    public void SetHealthBar(float maxHealth)
    {

        healthBar.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        //gameManager = FindObjectOfType<GameManager>();

    }

    public void SetGodMode(bool val)
    {

        godMode = val;
        if (val == false)
        {

            currentHealth = 20;

        }

    }


    private void Start()
    {
        SetHealthBar(maxHealth);
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage, float[] knockback)
    {


        if (!godMode)
        {

            //FindObjectOfType<AudioManager>().Play("PlayerHurt");
            animator.SetTrigger("hurt");

        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.velocity.x < 0)
        {

            rb.AddForce(new Vector2(knockback[0], knockback[1]));

        }
        else
        {

            rb.AddForce(new Vector2(-knockback[0], knockback[1]));

        }

        currentHealth -= damage;
        time = 0;

    }
    public void GainHealth(float health)
    {
        if (!dead)
        {

            currentHealth += health;
            time = 0;

        }


    }

    private void Update()
    {

        if (currentHealth <= 0)
        {

            dead = true;
            //gameManager.dead = true;
            currentHealth = 0;
            animator.SetTrigger("hurt");


        }

        if (currentHealth >= maxHealth)
        {

            currentHealth = maxHealth;

        }

        if (dead)
        {

            godMode = false;

        }

        if (godMode)
        {

            currentHealth = 30;

        }

        AnimateHealthBar();


    }

    private void AnimateHealthBar()
    {

        float targetHealth = currentHealth;
        float startHealth = healthBar.value;
        time += Time.deltaTime * lerpSpeed;

        healthBar.value = Mathf.Lerp(startHealth, targetHealth, time);

    }


}
