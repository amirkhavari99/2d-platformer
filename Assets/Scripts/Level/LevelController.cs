using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private UIController ui;
    [SerializeField] private SFXManager sfxManager;

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
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            DontDestroyOnLoad(instance);
        }
        else if (instance && instance != this)
            DestroyImmediate(gameObject);
    }

    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.isLoaded)
        {
            if (player == null)
                player = GameObject.FindWithTag("PlayerController").GetComponent<PlayerController>();
            if (ui == null)
                ui = GameObject.FindWithTag("UIController").GetComponent<UIController>();
            if (sfxManager == null)
                sfxManager = GameObject.FindWithTag("SFXManager").GetComponent<SFXManager>();
        }
    }

    public Vector2 GetPlayerPosition()
    {
        if (player != null)
            return player.GetCurrentPosition();
        else
            return Vector2.zero;
    }

    public void PlayerGotACollectible(GameObject collectibleGameObject)
    {
        if (collectiblePoint > 0)
        {
            sfxManager.PlayCollectSFX();
            playerScore += collectiblePoint;
            ui.UpdateScore(playerScore);
            Destroy(collectibleGameObject);
        }
    }

    public void LevelCompleted()
    {
        if (finished == false)
        {
            sfxManager.PlayWinSFX();
            ui.ShowLevelEnd();
            finished = true;
            Debug.Log("Finish");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        finished = false;
        playerScore = 0;
    }
}
