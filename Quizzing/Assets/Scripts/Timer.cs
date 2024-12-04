using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static event EventHandler<EventArgs> OnTimerRunOutWhileAnswering;
    public static event EventHandler<EventArgs> OnLoadNextScene;

    [SerializeField] private float timeToCompleteQuestion = 30f;
    [SerializeField] private float timeToShowCorrectAnswer = 10f;
    [SerializeField] private Image timerImage;

    private float currentMaxTimerValue;

    private bool isAnsweringQuestions = true;
    private float timerValue;

    private bool wasAnswerSelected;

    private void Start()
    {
        timerValue = timeToCompleteQuestion;
        currentMaxTimerValue = timeToCompleteQuestion;
        isAnsweringQuestions = true;

        Quiz.OnAnswerSelectedCall += Quiz_OnAnswerSelectedCall;
    }

    private void Quiz_OnAnswerSelectedCall(object sender, EventArgs e)
    {
        wasAnswerSelected = true;
        timerValue = 0;
    }

    private void Update()
    {
        timerValue -= Time.deltaTime;

        timerImage.fillAmount = timerValue / currentMaxTimerValue;

        if (timerValue <= 0)
        {
            if (isAnsweringQuestions)
            {
                isAnsweringQuestions = false;

                timerValue = timeToShowCorrectAnswer;
                currentMaxTimerValue = timeToShowCorrectAnswer;

                if (!wasAnswerSelected)
                {
                    OnTimerRunOutWhileAnswering?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                isAnsweringQuestions = true;
                OnLoadNextScene?.Invoke(this, EventArgs.Empty);

                timerValue = timeToCompleteQuestion;
                currentMaxTimerValue = timeToCompleteQuestion;
            }
            wasAnswerSelected = false;
        }
    }

    private void OnDestroy()
    {
        Quiz.OnAnswerSelectedCall -= Quiz_OnAnswerSelectedCall;
    }
}
