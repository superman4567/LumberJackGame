using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Interactable : MonoBehaviour
{
    public void TreeStateMachine(string messageFromPlayerScript)
    {
        Debug.Log("Received interaction segment: " + messageFromPlayerScript);
    }

}
