using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasTransition : MonoBehaviour
{
    Animator anim;
    float timer = 1f;
    bool starting = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fadeIn();
        
    }
    private void Update()
    {
        if(timer <=0 && starting)
        {
            starting = false;
            gameObject.SetActive(false);
        }
        else
        {
            timer -= Time.deltaTime;
        }
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
