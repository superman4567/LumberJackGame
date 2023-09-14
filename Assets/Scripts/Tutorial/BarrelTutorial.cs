using Cinemachine;
using System;
using UnityEngine;

public class BarrelTutorial : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;

    private PlayerMovement player;
    private ShowDialogue currentDialogue;

    private float moveSpeed;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
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
            player.movSpeed = moveSpeed;
            vCam.Priority = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == player.tag)
        {
            moveSpeed = player.movSpeed;
            player.movSpeed = 0;
            vCam.Priority = 21;
        }
    }
}
