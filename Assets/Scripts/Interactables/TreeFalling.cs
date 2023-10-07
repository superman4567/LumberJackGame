using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class TreeFalling : Interactable
{
    [SerializeField] private float waitForTreeToFallTime;
    [SerializeField] private Rigidbody rigidBody;

    private float timer;

    private void Start()
    {
        StartCoroutine(PlayTreeLandingSound()); 
    }

    private void Update()
    {
        CheckIfStatic();
    }

    private void CheckIfStatic()
    {
        timer += Time.deltaTime;

        if (timer > waitForTreeToFallTime)
        {
            if (Mathf.Approximately(rigidBody.velocity.magnitude, 0f))
            {
                canBeInteractedWith = false;
                rigidBody.isKinematic = false;
                Invoke("InteractComplete", 15f);
            }
        }
    }

    public override void InteractComplete()
    {
        Destroy(gameObject);
    }
   
    private IEnumerator PlayTreeLandingSound()
    {
        yield return new WaitForSeconds(3);
        AkSoundEngine.PostEvent("Play_Tree_Landing_SLOW_SFX", gameObject);
    }

}
