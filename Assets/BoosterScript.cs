using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterScript : MonoBehaviour
{

    public float spawnDelay = 5;
    private float timer = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (!gameObject.activeSelf)
        {
            timer += Time.deltaTime;

            if (timer > spawnDelay)
            {
                timer = 0;
                gameObject.SetActive(true);
            }
        }
    }

    //Future reference to spawn more boosters
    /*void SpawnBoosterBall()
    {
        gameObject.SetActive(true);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
