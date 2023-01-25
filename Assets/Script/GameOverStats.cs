using TMPro;
using UnityEngine;

public class GameOverStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI newhighScoreText;

    [SerializeField] Score_Controller Score_Controller;

    bool newHighScore;
    private void Start()
    {
        scoreText.text = "Your Score : " + Score_Controller.getScore();
        highScoreText.text = "HighScore : " + Score_Controller.GetHighScore();

        if(Score_Controller.getScore() >= Score_Controller.GetHighScore())
        {
            newHighScore = true;
        }

        HighScore();
    }

    void HighScore()
    {
        if(newHighScore == true)
            newhighScoreText.enabled = true;
        else
            newhighScoreText.enabled = false;
    }
}
