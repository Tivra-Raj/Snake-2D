using Sounds;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverStats : MonoBehaviour
{
    [Header("Player Score Info")]
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;

    [Header("Single Player Info")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI newhighScoreText;

    [Header("Multi Player Info")]
    [SerializeField] private TextMeshProUGUI player1WonText;
    [SerializeField] private TextMeshProUGUI player2WonText;

    [Header("Player Score Controller")]
    [SerializeField] private Score_Controller player1_ScoreController;
    [SerializeField] private Score_Controller player2_ScoreController;

    private bool player1Won;
    private bool newHighScore;

    public enum GamePlayType
    {
        None,
        SinglePlayer,
        MultiPlayer
    }

    [Header("GamePlay Type")]
    public GamePlayType gamePlayType;

    private void Awake()
    {
        if(player1WonText != null && player2WonText != null)
        {
            player1WonText.enabled = false;
            player2WonText.enabled = false;
        }
    }

    private void Start()
    {
        switch (gamePlayType)
        {
            case GamePlayType.SinglePlayer: SinglePlayerStats();
                                            break;

            case GamePlayType.MultiPlayer:  MultiPlayerStats();
                                            break;
        }
    }

    void SinglePlayerStats()
    {
        player1ScoreText.text = "Your Score : " + player1_ScoreController.GetScore();
        highScoreText.text = "HighScore : " + player1_ScoreController.GetHighScore();

        if (player1_ScoreController.GetScore() >= player1_ScoreController.GetHighScore())
        {
            newHighScore = true;
        }

        if (newHighScore == true)
        {
            StartCoroutine(StopAndPlayBackgroundMusic(6f));
            StartCoroutine(CountDown());   
            newhighScoreText.enabled = true;
        }      
        else
            newhighScoreText.enabled = false;
    }

    private void MultiPlayerStats()
    {
        player1ScoreText.text = "Player 1 : " + player1_ScoreController.GetScore();
        player2ScoreText.text = "Player 2 : " + player2_ScoreController.GetScore();


        if (player1_ScoreController.GetScore() > player2_ScoreController.GetScore())
        {
            player1Won = true;
        }
        else if (player2_ScoreController.GetScore() > player1_ScoreController.GetScore())
        {
            player1Won = false;
        }

        if (player1Won == true)
        {
            StartCoroutine(StopAndPlayBackgroundMusic(1f));
            SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.GameWon);
            player1WonText.enabled = true;
        }         
        else
        {
            StartCoroutine(StopAndPlayBackgroundMusic(1f));
            SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.GameWon);
            player2WonText.enabled = true;
        }         
    }

    private IEnumerator StopAndPlayBackgroundMusic(float value)
    {
        SoundManager.Instance.StopMusic(Sounds.Sounds.BackgroundMusic);
        yield return new WaitForSeconds(value);
        SoundManager.Instance.PlayMusic(Sounds.Sounds.BackgroundMusic);
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(5f);
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.GameWon);
    }
}
