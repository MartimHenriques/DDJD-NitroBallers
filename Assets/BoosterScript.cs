using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoosterScript : MonoBehaviour
{
    public LogicScript logicManager;

    public float activateDelay;

    private readonly float minScale = 1.5f;
    private readonly float maxScale = 2f;
    private readonly float scaleSpeed = 2f;
    private Vector3 targetScale;
    
    void Start()
    {
        activateDelay = 12;
        targetScale = transform.localScale;
    }

    void Update()
    {
        float scaleFactor = Mathf.PingPong(Time.time * scaleSpeed, 1.0f); // Ping pong between 0 and 1
        float scale = Mathf.Lerp(minScale, maxScale, scaleFactor);
        targetScale.x = scale;
        targetScale.y = scale;
        transform.localScale = targetScale;
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
