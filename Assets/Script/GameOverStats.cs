using TMPro;
using UnityEngine;

public class GameOverStats : MonoBehaviour
{
    [Header("Player Score Info")]
    [SerializeField] TextMeshProUGUI player1ScoreText;
    [SerializeField] TextMeshProUGUI player2ScoreText;

    [Header("Single Player Info")]
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI newhighScoreText;

    [Header("Multi Player Info")]
    [SerializeField] TextMeshProUGUI player1WonText;
    [SerializeField] TextMeshProUGUI player2WonText;

    [Header("Player Score Controller")]
    [SerializeField] Score_Controller player1_ScoreController;
    [SerializeField] Score_Controller player2_ScoreController;

    bool player1Won;
    bool newHighScore;

    enum GamePlayType
    {
        None,
        SinglePlayer,
        MultiPlayer
    }

    [Header("GamePlay Type")]
    [SerializeField] GamePlayType gamePlayType;

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
        player1ScoreText.text = "Your Score : " + player1_ScoreController.getScore();
        highScoreText.text = "HighScore : " + player1_ScoreController.GetHighScore();

        if (player1_ScoreController.getScore() >= player1_ScoreController.GetHighScore())
        {
            newHighScore = true;
        }

        if (newHighScore == true)
            newhighScoreText.enabled = true;
        else
            newhighScoreText.enabled = false;
    }

    private void MultiPlayerStats()
    {
        player1ScoreText.text = "Player 1 : " + player1_ScoreController.getScore();
        player2ScoreText.text = "Player 2 : " + player2_ScoreController.getScore();


        if (player1_ScoreController.getScore() > player2_ScoreController.getScore())
        {
            player1Won = true;
        }
        else if (player2_ScoreController.getScore() > player1_ScoreController.getScore())
        {
            player1Won = false;
        }

        if (player1Won == true)
            player1WonText.enabled = true;
        else
            player2WonText.enabled = true;
    }
}
