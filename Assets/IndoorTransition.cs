using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IndoorTransition : MonoBehaviour
{
    [SerializeField] private int sceneIndexToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneIndexToLoad);
        }
    }
}
