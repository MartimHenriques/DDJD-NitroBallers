using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public GameObject boosterObjectPrefab;
    public GameObject sizeUpObjectPrefab;

    public int playerScore;
    public int botScore;
    public Text textScore;
    public TextMeshProUGUI goalText;

    public Transform playerCarStartPosition;

    public Transform botCarStartPosition;
    public Transform ballStartPosition;

    public GameObject playerCar;
    public CarController playerCarController;

    public GameObject botCar;
    public GameObject ball;

    public bool isGoalTextDisplayed = false;

    public int isWinner;

    private Dictionary<GameObject, float> deactivatedPowerUps = new Dictionary<GameObject, float>();

    void Start()
    {
        InitializePowerUps();

        playerCar = GameObject.FindGameObjectWithTag("Player");
        playerCar.transform.rotation = Quaternion.Euler(0, 0, -90);
        botCar = GameObject.FindGameObjectWithTag("Bot");
        botCar.transform.rotation = Quaternion.Euler(0, 0, 90);
        ball = GameObject.FindGameObjectWithTag("Ball");

        playerCarController = playerCar.GetComponent<CarController>();
        playerScore = 0;
        botScore = 0;
        textScore.text = "0 - 0";
        goalText.gameObject.SetActive(false);
        isWinner = 0;
        isGoalTextDisplayed = false;
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

        playerCar.transform.SetPositionAndRotation(playerCarStartPosition.position, Quaternion.Euler(0, 0, -90));
        botCar.transform.SetPositionAndRotation(botCarStartPosition.position, Quaternion.Euler(0, 0, 90));
        ball.transform.position = ballStartPosition.position;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        isGoalTextDisplayed = false;
    }

    void InitializePowerUps()
    {
        Vector2[] powerUpsPos = new Vector2[4];
        powerUpsPos[0] = new Vector2(6, 4);
        powerUpsPos[1] = new Vector2(-6, 4);
        powerUpsPos[2] = new Vector2(-6, -4);
        powerUpsPos[3] = new Vector2(6, -4);

        for (int i = 0; i < powerUpsPos.Length; i++)
        {
            if (i%2==0)
            {
                GameObject powerUp = Instantiate(boosterObjectPrefab, powerUpsPos[i], Quaternion.identity);
                powerUp.GetComponent<BoosterScript>().logicManager = this;
            }
            else
            {
                GameObject powerUp = Instantiate(sizeUpObjectPrefab, powerUpsPos[i], Quaternion.identity);
                powerUp.GetComponent<SizeUpScript>().logicManager = this;
            }
        }
    }

    [ContextMenu("AddPlayerScore")]
    public void AddPlayerScore()
    {
        playerScore++;
        textScore.text = playerScore.ToString() + " - " + botScore.ToString();
        ResetGame();
        if (playerScore == 3)
        {
            isWinner = 1;
            // Store the winner information in PlayerPrefs
            PlayerPrefs.SetInt("Winner", isWinner);
            SceneManager.LoadScene("GameOver");
        }
    }

    [ContextMenu("AddBotScore")]
    public void AddBotScore()
    {
        botScore++;
        textScore.text = playerScore.ToString() + " - " + botScore.ToString();
        ResetGame();
        if (botScore == 3)
        {
            isWinner = 2;
            // Store the winner information in PlayerPrefs
            PlayerPrefs.SetInt("Winner", isWinner);
            SceneManager.LoadScene("GameOver");
        }
    }

    public void HandleBoosterDeactivated(GameObject collider, GameObject powerUpObject, float delay)
    {
        collider.GetComponent<CarController>().SetBoosterPowerUp(true);

        deactivatedPowerUps[powerUpObject] = delay;
        StartCoroutine(ReactivatePowerUp(powerUpObject, delay));
    }

    public void HandleSizeUpDeactivated(GameObject collider, GameObject powerUpObject, float delay)
    {
        collider.GetComponent<CarController>().SetSizePowerUp(true);

        deactivatedPowerUps[powerUpObject] = delay;
        StartCoroutine(ReactivatePowerUp(powerUpObject, delay));
    }

    private IEnumerator ReactivatePowerUp(GameObject powerUpObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reactivate the booster object
        if (deactivatedPowerUps.ContainsKey(powerUpObject))
        {
            deactivatedPowerUps.Remove(powerUpObject);
            powerUpObject.SetActive(true);
        }
        else
        {
            Debug.Log("Booster object is not in the dictionary");
        }
    }
}


