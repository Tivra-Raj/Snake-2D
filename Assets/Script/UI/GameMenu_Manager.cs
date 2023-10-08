using Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu_Manager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Snake_Controller[] snake_Controller;

    [Header("Game Over Buttons")]
    [SerializeField] private Button restart;
    [SerializeField] private Button mainMenu;

    [Header("Pause Menu Buttons")]
    [SerializeField] private Button back;

    private static bool GameIsPaused = false;

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
        for(int i = 0; i < snake_Controller.Length; i++)
            snake_Controller[i].gameOverOrPaused = true;
        gameOverMenu.SetActive(true);
    }

    public void Restart()
    {
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.ButtonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.ButtonClick);
        SceneManager.LoadScene(0);
    }
    public void PauseMenu()
    {
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.ButtonClick);
        pauseMenu.SetActive(true);
        for (int i = 0; i < snake_Controller.Length; i++)
            snake_Controller[i].gameOverOrPaused = true;
        GameIsPaused = true;
    }

    public void ClosePauseMenu()
    {
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.ButtonClick);
        pauseMenu.SetActive(false);
        for (int i = 0; i < snake_Controller.Length; i++)
            snake_Controller[i].gameOverOrPaused = false;
        GameIsPaused = false;
    }
}
