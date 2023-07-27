using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioClip collectSFX;
    [SerializeField] private AudioClip winSFX;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCollectSFX()
    {
        PlaySFX(collectSFX);
    }

    public void PlayWinSFX()
    {
        PlaySFX(winSFX);
    }

    private void PlaySFX(AudioClip audioClip)
    {
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
