using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FXplayer : MonoBehaviour
{
    public static FXplayer fxplayer;
    AudioSource fxSource;

    // drop fx audio clips in the appropriate spots. Don't repeat fxOptions (left-side column)
    public audioOptions [] fxOptions = new audioOptions[] { };

    private void Awake()
    {
        if(fxplayer == null)
        {
            fxplayer = this;
        } else
        {
            Destroy(gameObject);
        }
        fxSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays audio with AudioSource.PlayOneShot(AudioClip audioClip)
    /// </summary>
    /// <param name="audioClip"></param>
    public void Play(AudioClip audioClip)
    {
        fxSource?.PlayOneShot(audioClip);
    }

    /// <summary>
    /// Plays audioclip based on the FX you want.
    /// </summary>
    /// <param name="fx"></param>
    public void PlayFX(fxOptions fx)
    {
        foreach (var option in fxOptions)
        {
            if (fx == option.fx)
            {
                print((int)option.fx);
                AudioClip clip = fxOptions[(int)option.fx].audioClip;
                float vol = fxOptions[(int)option.fx].clipVolume;
                fxSource.PlayOneShot(clip, vol);
                break;
            }
        }
    }
}
