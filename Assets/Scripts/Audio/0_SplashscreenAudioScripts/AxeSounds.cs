using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSounds : MonoBehaviour
{
    public void AxeImpactSound()
    {
        AudioManager.instance.PlaySoundEffect(0);
    }
}
