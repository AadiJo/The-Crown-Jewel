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
    public float[] knockback = new float[] { 400f, 200f };

    
    void Start()
    {

    }
 
    void Update()
    {

    }
}
