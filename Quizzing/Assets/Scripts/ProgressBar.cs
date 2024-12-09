using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    [SerializeField] private Quiz quiz;

    private Slider progressBar;

    private void Start()
    {
        quiz.OnQuestionDisplay += Quiz_OnQuestionDisplay;
        progressBar = GetComponent<Slider>();

        progressBar.maxValue = quiz.GetNumberOfQuestions();
        progressBar.value = 0;
    }

    private void Quiz_OnQuestionDisplay(object sender, System.EventArgs e)
    {
        progressBar.value++;
    }

    private void OnDestroy()
    {
        quiz.OnQuestionDisplay -= Quiz_OnQuestionDisplay;
    }
}
