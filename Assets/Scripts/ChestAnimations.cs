using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void ChestOpen()
    {
        animator.SetTrigger("OpenChestTrigger");
    }
}
