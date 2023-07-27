using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text playerScoreText;
    [SerializeField] Text hoorayText;
    [SerializeField] Button restartButton;

    public void UpdateScore(int playerScore)
    {
        playerScoreText.text = $"Score: {playerScore}";
    }

    public void ShowLevelEnd()
    {
        hoorayText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void Restart()
    {
        LevelController.Instance.Restart();
    }
}
