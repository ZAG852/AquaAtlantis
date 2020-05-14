using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXTrigger : MonoBehaviour
{
    FXplayer fXplayer;
    [SerializeField] 
    bool deathTrigger;


    [SerializeField] fxOptions fx;

    // Start is called before the first frame update
    void Start()
    {
        fXplayer = FXplayer.fxplayer;
        if(!deathTrigger)
        fXplayer.PlayFX(fx);
    }

    void OnDestroy(){
        fXplayer.PlayFX(fxOptions.deathFX);
    }
}
