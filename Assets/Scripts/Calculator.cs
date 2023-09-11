using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField] float roundMultiplier = 2f;
    [SerializeField] float enemiesTospawn = 10f;
    [SerializeField] int maxRounds;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < maxRounds; i++)
        {
            enemiesTospawn = Mathf.CeilToInt(enemiesTospawn + roundMultiplier);
            Debug.Log(enemiesTospawn);
        }
    }

    
}
