using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event EventHandler<OnEndGameEventArgs> OnEndGame;
    public class OnEndGameEventArgs : EventArgs
    {
        public int score;
    }

    [SerializeField] private Quiz quiz;
    [SerializeField] private Score score;

    private void Start()
    {
        quiz.OnLastQuestionAnswer += Quiz_OnLastQuestionAnswer;
    }

    private void Quiz_OnLastQuestionAnswer(object sender, System.EventArgs e)
    {
        OnEndGame?.Invoke(this, new OnEndGameEventArgs() { 
            score = score.GetScore()
        });
        quiz.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        quiz.OnLastQuestionAnswer -= Quiz_OnLastQuestionAnswer;
    }
}
