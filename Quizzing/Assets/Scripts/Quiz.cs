using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;

public class Quiz : MonoBehaviour
{
    public static event EventHandler<EventArgs> OnAnswerSelectedCall;
    public event EventHandler<EventArgs> OnCorrectAnswerSelected;
    public event EventHandler<EventArgs> OnQuestionDisplay;
    public event EventHandler<EventArgs> OnLastQuestionAnswer;

    [Header("Questions")]
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private List<QuestionSO> questionSOs = new List<QuestionSO>();
    private QuestionSO currentQuestionSO;

    [Header("Answers")]
    [SerializeField] private AnswerButton[] answerButtons;
    private byte correctAnswerIndex;

    private void Start()
    {
        Timer.OnLoadNextScene += Timer_OnLoadNextScene;
        Timer.OnTimerRunOutWhileAnswering += Timer_OnTimerWhileAnsweringRunOut;
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.01f);
        DisplayRandomQuestion();
    }

    private void Timer_OnTimerWhileAnsweringRunOut(object sender, EventArgs e)
    {
        SetAllButtonsInteractable(false);

        questionText.text = "You've ran out of time :( \n" + "Correct answer is: " + answerButtons[correctAnswerIndex].GetButtonText().text;
        answerButtons[correctAnswerIndex].GetCorrectAnswerBackground().SetActive(true);
    }

    private void Timer_OnLoadNextScene(object sender, EventArgs e)
    {
        DisplayRandomQuestion();
    }

    private void DisplayRandomQuestion()
    {
        if (questionSOs.Count == 0)
        {
            OnLastQuestionAnswer?.Invoke(this, EventArgs.Empty);
            return;
        }
        OnQuestionDisplay?.Invoke(this, EventArgs.Empty);

        currentQuestionSO = questionSOs[UnityEngine.Random.Range(0, questionSOs.Count)];

        questionText.text = currentQuestionSO.GetQuestion();
        correctAnswerIndex = currentQuestionSO.GetCorrectAnswerIndex();

        SetAllButtonsInteractable(true);
        SetAllButtonsBacgroundToDefault();

        for (byte i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetButtonText().text = currentQuestionSO.GetAnswer(i);
        }

        questionSOs.Remove(currentQuestionSO);
    }

    public void OnAnswerSelected(int index)
    {
        SetAllButtonsInteractable(false);

        if (correctAnswerIndex == index)
        {
            questionText.text = "Correct!";
            answerButtons[index].GetCorrectAnswerBackground().SetActive(true);
            OnCorrectAnswerSelected?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            questionText.text = answerButtons[index]?.GetButtonText().text + " is incorrect! :(" + "\n" + "Correct answer is: " + answerButtons[correctAnswerIndex].GetButtonText().text;
            answerButtons[correctAnswerIndex].GetCorrectAnswerBackground().SetActive(true);
        }

        OnAnswerSelectedCall?.Invoke(this, EventArgs.Empty);
    }

    private void SetAllButtonsInteractable(bool setInteractable)
    {
        for(byte i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetButton().interactable = setInteractable;
        }
    }

    private void SetAllButtonsBacgroundToDefault()
    {
        for (byte i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetCorrectAnswerBackground().SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Timer.OnLoadNextScene -= Timer_OnLoadNextScene;
        Timer.OnTimerRunOutWhileAnswering -= Timer_OnTimerWhileAnsweringRunOut;
    }

    public byte GetNumberOfQuestions()
    {
        return (byte)questionSOs.Count;
    }
}
