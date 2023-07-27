using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Camera mainCamera;

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
}
