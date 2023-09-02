using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    public float fadeSpeed, fadeAmount;
    private float originalOpcaity;

    [SerializeField]
    private Material[] materials;
    public bool DoFade = false;

    void Start()
    {
        materials= GetComponentInChildren<Renderer>().materials;

        for (int i = 0; i < materials.Length; i++)
        {
            originalOpcaity = materials[i].color.a;
        }
    }

    void Update()
    {
        if (DoFade)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }

    private void FadeNow()
    {

        for (int i = 1; i < materials.Length; i++)
        {
            Color currentColor = materials[i].color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
            materials[i].color = smoothColor;
        }
    }

    private void ResetFade()
    {
        for (int i = 1; i < materials.Length; i++)
        {
            Color currentColor = materials[i].color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, originalOpcaity, fadeSpeed * Time.deltaTime));
            materials[i].color = smoothColor;
        }
    }
}
