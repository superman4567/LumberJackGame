using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Animator animator;
    private AxeDetection axeDetection;
    private bool hasExited = false;

    void Start()
    {
        axeDetection = FindObjectOfType<AxeDetection>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasExited && other.gameObject.CompareTag("Axe"))
        {
            hasExited = true;

            animator.SetTrigger("DummyHit");
            var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMeshPro>().text = axeDetection.axeDamage.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hasExited = false;
    }
}
