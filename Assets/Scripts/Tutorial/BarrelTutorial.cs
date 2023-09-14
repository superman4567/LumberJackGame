using Cinemachine;
using System;
using UnityEngine;

public class BarrelTutorial : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;

    private PlayerMovement player;
    private ShowDialogue currentDialogue;
    private Animator animator;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = player.GetComponent<Animator>();
        currentDialogue = GetComponent<ShowDialogue>();
        ShowDialogue.OnFinishedDialogue += ShowDialogue_OnFinishedDialogue;
    }

    private void OnDestroy()
    {
        ShowDialogue.OnFinishedDialogue -= ShowDialogue_OnFinishedDialogue;
    }

    private void ShowDialogue_OnFinishedDialogue(object sender, string e)
    {
        if ((ShowDialogue)sender == currentDialogue)
        {
            player.enabled = true;
            vCam.Priority = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.enabled = false;
            animator.SetFloat("Velocity", 0);
            vCam.Priority = 21;
        }
    }
}
