using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public GameObject boosterObjectPrefab;
    public CarController playerCar;

    public int playerScore;
    public int botScore;
    public Text textScore;

    private Dictionary<GameObject, float> deactivatedBoosters = new Dictionary<GameObject, float>();

    void Start()
    {
        InitializeBoosters();

        playerCar = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        playerScore = 0;
        botScore = 0;
        textScore.text = "0 - 0";
    }

    void InitializeBoosters()
    {
        Vector2[] boostersPos = new Vector2[4];
        boostersPos[0] = new Vector2(6, 4);
        boostersPos[1] = new Vector2(-6, 4);
        boostersPos[2] = new Vector2(-6, -4);
        boostersPos[3] = new Vector2(6, -4);

        foreach (Vector2 pos in boostersPos)
        {
            GameObject booster = Instantiate(boosterObjectPrefab, pos, Quaternion.identity);
            booster.GetComponent<BoosterScript>().logicManager = this;
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

    public void HandleBoosterDeactivated(GameObject boosterObject, float delay)
    {
        playerCar.SetBoosterFuel(true);

        deactivatedBoosters[boosterObject] = delay;
        StartCoroutine(ReactivateBooster(boosterObject, delay));
    }

    private IEnumerator ReactivateBooster(GameObject boosterObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reactivate the booster object
        if (deactivatedBoosters.ContainsKey(boosterObject))
        {
            deactivatedBoosters.Remove(boosterObject);
            boosterObject.SetActive(true);
        }
        else
        {
            Debug.Log("Booster object is not in the dictionary");
        }
    }
}
