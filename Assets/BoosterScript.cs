using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoosterScript : MonoBehaviour
{
    public LogicScript logicManager;

    public float activateDelay;
    
    void Start()
    {
        activateDelay = 5;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            logicManager.HandleBoosterDeactivated(gameObject, activateDelay);
        }
    }
}
