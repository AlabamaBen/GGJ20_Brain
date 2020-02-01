using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Gm = null;

    public bool ColorActivated = false;
    public bool ReadActivated = false;
    public bool SoundActivated = false;
    private bool gameOnPause;

    void Awake()
    {
        if (Gm == null)
        {
            Gm = this;
        }
        else if (Gm != this)
        {
            Destroy(gameObject);
        }
    }

    //Update is called once per frame
    void Update()
    {
        //Pause game
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start")) && !SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            if (!gameOnPause)
            {
                PauseGame();           
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    //Unpause the game
    public void PauseGame()
    {
        gameOnPause = true;
        Time.timeScale = 0;
        UIManager.instance.PauseGame();
    }

    //Unpause the game
    public void UnpauseGame()
    {
        gameOnPause = false;
        Time.timeScale = 1;
        UIManager.instance.UnpauseGame();
    }

    //Restart current level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    //Start a new game
    public void StartNewGame()
    {
        SceneManager.LoadScene("Annelyse", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    //Load menu
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //Quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    public bool IsGameOnPause()
    {
        return gameOnPause;
    }
}


