using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] Text playerScoreText;

    private int playerScore = 0;

    private static LevelController instance = null;
    public static LevelController Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance && instance != this)
            DestroyImmediate(gameObject);
    }

    public Vector2 GetPlayerPosition()
    {
        return player.GetCurrentPosition();
    }

    public void PlayerGotAPoint(int point)
    {
        if(point > 0)
        {
            playerScore += point;
            playerScoreText.text = $"Score: {playerScore}"; 
        }
    }
}
