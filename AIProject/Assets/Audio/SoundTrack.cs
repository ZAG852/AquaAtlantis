using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundTrack : MonoBehaviour
{
    public static SoundTrack soundTrack;

    public AudioClip[] soundTracks = new AudioClip[] { };

    AudioSource soundTrackSource;

    int m_currentSceneIndex;

    private void Awake()
    {
        if(soundTrack == null)
        {
            soundTrack = this;
        } else
        {
            Destroy(gameObject);
        }
        // grabs the current scene's index
        m_currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += sceneChanged;
    }

    private void Start()
    {
        soundTrackSource = GetComponent<AudioSource>();

    }

    void sceneChanged(Scene scene, LoadSceneMode mode)
    {
        m_currentSceneIndex = scene.buildIndex;
    }
}
