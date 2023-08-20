using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 10;
    int currentHealth;
    public GameObject glow;
    private Animator animator;
    public float attackPower;
    private GameObject player;
    private GameManager gameManager;
    private PlayerHealth playerHealth;
    private PlayerCombat playerCombat;
    public float[] knockback = new float[] { 400f, 200f };


    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerCombat = player.GetComponent<PlayerCombat>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void TakeDamage(float Damage)
    {
        Debug.Log("Hit!");

        StartCoroutine(delayHurtAnimation(0.1f));

    }

    IEnumerator delayHurtAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetTrigger("Hit");

    }
}
