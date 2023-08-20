using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private GameObject player;

    private void Start()
    {

        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();

    }

    public void Reset()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
