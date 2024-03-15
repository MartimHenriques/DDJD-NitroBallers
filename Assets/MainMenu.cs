using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructions;
    public GameObject backButton;

    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject quitButton;

    //start
    void Start()
    {
        instructions.SetActive(false);
        backButton.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowInstructions()
    {
        instructions.SetActive(true);
        backButton.SetActive(true);
        playButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void HideInstructions()
    {
        instructions.SetActive(false);
        backButton.SetActive(false);
        playButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
