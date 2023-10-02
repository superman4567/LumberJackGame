using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkManualSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("Play_Fireplace_1_SFX__cabin_", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
