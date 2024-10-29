using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public const string ANIM_MOVING = "Moving";
    public const string ANIM_DEAD = "Dead";

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayAnimMove()
    {
        animator.SetBool(ANIM_MOVING,true);
    }
    public void PlayAnimIdle()
    {
        animator.SetBool(ANIM_MOVING, false);
    }

    public void PlayAnimDead()=>animator.SetBool(ANIM_DEAD,true);
}
