using TMPro;
using UnityEngine;


public class Score_Controller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

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

    public void DecreaseScore(int decreament)
    {
        if(score > 0)
            score -= decreament;
        else score = 0;
        scoreText.text = "" + score;
    }

    public int GetScore() { return score; }

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