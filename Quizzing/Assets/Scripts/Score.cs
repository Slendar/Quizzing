using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Quiz quiz;

    private byte numberOfAllQuestions = 0;
    private byte numberOfCorrectAnswers = 0;

    private void Start()
    {
        quiz.OnCorrectAnswerSelected += Quiz_OnCorrectAnswerSelected;

        numberOfAllQuestions = quiz.GetNumberOfQuestions();
    }

    private void Quiz_OnCorrectAnswerSelected(object sender, System.EventArgs e)
    {
        numberOfCorrectAnswers++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + GetScore() + "%";
    }

    public int GetScore()
    {
        return Mathf.RoundToInt((float)numberOfCorrectAnswers / numberOfAllQuestions * 100);
    }

    private void OnDestroy()
    {
        quiz.OnCorrectAnswerSelected -= Quiz_OnCorrectAnswerSelected;
    }
}
