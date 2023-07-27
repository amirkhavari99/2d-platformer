using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] Text playerScoreText;
    [SerializeField] int collectiblePoint;

    private int playerScore = 0;
    public bool finished
    {
        get;
        private set;
    } = false;

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

    public void PlayerGotACollectible(GameObject collectibleGameObject)
    {
        if (collectiblePoint > 0)
        {
            playerScore += collectiblePoint;
            playerScoreText.text = $"Score: {playerScore}";
            Destroy(collectibleGameObject);
        }
    }

    public void LevelCompleted()
    {
        if (finished == false)
        {
            finished = true;
            Debug.LogError("Finish");
        }
    }
}
