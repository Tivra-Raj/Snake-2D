using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject HowToPlayPanel;

    [Header("Main Menu Buttons")]
    [SerializeField] Button singlePlayer;
    [SerializeField] Button multiPlayer;
    [SerializeField] Button howToPlay;
    [SerializeField] Button quit;

    [Header("HowToPlay Buttons")]
    [SerializeField] Button closeHowToPlayPanel;

    private void Awake()
    {
        singlePlayer.onClick.AddListener(SinglePlayerLevel);
        multiPlayer.onClick.AddListener(MultiPlayerLevel);
        howToPlay.onClick.AddListener(HowToPlay);
        quit.onClick.AddListener(Quit);

        closeHowToPlayPanel.onClick.AddListener(CloseHowToPlay);
        HowToPlayPanel.SetActive(false);
    }

    public void SinglePlayerLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void MultiPlayerLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void HowToPlay()
    {
        HowToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        HowToPlayPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
