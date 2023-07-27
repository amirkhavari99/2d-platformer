using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
