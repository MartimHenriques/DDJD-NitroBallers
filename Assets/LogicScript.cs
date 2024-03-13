using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public GameObject boosterObjectPrefab;
    public GameObject sizeUpObjectPrefab;
    public CarController playerCar;

    public int playerScore;
    public int botScore;
    public Text textScore;

    private Dictionary<GameObject, float> deactivatedPowerUps = new Dictionary<GameObject, float>();

    void Start()
    {
        InitializePowerUps();

        playerCar = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        playerScore = 0;
        botScore = 0;
        textScore.text = "0 - 0";
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
        UpdateScoreText();
    }

    [ContextMenu("AddBotScore")]
    public void AddBotScore()
    {
        botScore++;
        UpdateScoreText();
    }

    [ContextMenu("ResetScore")]
    public void ResetScore()
    {
        playerScore = 0;
        botScore = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        textScore.text = playerScore.ToString() + " - " + botScore.ToString();
    }

    public void HandleBoosterDeactivated(GameObject powerUpObject, float delay)
    {
        playerCar.SetBoosterPowerUp(true);

        deactivatedPowerUps[powerUpObject] = delay;
        StartCoroutine(ReactivatePowerUp(powerUpObject, delay));
    }

    public void HandleSizeUpDeactivated(GameObject powerUpObject, float delay)
    {
        playerCar.SetSizePowerUp(true);

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
