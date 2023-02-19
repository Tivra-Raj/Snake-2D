using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu_Manager : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Snake_Controller snake_Controller;

    [Header("Game Over Buttons")]
    [SerializeField] Button restart;
    [SerializeField] Button mainMenu;

    [Header("Pause Menu Buttons")]
    [SerializeField] Button back;

    static bool GameIsPaused = false;

    private void Awake()
    {
        restart.onClick.AddListener(Restart);
        mainMenu.onClick.AddListener(MainMenu);

        back.onClick.AddListener(ClosePauseMenu);

        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ClosePauseMenu();
            }
            else
            {
                PauseMenu();
            }
        }
    }
    public void GameOver()
    {
        snake_Controller.gameOver = true;
        gameOverMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
