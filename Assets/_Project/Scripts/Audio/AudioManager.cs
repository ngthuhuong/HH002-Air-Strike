using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusicSource;
    void Start()
    {
        backgroundMusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
