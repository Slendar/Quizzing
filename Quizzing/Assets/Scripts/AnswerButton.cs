using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private GameObject correctAnswerBackground;
    [SerializeField] private TMP_Text buttonText;

    public Button GetButton()
    {
        return GetComponent<Button>();
    }

    public GameObject GetCorrectAnswerBackground()
    {
        return correctAnswerBackground;
    }

    public TMP_Text GetButtonText()
    {
        return buttonText; 
    }
}
