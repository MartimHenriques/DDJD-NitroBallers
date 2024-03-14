using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI winText;

    void Start()
    {
        int winner = PlayerPrefs.GetInt("Winner", -1);
        if (winner == 1)
        {
            winText.text = "YOU WON";
        }
        else
        {
            winText.text = "YOU LOST";
        }
    }


    public void Restart()
    {
        PlayerPrefs.SetInt("Winner", 0);
        SceneManager.LoadScene("Scene");
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
