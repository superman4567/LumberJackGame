using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapToBroken : MonoBehaviour
{
    [SerializeField] private Transform[] objectsToChangeAlpha; 
    [SerializeField] private Collider ParentCollider;
    [SerializeField] private GameObject defaultVersion;
    [SerializeField] private GameObject brokenVersion;

    [SerializeField] private float fadeInstantiationTime = 10;
    [SerializeField] private float DestroyInstantiationTime = 12;

    [SerializeField] Material[] materials;
    public Action dropWoodAndCoins;


    private void Start()
    {
        foreach (Transform obj in objectsToChangeAlpha)
        {
            obj.GetComponent<MeshRenderer>().material = materials[0];
        }
        defaultVersion.gameObject.SetActive(true);
        brokenVersion.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Axe"))
        {
            ParentCollider.enabled = false;
            defaultVersion.gameObject.SetActive(false);
            brokenVersion.SetActive(true);

            StartCoroutine(StartFade());
            dropWoodAndCoins?.Invoke();
            Invoke("DestroyGameObject", DestroyInstantiationTime);
        }
    }

    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(fadeInstantiationTime);
        foreach (Transform obj in objectsToChangeAlpha)
        {
            obj.GetComponent<MeshRenderer>().material = materials[1];

            Animator animator = obj.GetComponent<Animator>();
            animator.SetTrigger("Lerp");
        }
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
