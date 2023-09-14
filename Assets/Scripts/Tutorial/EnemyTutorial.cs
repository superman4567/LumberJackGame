using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTutorial : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private GameObject dummy;

    private ShowDialogue currentDialogue;
    private PlayerMovement player;
    private Animator animator;
    private bool findFollow;

    void Start()
    {
        currentDialogue = GetComponent<ShowDialogue>();
        player = FindObjectOfType<PlayerMovement>();
        animator = player.GetComponent<Animator>();
        ShowDialogue.OnFinishedDialogue += ShowDialogue_OnFinishedDialogue;
    }

    private void OnDestroy()
    {
        ShowDialogue.OnFinishedDialogue -= ShowDialogue_OnFinishedDialogue;
    }

    private void LateUpdate()
    {
        if (!findFollow) return;

        if (vCam.Follow == null)
        {
            //change to dummy
            vCam.Follow = dummy.transform;
        }
    }

    private void ShowDialogue_OnFinishedDialogue(object sender, string e)
    {
        if (currentDialogue == (ShowDialogue)sender)
        {
            player.enabled = true;
            vCam.Priority = 0;
            Time.timeScale = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            findFollow = true;
            vCam.Priority = 21;
            Time.timeScale = .5f;
            player.enabled = false;
            animator.SetFloat("Velocity", 0);
        }
    }
}
