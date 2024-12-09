using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        GameManager.OnEndGame += GameManager_OnEndGame;
        gameObject.SetActive(false);
    }

    private void GameManager_OnEndGame(object sender, GameManager.OnEndGameEventArgs e)
    {
        scoreText.text = "Congratulations\n" + e.score + "%";
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.OnEndGame -= GameManager_OnEndGame;
    }
}
