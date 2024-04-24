using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip clipMusic;
    public AudioClip clipVictory;
    public AudioClip clipGameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void Music()
    {
        audioSource.PlayOneShot(clipMusic);
    }
    public void Victory()
    {
        audioSource.PlayOneShot(clipVictory);
    }
    public void GameOver()
    {
        audioSource.PlayOneShot(clipGameOver);
    }
}
