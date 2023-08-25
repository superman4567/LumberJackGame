using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1Script : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DialogueManager.Instance.ShowTutorial1();
    }
}
