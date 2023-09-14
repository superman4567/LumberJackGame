using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;

    private ShowDialogue currentDialogue;
    private PlayerMovement player;
    private float moveSpeed;
    private bool findFollow;

    // Start is called before the first frame update
    void Start()
    {
        currentDialogue = GetComponent<ShowDialogue>();
        player = FindObjectOfType<PlayerMovement>();
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
            vCam.Follow = FindObjectOfType<Orc_Animations>().transform;
        }
    }

    private void ShowDialogue_OnFinishedDialogue(object sender, string e)
    {
        if (currentDialogue == (ShowDialogue)sender)
        {
            player.movSpeed = moveSpeed;
            vCam.Priority = 0;
            Time.timeScale = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == player.tag)
        {
            moveSpeed = player.movSpeed;
            player.movSpeed = 0;
            findFollow = true;
            vCam.Priority = 21;
            Time.timeScale = .5f;
        }
    }
}
