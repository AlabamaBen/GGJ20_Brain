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

    // Update is called once per frame
    void Update()
    {
        //Pause game
        if (Input.GetKeyDown(KeyCode.Escape) && !SceneManager.GetActiveScene().name.Equals("Game_Menu"))
        {
            Time.timeScale = 0;
        }
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

    //Quit game
    public void QuitGame()
    {
        Application.Quit();
    }

}


