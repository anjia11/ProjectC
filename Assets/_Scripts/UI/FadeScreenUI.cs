using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreenUI : MonoBehaviour
{
    public static FadeScreenUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeIn() => anim.SetTrigger("FadeIn");
    public void FadeOut() => anim.SetTrigger("FadeOut");
}
