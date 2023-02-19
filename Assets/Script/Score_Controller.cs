using TMPro;
using UnityEngine;


public class Score_Controller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    private int score = 0;
    private int HighScore; 

    private void Start()
    {
        if(highScoreText!= null)
            highScoreText.text = " HighScore : " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void AddScore(int increament)
    {
        score += increament;
        scoreText.text = "" + score;
        GetHighScore();
    }

    public int getScore() { return score; }

    public int GetHighScore()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > HighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            if (highScoreText != null)
                highScoreText.text = "HighScore : " + score.ToString();
        }
        return HighScore;
    }
}