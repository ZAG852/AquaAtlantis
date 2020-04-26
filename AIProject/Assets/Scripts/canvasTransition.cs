using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasTransition : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fadeIn();
    }

    public void fadeIn()
    {
        anim.SetTrigger("FadeIn");
    }
    public void fadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
}
