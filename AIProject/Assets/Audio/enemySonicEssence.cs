using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class enemySonicEssence : MonoBehaviour
{
    [SerializeField] enemyOptions enemy;

    AudioSource essenceSource;

    private void Awake()
    {
        essenceSource = GetComponent<AudioSource>();
        essenceSource.playOnAwake = true;
    }

    void Start()
    {
        // set output routing
        AudioMixer mixer = Resources.Load("MainMixer") as AudioMixer;
        AudioMixerGroup enemyGroup = mixer.FindMatchingGroups("Master/EnemyEssence")[0];

        essenceSource.outputAudioMixerGroup = enemyGroup;

        // set loop
        if (!essenceSource.loop)
            essenceSource.loop = true;

        // set random pitch
        essenceSource.pitch = Random.Range(.9f, 1.2f);

        // set audio clip
        essenceSource.clip = AudioManager.audioManager.GetEnemyEssenceClip(enemy);

        // play
        if (!essenceSource.isPlaying)
            essenceSource.Play();
    }
}
