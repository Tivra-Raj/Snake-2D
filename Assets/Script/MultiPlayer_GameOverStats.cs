using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiPlayer_GameOverStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI player1ScoreText;
    [SerializeField] TextMeshProUGUI player2ScoreText;
    [SerializeField] TextMeshProUGUI player1WonText;
    [SerializeField] TextMeshProUGUI player2WonText;

    [SerializeField] Score_Controller Score1_Controller;
    [SerializeField] Score_Controller Score2_Controller;

    bool player1Won;

    private void Awake()
    {
        player1WonText.enabled = false;
        player2WonText.enabled = false;
    }
    private void Start()
    {
        player1ScoreText.text = "Player 1 : " + Score1_Controller.getScore();
        player2ScoreText.text = "Player 2 : " + Score2_Controller.GetMultiPlayerScore();

        if (Score1_Controller.getScore() > Score2_Controller.GetMultiPlayerScore())
        {
            player1Won = true;
        }
        else if (Score2_Controller.GetMultiPlayerScore() > Score1_Controller.getScore())
        {
            player1Won = false;
        }

        DisplayWonMessage();
    }

    void DisplayWonMessage()
    {
        if (player1Won == true)
            player1WonText.enabled = true;
        else
            player2WonText.enabled = true;
    }
}
