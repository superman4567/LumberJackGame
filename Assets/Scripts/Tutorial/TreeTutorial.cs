using Cinemachine;
using UnityEngine;

public class TreeTutorial : MonoBehaviour
{
    [SerializeField] private ShowDialogue dialogue;
    [SerializeField] private CinemachineVirtualCamera vCam;

    private PlayerMovement player;
    private Animator animator;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = player.GetComponent<Animator>();
        ShowDialogue.OnFinishedDialogue += ShowDialogue_OnFinishedDialogue;
        dialogue.OnNextDialogue += Dialogue_OnNextDialogue;
    }

    private void OnDestroy()
    {
        ShowDialogue.OnFinishedDialogue -= ShowDialogue_OnFinishedDialogue;
        dialogue.OnNextDialogue -= Dialogue_OnNextDialogue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.enabled = false;
            animator.SetFloat("Velocity", 0);
        }
    }

    private void ShowDialogue_OnFinishedDialogue(object sender, string e)
    {
        if ((ShowDialogue)sender == dialogue)
        {
            player.enabled = true;
            vCam.Priority = 0;
        }
    }

    private void Dialogue_OnNextDialogue(object sender, int e)
    {
        if (e == 2)
        {
            vCam.Priority = 20;
        }
    }
}
