using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueableAnimations : MonoBehaviour
{
    private const string WALK = "Walk", JOY = "Joy", IDLE = "Idle";
    [SerializeField] Animator animator;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();                 
    }
    public void Walk()
    {
        animator.SetBool(WALK,true);
        animator.SetBool(JOY,false);
        animator.SetBool(IDLE,false);
    }
    public void Joy()
    {
        animator.SetBool(WALK,false);
        animator.SetBool(JOY,true);
        animator.SetBool(IDLE,false);
    }
    public void Idle()
    {
        animator.SetBool(WALK,false);
        animator.SetBool(JOY,false);
        animator.SetBool(IDLE,true);
    }
}
