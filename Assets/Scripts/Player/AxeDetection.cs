using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class AxeDetection : MonoBehaviour
{
    public bool axeHitSomething = false;
    public Transform axe;
    public int axeDamage = 20;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            axeHitSomething = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("I hit an Enemy!");
            axeHitSomething = true;
        }
    }

    public int GetAxeDamage()
    {
        return axeDamage;
    }
}
