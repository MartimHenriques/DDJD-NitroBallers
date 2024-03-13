using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public int botScore;
    public Text textScore;
    public TextMeshProUGUI goalText;

    public Transform playerCarStartPosition;
    //public Transform botCarStartPosition;
    public Transform ballStartPosition;

    public GameObject playerCar;
    //public GameObject botCar;
    public GameObject ball;

    public bool isGoalTextDisplayed = false;

    void Start()
    {
        playerScore = 0;
        botScore = 0;
        textScore.text = "0 - 0";
        goalText.gameObject.SetActive(false);
    }

    void ResetGame()
    {
        StartCoroutine(ShowGoalTextAndReset());
    }

    IEnumerator ShowGoalTextAndReset()
    {

        isGoalTextDisplayed = true;

        goalText.text = "GOAL";
        goalText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        goalText.gameObject.SetActive(false);

        // Reset positions
        playerCar.transform.position = playerCarStartPosition.position;
        //botCar.transform.position = botCarStartPosition.position;
        ball.transform.position = ballStartPosition.position;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Reset car movement
        //playerCar.GetComponent<CarController>().StopCarMovement();
        //botCar.GetComponent<CarController>().StopCarMovement();

        isGoalTextDisplayed = false;
    }

    [ContextMenu("AddPlayerScore")]
    public void AddPlayerScore()
    {
        playerScore++;
        textScore.text = playerScore.ToString() + " - " + botScore.ToString();
        ResetGame();
    }

    [ContextMenu("AddBotScore")]
    public void AddBotScore()
    {
        botScore++;
        textScore.text = playerScore.ToString() + " - " + botScore.ToString();
        ResetGame();
    }

}