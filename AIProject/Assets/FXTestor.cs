using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXTestor : MonoBehaviour
{
    FXplayer fXplayer;
    void Start()
    {
        fXplayer = FXplayer.fxplayer;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "ItemPickup"))
        {
            fXplayer.PlayFX(fxOptions.itemPickup);
        }
        if (GUI.Button(new Rect(10, 110, 150, 100), "Fireball"))
        {
            fXplayer.PlayFX(fxOptions.fireball);
        }
        if (GUI.Button(new Rect(10, 220, 150, 100), "Dragon Proj"))
        {
            fXplayer.PlayFX(fxOptions.dragonProjectile);
        }
        if (GUI.Button(new Rect(10, 330, 150, 100), "Golem Proj"))
        {
            fXplayer.PlayFX(fxOptions.golemProjecile);
        }
        if (GUI.Button(new Rect(10, 440, 150, 100), "Slime Bullet"))
        {
            fXplayer.PlayFX(fxOptions.slimeBullet);
        }
        if (GUI.Button(new Rect(10, 550, 150, 100), "Snake Bullet"))
        {
            fXplayer.PlayFX(fxOptions.snakeBullet);
        }
        if (GUI.Button(new Rect(10, 660, 150, 100), "Bonk"))
        {
            fXplayer.PlayFX(fxOptions.bonk);
        }
    }
}
