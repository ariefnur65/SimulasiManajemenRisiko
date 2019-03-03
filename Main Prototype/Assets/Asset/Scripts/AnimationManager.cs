using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
    public static AnimationManager instance;
    public Animator[] ObjectAnimation;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void StopAnimation()
    {
        foreach (Animator Animate in ObjectAnimation)
        {
            Animate.SetBool("Idle", true);
        }
    }

    public void PlayAnimation()
    {
        foreach (Animator Animate in ObjectAnimation)
        {
            Animate.SetBool("Idle", false);
        }
    }

    // Use this for initialization
    void Start () {
        StopAnimation();
    }
	
}
