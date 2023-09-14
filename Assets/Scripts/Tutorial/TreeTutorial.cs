using Cinemachine;
using UnityEngine;

public class TreeTutorial : MonoBehaviour
{
    [SerializeField] private ShowDialogue dialogue;
    [SerializeField] private CinemachineVirtualCamera vCam;

    private PlayerMovement player;
    private float moveSpeed;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        ShowDialogue.OnFinishedDialogue += ShowDialogue_OnFinishedDialogue;
        dialogue.OnNextDialogue += Dialogue_OnNextDialogue;
    }

    private void OnDestroy()
    {
        ShowDialogue.OnFinishedDialogue -= ShowDialogue_OnFinishedDialogue;
        dialogue.OnNextDialogue -= Dialogue_OnNextDialogue;
    }

    private void ShowDialogue_OnFinishedDialogue(object sender, string e)
    {
        if ((ShowDialogue)sender == dialogue)
        {
            player.movSpeed = moveSpeed;
            vCam.Priority = 0;
        }
    }

    private void Dialogue_OnNextDialogue(object sender, int e)
    {
        if (e == 2)
        {
            vCam.Priority = 20;
            moveSpeed = player.movSpeed;
            player.movSpeed = 0;
        }
    }
}
