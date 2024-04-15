using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)] [SerializeField] private string question = "Enter new question text here";

    [SerializeField] private string[] answers = new string[4];
    [SerializeField] private byte correctAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(byte index)
    {
        return answers[index];
    }

    public byte GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
}
