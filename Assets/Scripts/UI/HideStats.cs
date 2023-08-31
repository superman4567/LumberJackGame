using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HideStats : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI buttonText;
    private bool isShowing = true;

    private void Start()
    {
        animator.SetBool("IsShowing", true);
        buttonText.text = "Show stats";
        isShowing = true;
    }

    public void ShowOrHiding()
    {
        Debug.Log(isShowing);

        if (isShowing)
        {
            animator.SetBool("IsShowing", false);
            buttonText.text = "Hide";
            isShowing = false;
        }
        else if (!isShowing)
        {
            animator.SetBool("IsShowing", true);
            buttonText.text = "Show stats";
            isShowing = true;
        }
        
    }
}
