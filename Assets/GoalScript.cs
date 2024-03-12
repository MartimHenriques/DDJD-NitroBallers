using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoalScript : MonoBehaviour
{

    public LogicScript logicManager;

    // Start is called before the first frame update
    void Start()
    {
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            string parentObject = transform.parent.name;
            if (parentObject == "Player Goal" && !logicManager.isGoalTextDisplayed)
            {
                logicManager.AddBotScore();
            }
            else if (parentObject == "Bot Goal" && !logicManager.isGoalTextDisplayed)
            {
                logicManager.AddPlayerScore();
            }
        }
    }
}
