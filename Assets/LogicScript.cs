using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public int botScore;
    public Text textScore;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        botScore = 0;
        textScore.text = "0 - 0";
    }

    [ContextMenu("AddPlayerScore")]
    public void AddPlayerScore()
    {
        playerScore++;
        textScore.text = playerScore.ToString() + " - " + botScore.ToString();
    }

    [ContextMenu("AddBotScore")]
    public void AddBotScore()
    {
        botScore++;
        textScore.text = playerScore.ToString() + " - " + botScore.ToString();
    }

}
