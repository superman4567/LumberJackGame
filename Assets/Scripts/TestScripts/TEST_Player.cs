using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TEST_Player : MonoBehaviour
{
    //public static event Action interacting;
    [SerializeField] public UnityEvent<string> Interaction;

    public string messageToSend = "Hello, Listener! I am being send from Player to Interactable";

    private void Update()
    {
        Interact();
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interaction?.Invoke(messageToSend);

            float interactionPercentage = Time.deltaTime;
            int interactionSegment = (int)interactionPercentage;
            if (interactionPercentage >= 100) { return; }
        }
    }
}
