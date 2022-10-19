using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    private splashMenu sp;
    private static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    private void OnEnable()
    {
        InputScript.doPause += m_PauseGame;
    }

    private void OnDisable()
    {
        InputScript.doPause -= m_PauseGame;
    }

    private void m_PauseGame()
    {
        Debug.Log("happening");
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitingButton()
    {
        Application.Quit();
        Debug.Log("You Quit!");
    }

    public void LoadMenue()
    {
        SceneManager.LoadScene("UI Scene");
    }
}