using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlockDetection : MonoBehaviour
{
    private ObjectFader fader;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");  
        
        if(player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null)
                    return;
                
                if(hit.collider.gameObject == player)
                {
                    if(fader != null)
                    {
                        fader.DoFade = false;
                    }
                }
                else
                {
                    fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if(fader != null)
                    {
                        fader.DoFade = true;
                    }
                }
            }
        }
    }
}
